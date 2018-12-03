
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BibliotecaUdeA.Business.Dtos;
using Newtonsoft.Json;

namespace BibliotecaUdeA.Droid.Features
{
    [Activity(Label = "Details")]
    public class DetailsActivity : Activity
    {
        private BookItem boook;
        private TextView title;
        private TextView price;
        private TextView subtitle;
        private ImageView imagen;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            boook = JsonConvert.DeserializeObject<BookItem>(Intent.GetStringExtra("Book"));
            SetContentView(Resource.Layout.Details);

            LoadViews();

        }

        private void LoadViews()
        {
            title = FindViewById<TextView>(Resource.Id.title);
            price = FindViewById<TextView>(Resource.Id.price);
            subtitle = FindViewById<TextView>(Resource.Id.subtitle);
            imagen = FindViewById<ImageView>(Resource.Id.imagen);
           
        }
    }
}
