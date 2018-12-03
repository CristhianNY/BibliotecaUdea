using System;
using Android.Content;
using BibliotecaUdeA.Business.Dtos;
using BibliotecaUdeA.Business.Managers;
using BibliotecaUdeA.Droid.Features.Loaders;
using Java.Lang;

namespace BibliotecaUdeA.Droid.Features
{
    public class BooksTaskLoader : Android.Support.V4.Content.AsyncTaskLoader
    {
        private BooksManager booksManager;
        private string name;

        public BooksTaskLoader(Context context) : base(context)
        {
            booksManager = new BooksManager();
        }
        public void SetBookName(string name)
        {
            this.name = name;
        }
        public override Java.Lang.Object LoadInBackground()
        {
            var response = booksManager.FetchBooksByName(name);
            return new LoaderResponse<BaseResponse<BooksResponse>>(response);
        }
        protected override void OnReset()
        {
            CancelLoadInBackground();
        }
        protected override void OnStopLoading()
        {
            CancelLoadInBackground();
        }
    }
}
