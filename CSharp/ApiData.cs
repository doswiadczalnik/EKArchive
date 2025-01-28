using System;
using Newtonsoft.Json;

public class ApiData
{
    [JsonProperty("udtczas")]
    public DateTime UdtCzas { get; set; }

    [JsonProperty("znacznik")]
    public int Znacznik { get; set; }

    [JsonProperty("business_date")]
    public DateTime BusinessDate { get; set; }

    [JsonProperty("source_datetime")]
    public DateTime SourceDatetime { get; set; }
}