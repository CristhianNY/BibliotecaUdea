
using System.Collections.Generic;
using Android.App;

using Android.OS;
using Android.Support.V7.Widget;
using BibliotecaUdeA.Business.Dtos;

namespace BibliotecaUdeA.Droid.Features
{
    [Activity(Label = "HomeActivity", MainLauncher = true)]
    public class HomeActivity : Activity
    {
        private readonly RecyclerView rv_books;

        private List<BookItem> books;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
        }
    }
}
