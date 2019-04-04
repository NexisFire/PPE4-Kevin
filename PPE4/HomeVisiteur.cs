using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using PPE4.Model;

namespace PPE4
{
    [Activity]
    public class HomeVisiteur : AppCompatActivity
    {
        private ListView lv;
        private VisiteAdapter va;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.home_visiteur);

            lv = (ListView)FindViewById(Resource.Id.lv_visite);
            va = new VisiteAdapter(this, Resource.Layout.lvVisite);

            Visite v = new Visite();
            v.id = "1";
            v.date = "01/01/2010";
            v.compteRendu = "blablabla";
            v.practicien = "Jean Jacque";

            va.Add(v);
            lv.SetAdapter(va);
        }
    }
}