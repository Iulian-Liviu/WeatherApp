using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class DayForecast
    {
        public DateTime DateTime { get; set; }
        public double TemperatureMin { get; set; }
        public double TemperatureMax { get; set; }
        public double Rain { get; set; }
        public double Sun { get; set; }
    }
}
