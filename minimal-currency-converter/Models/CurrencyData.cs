using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace minimal_currency_converter.Models
{
    public class CurrencyData
    {
        public ICollection<string> CurrenciesCollection => Currencies.Keys;

        [JsonProperty("Date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("PreviousDate")]
        public DateTimeOffset PreviousDate { get; set; }

        [JsonProperty("PreviousURL")]
        public string PreviousUrl { get; set; }

        [JsonProperty("Timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("Valute")]
        public Dictionary<string, Currency> Currencies { get; set; }
        
        public class Currency
        {
            [JsonProperty("ID")]
            public string ID { get; set; }

            [JsonProperty("NumCode")]
            public string NumCode { get; set; }

            [JsonProperty("CharCode")]
            public string CharCode { get; set; }

            [JsonProperty("Nominal")]
            public decimal Nominal { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("Value")]
            public decimal Value { get; set; }

            [JsonProperty("Previous")]
            public decimal Previous { get; set; }
        }
    }
}