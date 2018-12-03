using System;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace BibliotecaUdeA.Droid.Features.Lastfiveelements
{
    public class SearchDialogAdapter : RecyclerView.Adapter, IFilterable
    {

        readonly JavaList<String> options;
        readonly IOnInteractionSearchDialog listener;
        readonly SearchDialogFragment dialogFragment;
        JavaList<String> optionsFiltered;

        public SearchDialogAdapter(SearchDialogFragment dialogFragment, JavaList<String> options, IOnInteractionSearchDialog listener)
        {
            this.options = options;
            this.optionsFiltered = options;
            this.listener = listener;
            this.dialogFragment = dialogFragment;
        }

        public override int ItemCount
        {
            get { return optionsFiltered.Size(); }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var value = optionsFiltered[position];
            var viewHolder = holder as SearchDialogAdapterViewHolder;
            viewHolder.TextViewSearch.Text = value;
            viewHolder.dialogFragment = dialogFragment;
            viewHolder.listener = listener;
            viewHolder.value = value;
        }


        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent,
                                                                   int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_search_dialog, parent, false);
            var viewHolder = new SearchDialogAdapterViewHolder(itemView);
            return viewHolder;
        }

        public void SetData(JavaList<String> optionsFiltered)
        {
            this.optionsFiltered = optionsFiltered;
        }

        public Filter Filter
        {
            get { return FilterHelper.NewInstance(options, this); }
        }

        internal class SearchDialogAdapterViewHolder : RecyclerView.ViewHolder
        {

            public TextView TextViewSearch { get; set; }
            public string value;
            public IOnInteractionSearchDialog listener;
            public SearchDialogFragment dialogFragment;


            public SearchDialogAdapterViewHolder(View itemView) : base(itemView)
            {
                InitView(itemView);
            }

            void InitView(View itemView)
            {
                TextViewSearch = itemView.FindViewById<TextView>(Resource.Id.tvText);
                itemView.Click += ItemView_Click;
            }

            void ItemView_Click(object sender, EventArgs e)
            {
                if (listener != null)
                {
                    listener.OnClickItem(value);
                    dialogFragment.Dismiss();
                }
            }

        }

    }
}
