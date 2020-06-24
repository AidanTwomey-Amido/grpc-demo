using System;
using Newtonsoft.Json;

namespace GrpcGreeterClient
{
    public class AirQualitySample
    {
        [JsonProperty("state_code")]
        public int StateCode { get; set; }

        [JsonProperty("county_code")]
        public int CountyCode { get; set; }

        [JsonProperty("site_number")]
        public int SiteNumber { get; set; }

        [JsonProperty("parameter_code")]
        public int ParameterCode { get; set; }

        [JsonProperty("poc")]
        public int Poc { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("intitude")]
        public double Longitude { get; set; }

        [JsonProperty("datum")]
        public string Datum { get; set; }

        [JsonProperty("parameter")]
        public string Parameter { get; set; }

        [JsonProperty("sample_duration")]
        public string SampleDuration { get; set; }

        [JsonProperty("pollutant_standard")]
        public object PollutantStandard { get; set; }

        [JsonProperty("date_local")]
        public DateTime DateLocal { get; set; }

        [JsonProperty("units_of_measure")]
        public string UnitsOfMeasure { get; set; }

        [JsonProperty("event_type")]
        public string EventType { get; set; }

        [JsonProperty("observation_count")]
        public int ObservationCount { get; set; }

        [JsonProperty("observation_percent")]
        public double ObservationPercent { get; set; }

        [JsonProperty("validity_indicator")]
        public string ValidityIndicator { get; set; }

        [JsonProperty("arithmetic_mean")]
        public double ArithmeticMean { get; set; }

        [JsonProperty("first_max_value")]
        public double FirstMaxValue { get; set; }

        [JsonProperty("first_max_hour")]
        public int FirstMaxHour { get; set; }

        [JsonProperty("aqi")]
        public object Aqi { get; set; }

        [JsonProperty("method_code")]
        public int MethodCode { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("local_site_name")]
        public string LocalSiteName { get; set; }

        [JsonProperty("site_address")]
        public string SiteAddress { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("cbsa_code")]
        public int CbsaCode { get; set; }

        [JsonProperty("cbsa")]
        public string Cbsa { get; set; }

        [JsonProperty("date_of_last_change")]
        public DateTime DateOfLastChange { get; set; }
    }
}
