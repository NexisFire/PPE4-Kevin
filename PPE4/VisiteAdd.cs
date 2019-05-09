using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;
using PPE4.SQLite;

namespace PPE4
{
    [Activity]
    public class VisiteAdd : AppCompatActivity
    {
        private EditText idVisiteur;
        private EditText date;
        private EditText compteRendu;
        private Spinner practiciens;

        private Button retour;
        private Button add;

        private DAODB dba;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.visite_add);
            dba = new DAODB(BaseContext);
            dba.open();

            //Recupération des Views
            idVisiteur = (EditText)FindViewById(Resource.Id.idVisiteur);
            date = (EditText)FindViewById(Resource.Id.tbAddDate);
            compteRendu = (EditText)FindViewById(Resource.Id.tbAddCompteRendu);
            practiciens = (Spinner)FindViewById(Resource.Id.cbAddPracticien);

            //Affectation de valeurs au Views en fontions des parametres récupéré
            List<string> listPracticiens = new List<string>();

            ICursor c = dba.getAllPracticien();

            if (c.MoveToFirst())
            {
                do
                {
                    string unPraticien = c.GetString(c.GetColumnIndex(C.PRACTICIENS_NOM)) + " " + c.GetString(c.GetColumnIndex(C.PRACTICIENS_PRENOM));
                    listPracticiens.Add(unPraticien);
                } while (c.MoveToNext());
            }

            var adapterPr = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, listPracticiens);

            practiciens.Adapter = adapterPr;

            //Action bouton
            retour = (Button)FindViewById(Resource.Id.btnAddRetour);
            add = (Button)FindViewById(Resource.Id.btnAdd);

            retour.Click += delegate
            {
                try
                {
                    Intent intent = new Intent(this, typeof(HomeDelegue));
                    StartActivity(intent);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            };
            add.Click += delegate
            {
                try
                {
                    sauvegarderVisiteDB();
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("AddClick: " + e.Message);
                    System.Diagnostics.Debug.WriteLine("AddClick: " + e.ToString());
                }
            };
        }

        private void sauvegarderVisiteDB()
        {
            System.Diagnostics.Debug.WriteLine(Convert.ToInt32(idVisiteur.Text));
            dba.insertVisit(Convert.ToInt32(idVisiteur.Text), date.Text, compteRendu.Text, dba.getPracticienByName(practiciens.SelectedItem.ToString()));
            dba.close();
            idVisiteur.Text = "";
            date.Text = "";
            compteRendu.Text = "";
            practiciens.SetSelection(0);
            Intent intent = new Intent(this, typeof(HomeDelegue));
            StartActivity(intent);
        }
    }
}