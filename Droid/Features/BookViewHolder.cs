using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace BibliotecaUdeA.Droid.Features
{
    public class BookViewHolder : RecyclerView.ViewHolder
    {
        public ImageView imageBook;
        public TextView  title;
        public TextView  description;

        public BookViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            LoadViews(itemView);
            itemView.Click += (sender, e) => listener(LayoutPosition);
          
        }

        private void LoadViews(View itemView)
        {
            imageBook = itemView.FindViewById<ImageView>(Resource.Id.imageBook);
            title = itemView.FindViewById<TextView>(Resource.Id.title);
            description = itemView.FindViewById<TextView>(Resource.Id.description);
        }

    }
}
