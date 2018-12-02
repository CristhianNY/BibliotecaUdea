
using System;
using System.Collections.Generic;
using Android.App;

using Android.OS;
using Android.Support.V7.Widget;
using Android.Util;
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
        private BooksAdapter booksAdapter;


    protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            LoadViews();
        }
        #region Class methods
        private void LoadViews()
        {
            rv_books = FindViewById<RecyclerView>(Resource.Id.rv_books);
            search_box = FindViewById<EditText>(Resource.Id.search_box);
        }

        private void PrepareRecyclers()
        {
            booksAdapter = new BooksAdapter(this, books);
        }
        #endregion


        #region Events

        private void Books_ItemClick(object sender, BookItem item)
        {

            Log.Info("prueba", "se llama el elemento");

        }

            #endregion
        }
}
