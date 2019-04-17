using Cet37Market.UIForms.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cet37Market.UIForms.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
