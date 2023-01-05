using System.Collections.Generic;
using Caliburn.Micro;
using minimal_currency_converter.Controllers;
using minimal_currency_converter.Models;

namespace minimal_currency_converter.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly SimpleContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly CurrencyDataController _currencyDataController;
        private readonly CurrencyController _currencyController;

        private CurrencyData _currencyData;
        public CurrencyData CurrencyData
        {
            get => _currencyData;
            private set
            {
                if (_currencyData == value)
                    return;
                
                _currencyData = value;
                
                NotifyOfPropertyChange(() => CurrencyData);
                NotifyOfPropertyChange(() => LeftCurrencyKind);
                NotifyOfPropertyChange(() => RightCurrencyKind);
            }
        }

        private string _leftCurrencyAmount = "0.00";
        public string LeftCurrencyAmount
        {
            get => _leftCurrencyAmount;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    value = "0";
                else if (value.Contains("."))
                    value = value.Replace('.', ',');

                if (_leftCurrencyAmount == value)
                    return;
                
                if (!decimal.TryParse(value, out decimal leftAmount))
                    return;
                
                _leftCurrencyAmount = value;

                if (!_currencyDataController.TryGetCurrencyFromCharName(_currencyData, SelectedLeftCurrencyKind, out CurrencyData.Currency leftCurrency))
                    return;
                
                if (!_currencyDataController.TryGetCurrencyFromCharName(_currencyData, SelectedRightCurrencyKind, out CurrencyData.Currency rightCurrency))
                    return;

                RightCurrencyAmount = $"{_currencyController.Convert(leftCurrency, rightCurrency, leftAmount):0.00}";
                
                NotifyOfPropertyChange(() => LeftCurrencyAmount);
            }
        }
        
        private string _rightCurrencyAmount = "0.00";
        public string RightCurrencyAmount
        {
            get => _rightCurrencyAmount;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    value = "0";
                else if (value.Contains("."))
                    value = value.Replace('.', ',');
                
                if (_rightCurrencyAmount == value)
                    return;
                
                if (!decimal.TryParse(value, out decimal rightAmount))
                    return;
                
                _rightCurrencyAmount = value;
                
                if (!_currencyDataController.TryGetCurrencyFromCharName(_currencyData, SelectedLeftCurrencyKind, out CurrencyData.Currency leftCurrency))
                    return;
                
                if (!_currencyDataController.TryGetCurrencyFromCharName(_currencyData, SelectedRightCurrencyKind, out CurrencyData.Currency rightCurrency))
                    return;

                LeftCurrencyAmount = $"{_currencyController.Convert(rightCurrency, leftCurrency, rightAmount):0.00}";
                
                NotifyOfPropertyChange(() => RightCurrencyAmount);
            }
        }
        
        public ICollection<string> LeftCurrencyKind
        {
            get
            {
                return CurrencyData?.CurrenciesCollection ?? new List<string>(0);
            }
        }
        
        public ICollection<string> RightCurrencyKind
        {
            get
            {
                return CurrencyData?.CurrenciesCollection ?? new List<string>(0);
            }
        }
        
        private string _selectedLeftCurrencyKind;
        public string SelectedLeftCurrencyKind
        {
            get => _selectedLeftCurrencyKind;
            set
            {
                if (_selectedLeftCurrencyKind == value)
                    return;
                
                if (!_currencyDataController.TryGetCurrencyFromCharName(CurrencyData, value, out CurrencyData.Currency leftCurrency))
                    return;
                
                _selectedLeftCurrencyKind = value;
                
                if (!_currencyDataController.TryGetCurrencyFromCharName(_currencyData, SelectedRightCurrencyKind, out CurrencyData.Currency rightCurrency))
                    return;

                decimal rightAmount = decimal.Parse(RightCurrencyAmount);
                
                LeftCurrencyAmount = $"{_currencyController.Convert(rightCurrency, leftCurrency, rightAmount):0.00}";
                
                NotifyOfPropertyChange(() => SelectedLeftCurrencyKind);
            }
        }
        
        private string _selectedRightCurrencyKind;
        public string SelectedRightCurrencyKind
        {
            get => _selectedRightCurrencyKind;
            set
            {
                if (_selectedRightCurrencyKind == value)
                    return;
                
                if (!_currencyDataController.TryGetCurrencyFromCharName(CurrencyData, value, out CurrencyData.Currency rightCurrency))
                    return;
                
                _selectedRightCurrencyKind = value;
                
                if (!_currencyDataController.TryGetCurrencyFromCharName(_currencyData, SelectedLeftCurrencyKind, out CurrencyData.Currency leftCurrency))
                    return;

                decimal leftAmount = decimal.Parse(LeftCurrencyAmount);
                
                RightCurrencyAmount = $"{_currencyController.Convert(leftCurrency, rightCurrency, leftAmount):0.00}";
                
                NotifyOfPropertyChange(() => SelectedRightCurrencyKind);
            }
        }

        private string _refreshButtonContent;
        public string RefreshButtonContent
        {
            get => _refreshButtonContent;
            set
            {
                if (_refreshButtonContent == value)
                    return;
                
                _refreshButtonContent = value;
                
                NotifyOfPropertyChange(() => RefreshButtonContent);
            }
        }

        public ShellViewModel(SimpleContainer container)
        {
            _container = container;
            _eventAggregator = _container.GetInstance<IEventAggregator>();
            _currencyDataController = _container.GetInstance<CurrencyDataController>();
            _currencyController = _container.GetInstance<CurrencyController>();
            
            UpdateCurrencyData();
        }

        private async void UpdateCurrencyData()
        {
            RefreshButtonContent = "Обновление...";
            
            CurrencyData = await _currencyDataController.GetCurrencyData();

            RefreshButtonContent = CurrencyData == null ? "Ошибка обновления" : "Обновить";
        }

        // ReSharper disable once UnusedMember.Global
        public void RefreshButtonClick()
        {
            UpdateCurrencyData();
        }
    }
}