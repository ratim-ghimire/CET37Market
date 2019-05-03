namespace Cet37Market.UIForms.ViewModels
{
    using System.Windows.Input;
    using Xamarin.Forms;
    using Cet37Market.UIForms.Views;
    using GalaSoft.MvvmLight.Command;

    //View Model for each item in List
    public class MenuItemViewModel : CET37Market.Common.Models.Menu
    {
        public ICommand SelectMenuCommand => new RelayCommand(this.MenuSelect);
        private async void MenuSelect()
        {
            App.Master.IsPresented = false;
            //var main = MainViewModel.GetInstance();

            switch (this.PageName)
            {
                case "AboutPage":
                    await App.Navigator.PushAsync(new AboutPage());
                    break;

                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;
                    //Gets back to login page
                default:
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
            }
        }
    }
}
