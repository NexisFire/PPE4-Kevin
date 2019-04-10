using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PPE4.Model;
using System.Linq;

namespace PPE4
{
    [Activity]
    public class HomeDelegue : AppCompatActivity
    {
        private ListView lv;
        private VisiteAdapter va;
        private string strJson;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.home_delegue);

            lv = (ListView)FindViewById(Resource.Id.lv_visiteD);
            va = new VisiteAdapter(this, Resource.Layout.lvVisite);

            strJson = getJson();
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

            lv.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                Visite visit = va.GetItem(e.Position);
                Intent intent = new Intent(this, typeof(VisiteCRUD));
                intent.PutExtra("Visite", JsonConvert.SerializeObject(visit));
                intent.PutExtra("json", strJson);
                StartActivity(intent);
            };
        }

        private string getJson()
        {
            return @"{
	                    'visites': 
		                    [
                                {
                                    'id': 1,
				                    'date': '01/01/2010',
                                    'compteRendu': 'blablabla',
                                    'practicien': 'Jean Jacque'
                                }
                            ],
	                    'practiciens':
		                    [
			                    { 
				                    'nom':'Jean Jacque'
			                    },
                                { 
				                    'nom':'Jean Michel'
			                    },
                                { 
				                    'nom':'Ricardo Milos'
			                    }
		                    ],
                        'visiteurs':
                            [
                                {
                                    'id':1,
                                    'nom':'Dupond',
                                    'prenom':'Jean'
                                }
                            ]
                    }";
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
                    i.PutExtra("json", strJson);
                    StartActivity(i);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}