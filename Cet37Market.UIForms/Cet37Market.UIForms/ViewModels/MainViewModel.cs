using CET37Market.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cet37Market.UIForms.ViewModels
{
    public class MainViewModel
    {
        //attribute singleton
        private static MainViewModel instance;

        public LoginViewModel Login { get; set; }

        public ProductViewModel Products{ get; set; }

        //To catch the token generated
        public TokenResponse Token { get; set; }
        
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public MainViewModel()
        {
            instance = this;
            this.LoadMenus();
        }

        private void LoadMenus()
        {
            var Menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_info",
                    PageName  ="AboutPage",
                    Title="About"
                },
                new Menu
                {
                    Icon = "ic_launcher",
                    PageName  ="SetupPage",
                    Title="Setup"
                },
                new Menu
                {
                    Icon = "ic_exit",
                    PageName  ="LoginPage",
                    Title="Close Section"
                }
            };
            this.Menus = new ObservableCollection<MenuItemViewModel>(Menus.Select(m=> new MenuItemViewModel
            {
                Icon = m.Icon,
                PageName = m.PageName,
                Title = m.Title

            }).ToList());

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
