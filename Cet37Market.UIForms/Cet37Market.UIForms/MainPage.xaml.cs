using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cet37Market.UIForms
{
    public partial class MainPage : ContentPage 
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            lblFollowers.Text = (int.Parse(lblFollowers.Text) + 1).ToString();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
          ToolbarItem tbi = (ToolbarItem)sender;
          bool result=   await Application.Current.MainPage.DisplayAlert("Selecionado!", tbi.Text, "OK", "Cancelar");
            if (tbi.Text.Equals("Editar")  && result)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new DemoPage());
            }
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {

        }

        private void ToolbarItem_Clicked_2(object sender, EventArgs e)
        {

        }

        private void ToolbarItem_Clicked_3(object sender, EventArgs e)
        {

        }

        private void ToolbarItem_Clicked_4(object sender, EventArgs e)
        {

        }

        private void ToolbarItem_Clicked_5(object sender, EventArgs e)
        {

        }
    }
}
