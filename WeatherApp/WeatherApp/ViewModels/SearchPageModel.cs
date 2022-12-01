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
        private Task queryTask;
        private IDispatcher uiThread;
        private bool _isDataLoading;

        public SearchPageModel()
        {
            CitiesResults = new ObservableCollection<Result>();
            _isDataLoading = false;
            Shell.Current.NavigatedTo += Current_NavigatedTo;
        }

        private void Current_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            if (e.GetType() == Type.GetType(nameof(MainPage)))
            {
                queryTask.Dispose();
                CitiesResults.Clear();
            }
        }

        [ObservableProperty]
        ObservableCollection<Result> citiesResults;

        [RelayCommand]
        public void SearchCities(string queryText)
        {
            if (queryText.Length >= 3 && _isDataLoading == false)
            {
                try
                {
                    _isDataLoading = true;
                    uiThread = Dispatcher.GetForCurrentThread();

                    if (uiThread != null)
                    {
                        if (CitiesResults.Count > 0)
                        {

                            CitiesResults.Clear();
                        }

                        queryTask = new Task(new Action(async () =>
                        {
                            using HttpClient client = new();
                            HttpResponseMessage response = await client.GetAsync($"https://geocoding-api.open-meteo.com/v1/search?name={queryText}&count=10");
                            if (response.IsSuccessStatusCode)
                            {
                                string jsonBody = await response.Content.ReadAsStringAsync();

                                var data = SearchedCities.FromJson(jsonBody);

                                if (data.Results != null)
                                {
                                    foreach (var item in data.Results)
                                    {

                                        uiThread.Dispatch(() => {
                                            item.ImageUri = $"https://flagsapi.com/{item.CountryCode}/flat/64.png";

                                            CitiesResults.Add(item);
                                        });

                                    }
                                    _isDataLoading = false;
                                }
                                else
                                {
                                    CitiesResults = new();
                                    AlertUser($"{data.Results.Count}");
                                }
                            }
                        }));
                        queryTask.Start();
                    }
                    else
                    {
                        AlertUser("UI Thread is null");
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
       
    }
}
