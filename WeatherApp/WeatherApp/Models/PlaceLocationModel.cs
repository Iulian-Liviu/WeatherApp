﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using WeatherApp.Models;
//
//    var placeLocationModel = PlaceLocationModel.FromJson(jsonString);

namespace WeatherApp.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PlaceLocationModel
    {
        [JsonProperty("place_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? PlaceId { get; set; }

        [JsonProperty("licence", NullValueHandling = NullValueHandling.Ignore)]
        public string Licence { get; set; }

        [JsonProperty("powered_by", NullValueHandling = NullValueHandling.Ignore)]
        public string PoweredBy { get; set; }

        [JsonProperty("osm_type", NullValueHandling = NullValueHandling.Ignore)]
        public string OsmType { get; set; }

        [JsonProperty("osm_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? OsmId { get; set; }

        [JsonProperty("lat", NullValueHandling = NullValueHandling.Ignore)]
        public string Lat { get; set; }

        [JsonProperty("lon", NullValueHandling = NullValueHandling.Ignore)]
        public string Lon { get; set; }

        [JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public Address Address { get; set; }

        [JsonProperty("boundingbox", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Boundingbox { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("village", NullValueHandling = NullValueHandling.Ignore)]
        public string Village { get; set; }

        [JsonProperty("leisure", NullValueHandling = NullValueHandling.Ignore)]
        public string Leisure { get; set; }

        [JsonProperty("road", NullValueHandling = NullValueHandling.Ignore)]
        public string Road { get; set; }

        [JsonProperty("suburb", NullValueHandling = NullValueHandling.Ignore)]
        public string Suburb { get; set; }

        [JsonProperty("district", NullValueHandling = NullValueHandling.Ignore)]
        public string District { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("postcode", NullValueHandling = NullValueHandling.Ignore)]
        public string Postcode { get; set; }

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("country_code", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryCode { get; set; }
    }

    public partial class PlaceLocationModel
    {
        public static PlaceLocationModel FromJson(string json) => JsonConvert.DeserializeObject<PlaceLocationModel>(json, WeatherApp.Models.ConverterPlace.Settings);
    }

    public static class SerializePlace
    {   
        public static string ToJson(this PlaceLocationModel self) => JsonConvert.SerializeObject(self, WeatherApp.Models.ConverterPlace.Settings);
    }

    internal static class ConverterPlace
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
