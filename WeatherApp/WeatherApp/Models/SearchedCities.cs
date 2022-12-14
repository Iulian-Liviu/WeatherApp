// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using WeatherApp.Models;
//
//    var searchedCities = SearchedCities.FromJson(jsonString);

namespace WeatherApp.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class SearchedCities
    {
        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public List<Result> Results { get; set; }

        [JsonProperty("generationtime_ms", NullValueHandling = NullValueHandling.Ignore)]
        public double? GenerationtimeMs { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("latitude", NullValueHandling = NullValueHandling.Ignore)]
        public double? Latitude { get; set; }

        [JsonProperty("longitude", NullValueHandling = NullValueHandling.Ignore)]
        public double? Longitude { get; set; }

        [JsonProperty("elevation", NullValueHandling = NullValueHandling.Ignore)]
        public long? Elevation { get; set; }

        [JsonProperty("feature_code", NullValueHandling = NullValueHandling.Ignore)]
        public string FeatureCode { get; set; }

        [JsonProperty("country_code", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryCode { get; set; }

        public string ImageUri { get; set; }

        [JsonProperty("admin1_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Admin1Id { get; set; }

        [JsonProperty("admin2_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Admin2Id { get; set; }

        [JsonProperty("timezone", NullValueHandling = NullValueHandling.Ignore)]
        public string Timezone { get; set; }

        [JsonProperty("population", NullValueHandling = NullValueHandling.Ignore)]
        public long? Population { get; set; }

        [JsonProperty("country_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? CountryId { get; set; }

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("admin1", NullValueHandling = NullValueHandling.Ignore)]
        public string Admin1 { get; set; }

        [JsonProperty("admin2", NullValueHandling = NullValueHandling.Ignore)]
        public string Admin2 { get; set; }
    }

    public partial class SearchedCities
    {
        public static SearchedCities FromJson(string json) => JsonConvert.DeserializeObject<SearchedCities>(json, WeatherApp.Models.ConverterSearchedCities.Settings);
    }

    public static class SerializeSearchedCities
    {
        public static string ToJson(this SearchedCities self) => JsonConvert.SerializeObject(self, WeatherApp.Models.ConverterSearchedCities.Settings);
    }

    internal static class ConverterSearchedCities
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
