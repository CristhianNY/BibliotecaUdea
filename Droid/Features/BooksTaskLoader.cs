using System;
using Android.Content;
using Java.Lang;

namespace BibliotecaUdeA.Droid.Features
{
    public class BooksTaskLoader : Android.Support.V4.Content.AsyncTaskLoader
    {
        public BooksTaskLoader(Context context, string name) : base(context)
        {
        }

        public override Java.Lang.Object LoadInBackground()
        {
            throw new NotImplementedException();
        }
    }
}
