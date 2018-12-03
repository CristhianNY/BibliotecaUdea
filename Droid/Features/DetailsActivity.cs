
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
using Square.Picasso;

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
        private Button btnStore;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            boook = JsonConvert.DeserializeObject<BookItem>(Intent.GetStringExtra("Book"));
            SetContentView(Resource.Layout.Details);

            LoadViews();
            LoadBookInformation();
            btnStore.Click += BtnStore_Click;
        }

        private void LoadViews()
        {
            title = FindViewById<TextView>(Resource.Id.title);
            price = FindViewById<TextView>(Resource.Id.price);
            subtitle = FindViewById<TextView>(Resource.Id.subtitle);
            imagen = FindViewById<ImageView>(Resource.Id.imagen);
            btnStore = FindViewById<Button>(Resource.Id.btn_go_to_store);
           
        }

        private void LoadBookInformation()
        {
            title.Text = boook.Title;
            subtitle.Text = boook.SubTitle;
            price.Text = boook.Price;
            Picasso.With(this).Load(boook.Image).Into(imagen);
        }

        void BtnStore_Click(object sender, EventArgs e)
        {
            var uri = Android.Net.Uri.Parse(boook.Url);
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }

    }
}
