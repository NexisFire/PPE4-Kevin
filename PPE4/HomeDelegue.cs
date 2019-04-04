using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;
using PPE4.Model;

namespace PPE4
{
    [Activity]
    public class HomeDelegue : AppCompatActivity
    {
        private ListView lv;
        private VisiteAdapter va;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.home_delegue);

            lv = (ListView)FindViewById(Resource.Id.lv_visiteD);
            va = new VisiteAdapter(this, Resource.Layout.lvVisite);

            Visite v = new Visite();
            v.id = "1";
            v.date = "01/01/2010";
            v.compteRendu = "blablabla";
            v.practicien = "Jean Jacque";

            va.Add(v);
            lv.SetAdapter(va);

            lv.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                Visite visit = va.GetItem(e.Position);
                Intent intent = new Intent(this, typeof(VisiteCRUD));
                intent.PutExtra("Visite", JsonConvert.SerializeObject(visit));
                StartActivity(intent);
            };
        }
    }
}