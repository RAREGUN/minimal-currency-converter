using System.Globalization;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using minimal_currency_converter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace minimal_currency_converter.Controllers
{
    public class CurrencyDataController
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter
                {
                    DateTimeStyles = DateTimeStyles.AssumeUniversal
                }
            }
        };
        
        public async Task<CurrencyData> GetCurrencyData()
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            CurrencyData data = null;

            try
            {
                string stringData =
                    await webClient.DownloadStringTaskAsync("https://www.cbr-xml-daily.ru/daily_json.js");

                data = JsonConvert.DeserializeObject<CurrencyData>(stringData, Settings);
            }
            catch
            {
                // ignored
            }

            return data;
        }

        public bool TryGetCurrencyFromCharName(CurrencyData currencyData, string charName, out CurrencyData.Currency currency)
        {
            currency = null;
            
            if (string.IsNullOrWhiteSpace(charName))
                return false;
            
            return currencyData.Currencies.TryGetValue(charName, out currency);
        }
    }
}