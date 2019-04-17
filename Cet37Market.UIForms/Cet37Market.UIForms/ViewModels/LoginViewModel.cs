namespace Cet37Market.UIForms.ViewModels
{
    using Cet37Market.UIForms.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public LoginViewModel()
        {
            this.Email = "ratimghimire@gmail.com";
            this.Password = "123456";
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

            if(!this.Email.Equals("ratimghimire@gmail.com") || this.Password.Equals("123456"))
            {
                await Application.Current.MainPage.DisplayAlert("Ok", "Email or Password wrong!!!", "OK");
                return;
            }

            //await Application.Current.MainPage.DisplayAlert("Ok", "Fuck Entrámos!!!", "OK");

            //para abrir outra pagina
            MainViewModel.GetInstance().Products = new ProductViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductPage());
        }

    }
}
