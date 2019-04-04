using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using PPE4.Model;

namespace PPE4
{
    [Activity]
    public class VisiteCRUD : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.visite_crud);
            Visite v = JsonConvert.DeserializeObject<Visite>(Intent.GetStringExtra("Visite"));
            System.Diagnostics.Debug.WriteLine("ID: " + v.id);
        }
    }
}