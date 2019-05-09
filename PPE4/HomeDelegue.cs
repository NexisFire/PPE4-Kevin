using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PPE4.Model;
using PPE4.SQLite;
using System;
using System.Linq;

namespace PPE4
{
    [Activity]
    public class HomeDelegue : AppCompatActivity
    {
        private ListView lv;
        private VisiteAdapter va;
        private DAODB dba;
        private string strJson;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            dba = new DAODB(this);
            dba.open();
            SetContentView(Resource.Layout.home_delegue);

            base.OnCreate(savedInstanceState);
            lv = (ListView)FindViewById(Resource.Id.lv_visiteD);
            va = new VisiteAdapter(this, dba);
            lv.SetAdapter(va);

            lv.ItemClick += (parent, args) =>
            {
                try
                {
                    Visite visite = va.GetItem(args.Position, true);
                    Intent intent = new Intent(this, typeof(VisiteCRUD));
                    intent.PutExtra("visite", JsonConvert.SerializeObject(visite));
                    StartActivity(intent);
                }
                catch(Exception e){
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_add, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_add:
                    Intent i = new Intent(this, typeof(VisiteAdd));
                    StartActivity(i);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}