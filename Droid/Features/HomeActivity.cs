using System.Collections.Generic;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Widget;
using BibliotecaUdeA.Business.DependencyInjection;
using BibliotecaUdeA.Business.Dtos;
using BibliotecaUdeA.Droid.DependenctInjection;
using BibliotecaUdeA.Droid.Features.Loaders;
using Java.Lang;
using Ninject.Modules;
using Steelkiwi.Com.Library;

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
        private LoaderResponse<BaseResponse<BooksResponse>> loaderResponse;
        private DotsLoaderView loaderView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            InjectDependencies();
            LoadViews();
            InitLoaders();
            FecthListBooks();
            loaderView.Show();
        }
        #region Class methods

        private void InjectDependencies()
        {
            var modules = new NinjectModule[]
            {
                new PlatformModule()
            };

            DependencyInjectionManager dependencyInjectionManager = new DependencyInjectionManager();
            dependencyInjectionManager.ConfigureInjection(modules);
        }
        private void LoadViews()
        {
            rv_books = FindViewById<RecyclerView>(Resource.Id.rv_books);
            loaderView = FindViewById<DotsLoaderView>(Resource.Id.dotsLoaderView);
            search_box = FindViewById<EditText>(Resource.Id.search_box);

        }

        private void FecthListBooks()
        {
          booksTaskLoader.ForceLoad();
        }

        private void PrepareRecyclers()
        {
            booksAdapter = new BooksAdapter(this, books);
            booksAdapter.ItemClick += Books_ItemClick;
            rv_books.SetLayoutManager(new LinearLayoutManager(this));
            rv_books.HasFixedSize = true;
            rv_books.SetAdapter(booksAdapter);
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
            switch (id)
            {
                case loaderId:
                    return booksTaskLoader;
                default:
                    throw new System.ArgumentException();
            }
        }

        public void OnLoadFinished(Loader loader, Object data)
        {
            if (data.GetType() == typeof(LoaderResponse<BaseResponse<BooksResponse>>))
            {
                SupportLoaderManager.GetLoader(loaderId).CancelLoad();
                loaderResponse = (LoaderResponse<BaseResponse<BooksResponse>>)data;
                if (loaderResponse.Response.Exception == null)
                {
                    var response = loaderResponse.Response.Data;
                    books = response.Books;
                    PrepareRecyclers();
                    loaderView.Hide();
                }
                else
                {
                    Toast.MakeText(this, "no Se encontro nada", ToastLength.Long).Show();
                }
            }

        }

        public void OnLoaderReset(Loader loader)
        {
            //Not implemented
        }

        #endregion
    }
}
