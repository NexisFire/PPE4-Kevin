using Android.Content;
using Android.Database;
using Android.Views;
using Android.Widget;
using Java.Lang;
using PPE4.SQLite;
using System.Collections.Generic;

namespace PPE4.Model
{
    public class VisiteAdapter : BaseAdapter
    {
        private LayoutInflater inflater;
        private List<Visite> visites;
        private DAODB dba;

        public VisiteAdapter(Context context, DAODB dba)
        {
            this.dba = dba;
            this.inflater = LayoutInflater.From(context);
            this.visites = new List<Visite>();
            getData();
        }

        public void getData()
        {
            ICursor c = dba.getAllVisit();

            if (c.MoveToFirst())
            {
                do
                {
                    Visite v = new Visite();
                    v.id = c.GetString(c.GetColumnIndex(C.VISITES_KEY_ID));
                    v.idVisiteur = c.GetInt(c.GetColumnIndex(C.VISITES_IDVISITEUR));
                    v.date = c.GetString(c.GetColumnIndex(C.VISITES_DATE));
                    v.compteRendu = c.GetString(c.GetColumnIndex(C.VISITES_COMPTERENDU));
                    v.idPracticien = c.GetInt(c.GetColumnIndex(C.VISITES_IDPRACTICIEN));
                    visites.Add(v);
                } while (c.MoveToNext());
            }
        }

        public override int Count { get { return visites.Count; } }

        public Visite GetItem(int position, bool b)
        {
            return visites[position];
        }

        public override Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = inflater.Inflate(Resource.Layout.lvVisite, null);
            }
            TextView id = (TextView)convertView.FindViewById(Resource.Id.idVisite);
            TextView idVisiteur = (TextView)convertView.FindViewById(Resource.Id.idVisiteurtxt);
            TextView date = (TextView)convertView.FindViewById(Resource.Id.dateVisite);
            TextView compteRendu = (TextView)convertView.FindViewById(Resource.Id.CRVisite);
            TextView practicien = (TextView)convertView.FindViewById(Resource.Id.practicienVisite);
            Visite v = GetItem(position, true);
            id.Text = "id: " + v.id;
            idVisiteur.Text = "idVisiteur: " + v.idVisiteur;
            date.Text = "date: " + v.date;
            compteRendu.Text = "compteRendu: " + v.compteRendu;
            practicien.Text = "Praticien: " + dba.getUnPracticien(v.idPracticien);
            return convertView;
        }
    }
}