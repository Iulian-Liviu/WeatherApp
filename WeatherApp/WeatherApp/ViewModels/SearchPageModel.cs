using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    [ObservableObject]
    public partial class SearchPageModel
    {
        readonly CancellationTokenSource _cancellationTokenSource;

        public SearchPageModel()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            CitiesResults = new ObservableCollection<Result>();
        }

        [ObservableProperty]
        ObservableCollection<Result> citiesResults;

        [RelayCommand]
        public async Task SearchCitie(string queryText)
        {
            if (queryText.Length >= 2)
            {
                CancellationToken cancellationToken = _cancellationTokenSource.Token;
                try
                {
                    while (!cancellationToken.IsCancellationRequested && queryText.Length > 3)
                    {
                        using HttpClient client = new();
                        HttpResponseMessage response = await client.GetAsync($"https://geocoding-api.open-meteo.com/v1/search?name={queryText}&count=5", cancellationToken);
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonBody = await response.Content.ReadAsStringAsync(cancellationToken);

                            var data = SearchedCities.FromJson(jsonBody);

                            if (data.Results != null)
                            {
                                foreach (var item in data.Results)
                                {
                                    item.ImageUri = $"https://assets.open-meteo.com/images/country-flags/{item.CountryCode.ToLower()}.svg";

                                    CitiesResults.Add(item);
                                }
                                _cancellationTokenSource.Cancel();
                            }
                            else
                            {
                                _cancellationTokenSource.Cancel();
                                CitiesResults = new();
                                AlertUser($"{data.Results.Count}");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    AlertUser($"{e.Message}");

                }
            }

        }
        private static async void AlertUser(string mess)
        {
            await Shell.Current.DisplayAlert("Info", mess, "OK");
        }

        [RelayCommand]
        public void CancelRequest()
        {
            if (_cancellationTokenSource != null && _cancellationTokenSource.IsCancellationRequested == false)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
        }
    }
}
