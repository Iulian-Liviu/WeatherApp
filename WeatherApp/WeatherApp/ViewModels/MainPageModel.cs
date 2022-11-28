using System.Collections.ObjectModel;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Models;
using WeatherApp.Views;

namespace WeatherApp.ViewModels
{
    [ObservableObject]
    public partial class MainPageModel
    {
        private CancellationTokenSource _cancelTokenSource;

        private bool _isCheckingLocation;

        public MainPageModel()
        {
            IsDataLoading = false;
            DaysForecasts = new ObservableCollection<DayForecast>();
        }

        [ObservableProperty]
        private bool isDataLoading;

        [ObservableProperty]
        private ObservableCollection<DayForecast> daysForecasts;

        [ObservableProperty]
        private ForecastModel todayForecast;

        [ObservableProperty]
        private string locationName;

        [ObservableProperty]
        private PlaceLocationModel placeLocation;

        [RelayCommand]
        public async Task GetCurrentLocation()
        {
            try
            {
                NetworkAccess accessType = Connectivity.Current.NetworkAccess;

                if (accessType == NetworkAccess.Internet || accessType == NetworkAccess.Unknown)
                {
                    // Connection to internet is available
                    _cancelTokenSource = new CancellationTokenSource();

                    while (!_cancelTokenSource.Token.IsCancellationRequested)
                    {
                        _isCheckingLocation = true;
                        IsDataLoading = true;

                        GeolocationRequest request = new(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));


                        Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);
                        _cancelTokenSource.Token.ThrowIfCancellationRequested();
                        if (location != null)
                        {
                            if (location != null && location.IsFromMockProvider)
                            {
                                // location is from a mock provider
                                request.DesiredAccuracy = GeolocationAccuracy.Best;
                                location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);
                                _cancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            // call the weather api
                            IsDataLoading = false;

                            PlaceLocationModel place = await GetPlaceModel(location.Latitude, location.Longitude, cancellationToken: _cancelTokenSource.Token);
                            ForecastModel data = await Get7DaysForecacstModel(location.Latitude, location.Longitude, cancellationToken: _cancelTokenSource.Token);
                            ForecastModel forecastModel = await GetTodyForecast(location.Latitude, location.Longitude, cancellationToken: _cancelTokenSource.Token);
                            _cancelTokenSource.Token.ThrowIfCancellationRequested();

                            if (place != null && data != null && forecastModel != null)
                            {
                                PlaceLocation = place;
                                TodayForecast = forecastModel;

                                LocationName = place.Address.Village ?? place.Address.City;

                                if (DaysForecasts.Count > 0)
                                {
                                    DaysForecasts.Clear();
                                }

                                for (int i = 0; i < data.Daily.Time.Count; i++)
                                {

                                    DaysForecasts.Add(new DayForecast
                                    {
                                        DateTime = data.Daily.Time[i].DateTime,
                                        TemperatureMin = data.Daily.Temperature2MMin[i],
                                        TemperatureMax = data.Daily.Temperature2MMax[i],
                                        Rain = data.Daily.RainSum[i] ?? 0,
                                        Sun = data.Daily.RainSum[i] ?? 0
                                    });
                                    //_cancelTokenSource.Dispose();
                                }
                                _cancelTokenSource.Cancel();

                                _isCheckingLocation = false;
                                IsDataLoading = false;
                            }
                            else
                            {
                                MainPageModel.AlertUser("Something is wrong..");
                            }
                        }
                    }
                }
                else
                {
                    MainPageModel.AlertUser("No Internet Connectivity...");
                }
            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException


            catch (System.OperationCanceledException oce)
            {
                MainPageModel.AlertUser($"The operation was canceled");
                Console.WriteLine(oce.Message);
            }
            catch (Exception ex)
            {
                // Unable to get location
                MainPageModel.AlertUser($"{ex.Message}, {ex.InnerException}, {ex.StackTrace}, {ex.Data}");
                _cancelTokenSource.Cancel();
                _cancelTokenSource.Dispose();
            }
            finally
            {
                _isCheckingLocation = false;
                IsDataLoading = false;
                _cancelTokenSource.Cancel();

                _cancelTokenSource.Dispose();
            }
        }

        private async Task<ForecastModel> Get7DaysForecacstModel(double lat, double lon, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using HttpClient client = new();

                string today = DateTime.Today.Date.AddDays(1).ToString("yyyy-MM-dd");

                string the7Day = DateTime.Today.AddDays(7).Date.ToString("yyyy-MM-dd");


                string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&models=best_match&daily=temperature_2m_max,temperature_2m_min,precipitation_sum,rain_sum&current_weather=true&timezone=auto&start_date={today}&end_date={the7Day}";

                HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
                _cancelTokenSource.Token.ThrowIfCancellationRequested();
                if (response.IsSuccessStatusCode)
                {
                    string jsonBody = await response.Content.ReadAsStringAsync(cancellationToken);
                    return ForecastModel.FromJson(jsonBody);
                }
                return null;
            }
            return null;
        }
        private async Task<ForecastModel> GetTodyForecast(double lat, double lon, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using HttpClient client = new();

                string today = DateTime.Today.Date.ToString("yyyy-MM-dd");



                string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&models=best_match&daily=temperature_2m_max,temperature_2m_min,precipitation_sum,rain_sum&current_weather=true&timezone=auto&start_date={today}&end_date={today}";

                HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
                _cancelTokenSource.Token.ThrowIfCancellationRequested();
                if (response.IsSuccessStatusCode)
                {
                    string jsonBody = await response.Content.ReadAsStringAsync(cancellationToken);
                    return ForecastModel.FromJson(jsonBody);
                }
                return null;
            }
            return null;
        }

        private async Task<PlaceLocationModel> GetPlaceModel(double lat, double lon, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using HttpClient client = new();
                HttpResponseMessage response = await client.GetAsync($"https://geocode.maps.co/reverse?lat={lat}&lon={lon}", cancellationToken);
                _cancelTokenSource.Token.ThrowIfCancellationRequested();
                if (response.IsSuccessStatusCode)
                {
                    string jsonBody = await response.Content.ReadAsStringAsync(_cancelTokenSource.Token);

                    return PlaceLocationModel.FromJson(jsonBody);
                }
                return null;
            }
            return null;

        }
        private static async void AlertUser(string mess)
        {
            await Shell.Current.DisplayAlert("Info", mess, "OK");
        }

        [RelayCommand]
        public async void NavigateToSearchPage()
        {
            await Shell.Current.GoToAsync(nameof(SearchPage));
        }

        [RelayCommand]
        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            {
                _cancelTokenSource.Cancel();
                _cancelTokenSource.Dispose();

            }
        }
    }
}
