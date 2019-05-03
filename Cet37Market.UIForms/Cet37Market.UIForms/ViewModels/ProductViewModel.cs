using CET37Market.Common.Models;
using CET37Market.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Cet37Market.UIForms.ViewModels
{
    public class ProductViewModel:BaseViewModel
    {

        #region Attributes

        private ApiService apiService;
        private NetService netService;
        private ObservableCollection<Product> products;
        private bool isRefreshing;

        #endregion

        #region Events


        #endregion

        #region Properties
      

        //TODO: Colocar inotify generico
        public ObservableCollection<Product> Products
        {
            get => this.products;
            set => this.SetValue(ref this.products, value);

        }
        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);

        }
        #endregion
        public ProductViewModel()
        {
            this.apiService = new ApiService();
            this.netService = new NetService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var connection = this.netService.CheckConnection();
            if (!connection.Success)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                return;
            }
            var url = Application.Current.Resources["UrlAPI"].ToString();
            //Gets the products after login with token
            var response = await this.apiService.GetListAsync<Product>(
                url,
                "/api",
                "/products",
                "bearer",
                MainViewModel.GetInstance().Token.Token
                );
            //var response = await this.apiService.GetListAsync<Product>(
            //    "http://ratim47-001-site1.gtempurl.com",
            //    "/api",
            //    "/Products"
            //    );

            this.IsRefreshing = false;
            if (!response.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
                return;
            }

            var myProducts = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(myProducts);
            
        }
    }
}
