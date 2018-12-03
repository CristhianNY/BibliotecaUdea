using System.Collections.Generic;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views.InputMethods;
using Android.Widget;
using BibliotecaUdeA.Business.DependencyInjection;
using BibliotecaUdeA.Business.Dtos;
using BibliotecaUdeA.Droid.DependenctInjection;
using BibliotecaUdeA.Droid.Features.Lastfiveelements;
using BibliotecaUdeA.Droid.Features.Loaders;
using BibliotecaUdeA.Droid.Features.SharedPreferences;
using Java.Lang;
using Newtonsoft.Json;
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
        private Button BtnRecent;
        private List<BookItem> books;
        private BooksAdapter booksAdapter;
        private LoaderResponse<BaseResponse<BooksResponse>> loaderResponse;
        private DotsLoaderView loaderView;
        private Button btnSearch;
        private LinearLayout mainLayout;
        Android.Content.Context mContext = Android.App.Application.Context;
        AppPreferences preference;


        Android.Runtime.JavaList<string> lasSearch = new JavaList<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            LoadViews();
            preference = new AppPreferences(mContext);

            InjectDependencies();

            Window.SetSoftInputMode(Android.Views.SoftInput.StateAlwaysHidden);
            GetLastFiveSearch();
            btnSearch.Click += BtnSearch_Click;
            BtnRecent.Click += Search_Box_Click;
            InitLoaders();
                       
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
            BtnRecent = FindViewById<Button>(Resource.Id.btn_recent);
            mainLayout = FindViewById<LinearLayout>(Resource.Id.mainLayout);
        }

        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
        loaderView.Show();
         var name = search_box.Text;   
               
         var listInPreference=    preference.GetLastFiveNames();
            if (listInPreference != null)
            {
                lasSearch = listInPreference;

                if ((listInPreference.Count >= 0) && listInPreference.Count < 5)
                {
                    preference.SaveName(name);
                }
                else
                {
                    preference.UpdateList(name);
                }
            }
            else
            {
                preference.SaveName(name);
            }

            InputMethodManager imm = (InputMethodManager)GetSystemService(Android.Content.Context.InputMethodService);
            imm.HideSoftInputFromWindow(mainLayout.WindowToken, 0);
            FecthListBooks(name);
        }

        private void FecthListBooks(string name)
        {
          booksTaskLoader.SetBookName(name);
          booksTaskLoader.ForceLoad();
        }

        private void PrepareRecyclers(List<BookItem> b)
        {
            booksAdapter = new BooksAdapter(this, b);
            booksAdapter.ItemClick += Books_ItemClick;
            booksAdapter.NotifyDataSetChanged();
            rv_books.SetLayoutManager(new LinearLayoutManager(this));
            rv_books.HasFixedSize = true;
            rv_books.SetAdapter(booksAdapter);
        }

        private void InitLoaders()
        {
            booksTaskLoader = new BooksTaskLoader(this);
            SupportLoaderManager.InitLoader(loaderId, null, this);
        }
            #endregion


            #region Events

            private void Books_ItemClick(object sender, BookItem item)
        {
            Android.Content.Intent intent = new Android.Content.Intent(this, typeof(DetailsActivity));
            intent.PutExtra("Book", JsonConvert.SerializeObject(item));
            StartActivityForResult(intent, 1);
        }

        private void Search_Box_Click(object sender, System.EventArgs e)
        {
            if (preference.GetLastFiveNames() != null)
            {
                lasSearch = preference.GetLastFiveNames();
            }
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
                    PrepareRecyclers(books);
                   
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

            Toast.MakeText(this, "estamos buscando tu libro", ToastLength.Long).Show();
        }

        #endregion
    }
}
