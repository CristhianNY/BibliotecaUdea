using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Content;
using System.Collections.Generic;
using BibliotecaUdeA.Business.Dtos;
using System.Linq;

namespace BibliotecaUdeA.Droid.Features
{
    public class BooksAdapter : RecyclerView.Adapter
    {
        private readonly Context context;
        private readonly List<BookItem> items;
        public event EventHandler<BookItem> ItemClick;

        public BooksAdapter(Context context, List<BookItem> items)
        {
            this.context = context;
            this.items = items;
        }

        public override int ItemCount => items.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var currentItem = items[position];
            var viewHolder = holder as BookViewHolder;
            viewHolder.title.Text = currentItem.Title;
            viewHolder.description.Text = currentItem.SubTitle;
            Android.Net.Uri url = Android.Net.Uri.Parse(currentItem.Image);
            viewHolder.imageBook.SetImageURI(url);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemViewText = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Item, parent, false);
            return new BookViewHolder(itemViewText, OnClick);
        }

        private void OnClick(int position)
        {
            ItemClick(this, items.ElementAt(position));
        }
    }
}
