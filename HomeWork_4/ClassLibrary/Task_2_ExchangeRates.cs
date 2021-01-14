using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class ExchangeRates
    {
        [JsonProperty("r030")]
        public string R030 { get; set; }
        [JsonProperty("txt")]
        public string Text { get; set; }
        [JsonProperty("rate")]
        public decimal Rate { get; set; }
        [JsonProperty("cc")]
        public string Cc { get; set; }
        [JsonProperty("exchangedate")]
        public string ExchangeDate { get; set; }
    }
}
