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
using Newtonsoft.Json.Linq;
using PPE4.Model;

namespace PPE4
{
    [Activity]
    public class VisiteCRUD : AppCompatActivity
    {
        TextView title;
        EditText date;
        EditText compteRendu;
        Spinner practiciens;

        Visite v;
        JObject json;

        Button retour;
        Button save;
        Button delete;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.visite_crud);

            //Recupération des Views
            title = (TextView)FindViewById(Resource.Id.tvTitle);
            date = (EditText)FindViewById(Resource.Id.tbDate);
            compteRendu = (EditText)FindViewById(Resource.Id.tbCompteRendu);
            practiciens = (Spinner)FindViewById(Resource.Id.cbPracticien);

            //Récupération des parametres
            v = JsonConvert.DeserializeObject<Visite>(Intent.GetStringExtra("Visite"));
            json = JObject.Parse(Intent.GetStringExtra("json"));

            //Affectation de valeurs au Views en fontions des parametres récupéré
            List<string> listPracticiens = new List<string>();
            title.Text += v.id;
            date.Text = v.date;
            compteRendu.Text = v.compteRendu;
            int indice = 0;
            listPracticiens.Add(" ");
            for (int i = 0; i < json["practiciens"].Count(); i++)
            {
                listPracticiens.Add((string)json["practiciens"][i]["nom"]);
                if ((string)json["practiciens"][i]["nom"] == v.practicien)
                {
                    indice = i + 1;
                }
            }
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, listPracticiens);

            practiciens.Adapter = adapter;
            practiciens.SetSelection(indice);

            //Action bouton
            retour = (Button)FindViewById(Resource.Id.btnRetour);
            save = (Button)FindViewById(Resource.Id.btnSave);
            delete = (Button)FindViewById(Resource.Id.btnDel);

            retour.Click += delegate
            {
                Finish();
            };

            save.Click += delegate 
            {

            };

            delete.Click += delegate
            {

            };
        }
    }
}