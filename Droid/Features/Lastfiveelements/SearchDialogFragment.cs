using System;
using System.Collections.Generic;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Lang;
using String = System.String;

namespace BibliotecaUdeA.Droid.Features.Lastfiveelements
{
    public class SearchDialogFragment : DialogFragment, ITextWatcher
    {

        private RecyclerView recyclerView;
        private EditText editText;
        private SearchDialogAdapter adapter;
        private RecyclerView.LayoutManager layoutManager;
        JavaList<String> itemData = new JavaList<String> { };
        IOnInteractionSearchDialog listener;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public static SearchDialogFragment NewInstance(JavaList<String> itemData, IOnInteractionSearchDialog listener)
        {
            SearchDialogFragment fragment = new SearchDialogFragment();
            fragment.itemData = itemData;
            fragment.listener = listener;
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.search_dialog_fragment, container, false);

            recyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerViewSearchResults);
            editText = rootView.FindViewById<EditText>(Resource.Id.editTextSearch);

            layoutManager = new LinearLayoutManager(Activity);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.AddItemDecoration(new DividerItemDecoration(Activity, LinearLayoutManager.Vertical));

            adapter = new SearchDialogAdapter(this, itemData, listener);
            recyclerView.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

            editText.AddTextChangedListener(this);

            return rootView;
        }

        void ITextWatcher.AfterTextChanged(IEditable s)
        {
            // Need Empty Method
        }

        void ITextWatcher.BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            // Need Empty Method
        }

        void ITextWatcher.OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            adapter.Filter.InvokeFilter(s);
        }

    }
}
