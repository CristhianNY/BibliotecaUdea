using System.Collections.Generic;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Widget;
using BibliotecaUdeA.Business.Dtos;
using Java.Lang;

namespace BibliotecaUdeA.Droid.Features
{
    [Android.App.Activity(Label = "HomeActivity", MainLauncher = true)]
    public class HomeActivity : FragmentActivity, LoaderManager.ILoaderCallbacks
    {
        private const int loaderId = 1;
        private BooksTaskLoader booksTaskLoader;
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

        private void InitLoaders()
        {
            booksTaskLoader = new BooksTaskLoader(this,"");
            SupportLoaderManager.InitLoader(loaderId, null, this);
        }
            #endregion


            #region Events

            private void Books_ItemClick(object sender, BookItem item)
        {

            Log.Info("prueba", "se llama el elemento");

        }

        public Loader OnCreateLoader(int id, Bundle args)
        {
            throw new System.NotImplementedException();
        }

        public void OnLoadFinished(Loader loader, Object data)
        {
            throw new System.NotImplementedException();
        }

        public void OnLoaderReset(Loader loader)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
