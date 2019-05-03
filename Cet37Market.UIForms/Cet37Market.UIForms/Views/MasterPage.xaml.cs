namespace Cet37Market.UIForms.Views
{

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    //Master Detail page for controlling all system
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
        }

        //Override to load prefered page/ asscociating 
        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Navigator = this.Navigator;
            App.Master = this;
        }
    }
}