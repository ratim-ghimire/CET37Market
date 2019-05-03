namespace Cet37Market.UIForms.Droid
{

    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Plugin.CurrentActivity;

    //class for application start up
    [Activity(Label = "Cet37Market.UIForms", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState) //
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            //put here for get access
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            //
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}