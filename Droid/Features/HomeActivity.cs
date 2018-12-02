
using System;
using System.Collections.Generic;
using Android.App;

using Android.OS;
using Android.Support.V7.Widget;
using Android.Widget;
using BibliotecaUdeA.Business.Dtos;

namespace BibliotecaUdeA.Droid.Features
{
    [Activity(Label = "HomeActivity", MainLauncher = true)]
    public class HomeActivity : Activity
    {
        private  RecyclerView rv_books;
        private EditText search_box;
        private List<BookItem> books;


    protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            LoadViews();
        }

        private void LoadViews()
        {
            rv_books = FindViewById<RecyclerView>(Resource.Id.rv_books);
            search_box = FindViewById<EditText>(Resource.Id.search_box);
        }
    }
}
