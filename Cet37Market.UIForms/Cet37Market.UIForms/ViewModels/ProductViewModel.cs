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
    public class ProductViewModel:INotifyPropertyChanged
    {

        #region Attributes

        private ApiService apiService;
        private ObservableCollection<Product> products;
        private bool isRefreshing;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        //TODO: Colocar inotify generico
        public ObservableCollection<Product> Products
        {
            get
            {
                return products;
            }
            set
            {
                if (this.products != value)
                {
                    this.products = value;
                    //Proertychanged will be responsible to change the database in  XAML if original data changes
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Products"));
                }
            }

        }
        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    //Proertychanged will be responsible to change the database in  XAML if original data changes
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshing"));
                }
            }

        }

        #endregion
        public ProductViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var response = await this.apiService.GetListAsync<Product>(
                "http://ratim47-001-site1.gtempurl.com",
                "/api",
                "/Products"
                );

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
