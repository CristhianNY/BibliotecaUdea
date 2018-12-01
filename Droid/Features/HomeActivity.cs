
using Android.App;

using Android.OS;


namespace BibliotecaUdeA.Droid.Features
{
    [Activity(Label = "HomeActivity", MainLauncher = true)]
    public class HomeActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
        }
    }
}
