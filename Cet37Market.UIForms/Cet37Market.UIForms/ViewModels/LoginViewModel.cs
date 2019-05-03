namespace Cet37Market.UIForms.ViewModels
{
    using Cet37Market.UIForms.Views;
    using CET37Market.Common.Models;
    using CET37Market.Common.Services;
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class LoginViewModel:BaseViewModel
    {

        private ApiService apiService;

        private NetService netService;

        private bool _isrunning;

        private bool _isenabled;

        public string Email { get; set; }

        public string Password { get; set; }
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.netService = new NetService();
            this.Email = "ratimghimire@gmail.com";
            this.Password = "@Nepal0176";
            this.IsEnabled = true;
        }
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter an email", "OK");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter your password", "OK");
                return;
            }
            //to make loader running
            IsRunning = true;
            IsEnabled = false;

            var request = new TokenRequest
            {
               Password =  this.Password,
               Username =  this.Email,
            };
            //Check the net
            var connection = this.netService.CheckConnection();
            if (!connection.Success)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                return;
            }
            //Helps to get the token from server

            var url = Application.Current.Resources["UrlAPI"].ToString();

            var response = await this.apiService.GetTokenAsync(
                url,
                "/Account",
                "/CreateToken",
                request
                );
            IsRunning = false;
            IsEnabled = true;

            if (!response.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Error", " Email or Password Incorrect", "Accept");
                return;
            }

            //Get token 
            var token = (TokenResponse)response.Result;
            var mainViewModel = MainViewModel.GetInstance();

            mainViewModel.Token = token;


            //if(!this.Email.Equals("ratimghimire@gmail.com") || this.Password.Equals("123456"))
            //{
            //    await Application.Current.MainPage.DisplayAlert("Ok", "Email or Password wrong!!!", "OK");
            //    return;
            //}
            //await Application.Current.MainPage.DisplayAlert("Ok", "Fuck Entrámos!!!", "OK");

            //para abrir outra pagina
            mainViewModel.Products = new ProductViewModel();
            //await Application.Current.MainPage.Navigation.PushAsync(new ProductPage());
            Application.Current.MainPage = new MasterPage();
        }
        //Property to run loading till the login is running
        public bool IsRunning
        {
            get => this._isrunning;
            set => this.SetValue(ref this._isrunning, value);
        }
        //Property to disable the button after login
        public bool IsEnabled
        {
            get => this._isenabled;
            set => this.SetValue(ref this._isenabled, value);
        }

        
    }
}
