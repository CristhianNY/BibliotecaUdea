using Android.Runtime;
using Android.Widget;
using Java.Lang;
using String = System.String;

namespace BibliotecaUdeA.Droid.Features.Lastfiveelements
{
    class FilterHelper : Filter
    {
        static JavaList<String> options;
        static SearchDialogAdapter adapter;

        public static FilterHelper NewInstance(JavaList<String> options, SearchDialogAdapter adapter)
        {
            FilterHelper.adapter = adapter;
            FilterHelper.options = options;
            return new FilterHelper();
        }

        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            FilterResults filterResults = new FilterResults();
            if (constraint != null && constraint.Length() > 0)
            {
                var query = constraint.ToString().ToUpper();

                var foundFilters = new JavaList<String>();

                for (int i = 0; i < options.Size(); i++)
                {
                    string option = options[i];

                    if (option.ToUpper().Contains(query))
                    {
                        foundFilters.Add(option);
                    }
                }
                filterResults.Count = foundFilters.Size();
                filterResults.Values = foundFilters;
            }
            else
            {
                filterResults.Count = options.Size();
                filterResults.Values = options;
            }

            return filterResults;
        }

        protected override void PublishResults(ICharSequence constraint, FilterResults results)
        {
            adapter.SetData((JavaList<String>)results.Values);
            adapter.NotifyDataSetChanged();
        }
    }
}
