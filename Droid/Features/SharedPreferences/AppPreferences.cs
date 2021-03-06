﻿using System;
using System.Collections.Generic;
using Android.Content;
using Android.Preferences;
using Android.Runtime;

namespace BibliotecaUdeA.Droid.Features.SharedPreferences
{
    public class AppPreferences
    {
        private ISharedPreferences mSharedPrefs;
        private ISharedPreferencesEditor mPrefsEditor;
        private Context mContext;
        private int contador = 0;

        Android.Runtime.JavaList<string> lasSearch = new JavaList<string>();

        private static String PREFERENCE_ACCESS_KEY = "PREFERENCE_ACCESS_KEY";

        public AppPreferences(Context context)
        {
            this.mContext = context;
            mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            mPrefsEditor = mSharedPrefs.Edit();
        }

        public void SaveName(string nombre)
        {
           var lasSearchFive = GetLastFiveNames();
            if(lasSearchFive != null)
            {
                lasSearch = lasSearchFive;
                if (lasSearch.Count < 5)
                {
                    lasSearch.Add(nombre);
                }
                else
                {
                    UpdateList(nombre);
                }
            }
            else
            {
                lasSearch.Add(nombre);
            }


            SaveNames(lasSearch);
            
        }
        public void UpdateList(string nombre)
        {
            if (GetLastFiveNames() != null)
            {
                lasSearch = GetLastFiveNames();

            }
           
            if (lasSearch.Count > 0)
            {
              
                contador++;
                if(contador < 5)
                {
                 lasSearch[contador] = nombre;
                }
                else
                {
                    contador = 0;
                    lasSearch[contador] = nombre;
                }

                SaveNames(lasSearch);
            }
           

           

        }

        public Android.Runtime.JavaList<string> GetLastFiveNames()
        {
            var list = mSharedPrefs.GetString(PREFERENCE_ACCESS_KEY, "");
            Android.Runtime.JavaList<string> result =   Newtonsoft.Json.JsonConvert.DeserializeObject<Android.Runtime.JavaList<string>>(list);
            return result;
        }

        internal void SaveNames(JavaList<string> lasSearch)
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(lasSearch);

            mPrefsEditor.PutString(PREFERENCE_ACCESS_KEY, str);
            mPrefsEditor.Commit();
        }
    }
}
