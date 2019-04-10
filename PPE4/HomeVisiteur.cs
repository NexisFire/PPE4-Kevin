using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json.Linq;
using PPE4.Model;
using System.Linq;

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

            string strJson = getJson();
            JObject json = JObject.Parse(strJson);
            for (int i = 0; i < json["visites"].Count(); i++)
            {
                Visite visite = new Visite();
                visite.id = (string)json["visites"][i]["id"];
                visite.date = (string)json["visites"][i]["date"];
                visite.compteRendu = (string)json["visites"][i]["compteRendu"];
                visite.practicien = (string)json["visites"][i]["practicien"];
                va.Add(visite);
            }
            lv.SetAdapter(va);
        }

        private string getJson()
        {
            return @"{ 'visites': [
                        {
                            'id': 1,'date': '01/01/2010',
                            'compteRendu': 'blablabla',
                            'practicien': 'Jean Jacque'
                        }
                    ]}";
        }
    }
}