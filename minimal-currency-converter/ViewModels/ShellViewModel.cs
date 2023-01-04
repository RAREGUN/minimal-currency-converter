using System.Collections.Generic;
using Caliburn.Micro;

namespace minimal_currency_converter.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly SimpleContainer _container;
        private readonly IEventAggregator _eventAggregator;
        
        private string _leftCurrencyAmount;
        public string LeftCurrencyAmount
        {
            get => _leftCurrencyAmount;
            set
            {
                if (_leftCurrencyAmount == value)
                    return;
                
                _leftCurrencyAmount = value;
                
                NotifyOfPropertyChange(() => LeftCurrencyAmount);
            }
        }
        
        private string _rightCurrencyAmount;
        public string RightCurrencyAmount
        {
            get => _rightCurrencyAmount;
            set
            {
                if (_rightCurrencyAmount == value)
                    return;
                
                _rightCurrencyAmount = value;
                
                NotifyOfPropertyChange(() => RightCurrencyAmount);
            }
        }
        
        public List<string> LeftCurrencyKind
        {
            get
            {
                return new List<string> { "Left", "bar" };
            }
        }
        
        public List<string> RightCurrencyKind
        {
            get
            {
                return new List<string> { "Right", "bar" };
            }
        }
        
        private string _selectedLeftCurrencyKind;
        public string SelectedLeftCurrencyKind
        {
            get => _selectedLeftCurrencyKind;
            set
            {
                _selectedLeftCurrencyKind = value;
                NotifyOfPropertyChange(() => SelectedLeftCurrencyKind);
            }
        }
        
        private string _selectedRightCurrencyKind;
        public string SelectedRightCurrencyKind
        {
            get => _selectedRightCurrencyKind;
            set
            {
                _selectedRightCurrencyKind = value;
                NotifyOfPropertyChange(() => SelectedRightCurrencyKind);
            }
        }

        public ShellViewModel(SimpleContainer container)
        {
            _container = container;
            _eventAggregator = _container.GetInstance<IEventAggregator>();
            _eventAggregator = _container.GetInstance<IEventAggregator>();
        }

        public void RefreshButtonClick()
        {
            
        }
    }
}