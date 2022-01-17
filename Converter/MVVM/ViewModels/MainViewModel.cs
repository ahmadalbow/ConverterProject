using Converter.Core;
using ServiceReference1;
using System;
using System.Windows;


namespace Converter.MVVM.ViewModels
{
    public class MainViewModel:ObservableObject
    {
        // The Command For changing the Icon of the Close Button once the Mouse Enter it.
        public RelayCommand<string> ChangeCloseButtonIconCommand { get; set; }
        // The Command For closing the application
        public RelayCommand<object> CloseButtonCommand { get; set; }
        // The Command For minimizing the application
        public RelayCommand<object> MinimizeButtonCommand { get; set; }
        // This command will call the converting motheod from the server
        public RelayCommand<object> ConvertButtonCommand { get; set; }
        // This string containg the path of the Image that is used in the Close Command   
        private string _closeIconSource;
        public string CloseIconSource
        {
            get { return _closeIconSource; }
            set
            {
                _closeIconSource = value;
                OnPropertyChanged();
            }
        }
        // This string is the entered amount in the Textbox. 
        private string _enteredAmount;
        public string EnteredAmount
        {
            get { return _enteredAmount; }
            set
            {
                _enteredAmount = value;
                OnPropertyChanged();
            }
        }
        /*  This string is the converted amount from the server, I assigned the value to it in the 
            ConvertButtonCommand and binded it to Textblock where the amount in words will be shown*/
        private string _convertedAmount;
        public string ConvertedAmount
        {
            get { return _convertedAmount; }
            set
            {
                _convertedAmount = value;
                OnPropertyChanged();
            }
        }
        /* it is binded to the Visibility of the Textblocks where the amount in words will be shown,
           it will be changed to visibile whence we get the converterd amount from the server 
           and will be changed to hidden whence we lost the connection with the server*/
        private Visibility _sResultTextBlockVisibile;
        public Visibility IsResultTextBlockVisibile
        {
            get { return _sResultTextBlockVisibile; }
            set 
                {  
                    _sResultTextBlockVisibile = value;
                    OnPropertyChanged();
                }
        }
        
        public MainViewModel()
        {
            // Here we create a new client
            ServiceClient client = new ServiceClient();
            // Here we set the default value of the visibility of the textblocks to hidden.
            IsResultTextBlockVisibile = Visibility.Hidden;

            ConvertButtonCommand = new RelayCommand<object>((o) =>
            {
                // Here we try to call the converting mathod from the server
                try
                {                    
                    ConvertedAmount = client.ConvertDollarsToTextAsync(EnteredAmount).Result;
                    IsResultTextBlockVisibile = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    IsResultTextBlockVisibile = Visibility.Hidden;
                    MessageBox.Show("No Connection");
                }
            });
            
            // Here we set the default path of the Image that is used in the close button 
            CloseIconSource = "/Images/CloseBlack.png";
            // we passed the path of the Image as string in CommandParammeter
            ChangeCloseButtonIconCommand = new RelayCommand<string>(o =>
            {
                CloseIconSource = o;
            });
            CloseButtonCommand = new RelayCommand<object>(o =>
            {
                Application.Current.MainWindow.Close();
                
            });
            MinimizeButtonCommand = new RelayCommand<object>(o =>
            {
                Application.Current.MainWindow.WindowState= WindowState.Minimized;
            });

        }
      
    }
}
