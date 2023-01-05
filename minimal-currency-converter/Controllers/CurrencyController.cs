using minimal_currency_converter.Models;

namespace minimal_currency_converter.Controllers
{
    public class CurrencyController
    {
        public decimal Convert(CurrencyData.Currency original, CurrencyData.Currency result, decimal amount)
        {
            return original.Value / result.Value * amount;
        }
    }
}