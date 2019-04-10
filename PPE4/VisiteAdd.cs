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
using Newtonsoft.Json.Linq;

namespace PPE4
{
    [Activity]
    public class VisiteAdd : AppCompatActivity
    {
        private Spinner visiteurs;
        private EditText date;
        private EditText compteRendu;
        private Spinner practiciens;

        private JObject json;

        private Button retour;
        private Button add;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.visite_add);

            //Recupération des Views
            visiteurs = (Spinner)FindViewById(Resource.Id.cbVisiteur);
            date = (EditText)FindViewById(Resource.Id.tbAddDate);
            compteRendu = (EditText)FindViewById(Resource.Id.tbAddCompteRendu);
            practiciens = (Spinner)FindViewById(Resource.Id.cbAddPracticien);

            //Récupération des parametres
            json = JObject.Parse(Intent.GetStringExtra("json"));

            //Affectation de valeurs au Views en fontions des parametres récupéré
            List<string> listVisiteurs = new List<string>();
            List<string> listPracticiens = new List<string>();
            for (int i = 0; i < json["visiteurs"].Count(); i++)
            {
                listVisiteurs.Add((string)json["visiteurs"][i]["id"] + " - " + (string)json["visiteurs"][i]["nom"]);
            }
            for (int i = 0; i < json["practiciens"].Count(); i++)
            {
                listPracticiens.Add((string)json["practiciens"][i]["nom"]);
            }
            var adapterVi = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, listVisiteurs);
            var adapterPr = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, listPracticiens);

            visiteurs.Adapter = adapterVi; 
            practiciens.Adapter = adapterPr;

            //Action bouton
            retour = (Button)FindViewById(Resource.Id.btnAddRetour);
            add = (Button)FindViewById(Resource.Id.btnAdd);

            retour.Click += delegate
            {
                Finish();
            };
            add.Click += delegate
            {

            };
        }
    }
}