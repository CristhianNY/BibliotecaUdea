using System.Collections.Generic;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Widget;
using BibliotecaUdeA.Business.DependencyInjection;
using BibliotecaUdeA.Business.Dtos;
using BibliotecaUdeA.Droid.DependenctInjection;
using BibliotecaUdeA.Droid.Features.Lastfiveelements;
using BibliotecaUdeA.Droid.Features.Loaders;
using Java.Lang;
using Ninject.Modules;
using Steelkiwi.Com.Library;

namespace BibliotecaUdeA.Droid.Features
{
    [Android.App.Activity(Label = "HomeActivity", MainLauncher = true)]
    public class HomeActivity : FragmentActivity, LoaderManager.ILoaderCallbacks , IOnInteractionSearchDialog
    {
        private const int loaderId = 1;
        private BooksTaskLoader booksTaskLoader;
        private  RecyclerView rv_books;
        private EditText search_box;
        private List<BookItem> books;
        private BooksAdapter booksAdapter;
        private LoaderResponse<BaseResponse<BooksResponse>> loaderResponse;
        private DotsLoaderView loaderView;
        private Button btnSearch;

        readonly Android.Runtime.JavaList<string> lasSearch = new JavaList<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            InjectDependencies();
           
            LoadViews();
            GetLastFiveSearch();
            btnSearch.Click += BtnSearch_Click;
            search_box.Click += Search_Box_Click;

                       
        }
        #region Class methods

        private void GetLastFiveSearch()
        {

        }
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
            btnSearch = FindViewById<Button>(Resource.Id.btn_search);
        }

        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
        loaderView.Show();         
        InitLoaders(search_box.Text);

        FecthListBooks();
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

        private void InitLoaders(string name)
        {
            booksTaskLoader = new BooksTaskLoader(this,name);
            SupportLoaderManager.InitLoader(loaderId, null, this);
        }
            #endregion


            #region Events

            private void Books_ItemClick(object sender, BookItem item)
        {

            Log.Info("prueba", "se llama el elemento");

        }

        private void Search_Box_Click(object sender, System.EventArgs e)
        {
            var fragment = SearchDialogFragment.NewInstance(lasSearch, this);
            fragment.Show(SupportFragmentManager, nameof(SearchDialogFragment));
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

        public void OnClickItem(string value)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
