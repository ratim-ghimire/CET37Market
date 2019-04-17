using System;
using System.Collections.Generic;
using System.Text;

namespace Cet37Market.UIForms.ViewModels
{
    public class MainViewModel
    {
        //attribute singleton
        private static MainViewModel instance;

        public LoginViewModel Login { get; set; }
        public ProductViewModel Products{ get; set; }
        
        public MainViewModel()
        {
            instance = this;
        }

        //this will instance the view model of itself
        public static MainViewModel GetInstance()
        {
            if(instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
    }
}
