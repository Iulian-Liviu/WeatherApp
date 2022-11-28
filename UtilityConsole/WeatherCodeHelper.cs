﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UtilityConsole
{
    public class WeatherCodeHelper
    {
        public Dictionary<string, string> WeatherCodes { get; set; }

        public WeatherCodeHelper() 
        {
            WeatherCodes = new Dictionary<string, string>
            {
                { "0",  "Clear sky" },
                { "1",  "Mainly clear" },
                { "2",  "Partly cloudy" },
                { "3",  "Overcast" },
                { "45", "Fog" },
                { "48", "Rime fog" },
                { "51", "Drizzle : Light" },
                { "53", "Drizzle : moderate" },
                { "56", "Freezing Drizzle : Light" },
                { "57", "Freezing Drizzle : dense" },
                { "61", "Rain : Slight"},
                { "63", "Rain : Moderate"},
                { "65", "Rain : Heavy"},
                { "66", "Freezing : Light"},
                { "67", "Rain : Heavy"},
                { "71", "Snow fall : Slight"},
                { "73", "Snow fall : Moderate"},
                { "75", "Snow fall : Heavy"},
                { "77", "Snow grains"},
                { "80", "Rain showers : Slight"},
                { "81", "Rain showers : Moderate"},
                { "82", "Rain showers : Violent"},
                { "85", "Snow showers : Slight"},
                { "86", "Snow showers : Heavy"},
                { "95", "Thunderstorm : Slight or Moderate"},
                { "96", "Thunderstorm : Slight"},
                { "99", "Thunderstorm : Heavy"}
            };

        }
        public string ToJson()
        {
            var json = JsonConvert.SerializeObject(WeatherCodes);
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "weathercodes.json"), json);
            return json;
        }
    }
}
