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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PPE4.Model;
using PPE4.SQLite;

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

        Button retour;
        Button save;
        Button delete;

        private DAODB dba;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.visite_crud);

            dba = new DAODB(BaseContext);
            dba.open();
            //Recupération des Views
            title = (TextView)FindViewById(Resource.Id.tvTitle);
            date = (EditText)FindViewById(Resource.Id.tbDate);
            compteRendu = (EditText)FindViewById(Resource.Id.tbCompteRendu);
            practiciens = (Spinner)FindViewById(Resource.Id.cbPracticien);

            //Récupération des parametres
            v = JsonConvert.DeserializeObject<Visite>(Intent.GetStringExtra("visite"));

            //Affectation de valeurs au Views en fontions des parametres récupéré
            List<string> listPracticiens = new List<string>();
            title.Text += v.id;
            date.Text = v.date;
            compteRendu.Text = v.compteRendu;
            int indice = 0;
            ICursor c = dba.getAllPracticien();

            if (c.MoveToFirst())
            {
                do
                {
                    string unPraticien = c.GetString(c.GetColumnIndex(C.PRACTICIENS_NOM)) + " " + c.GetString(c.GetColumnIndex(C.PRACTICIENS_PRENOM));
                    listPracticiens.Add(unPraticien);
                } while (c.MoveToNext());
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
                try
                {
                    updateVisiteDB();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            };

            delete.Click += delegate
            {
                try
                {
                    deleteVisiteDB();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            };
        }

        private void updateVisiteDB()
        {
            v.date = date.Text;
            v.compteRendu = compteRendu.Text;
            v.idPracticien = dba.getPracticienByName(practiciens.SelectedItem.ToString());
            dba.updateVisite(v);
            dba.close();
            date.Text = "";
            compteRendu.Text = "";
            practiciens.SetSelection(0);
            Intent intent = new Intent(this, typeof(HomeDelegue));
            StartActivity(intent);
        }

        private void deleteVisiteDB()
        {
            dba.delVisite(Convert.ToInt32(v.id));
            dba.close();
            Intent intent = new Intent(this, typeof(HomeDelegue));
            StartActivity(intent);
        }
    }
}