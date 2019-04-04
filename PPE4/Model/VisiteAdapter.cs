using Android.Content;
using Android.Views;
using Android.Widget;

namespace PPE4.Model
{
    public class VisiteAdapter : ArrayAdapter<Visite>
    {
        public VisiteAdapter(Context context, int textViewRessourceId) : base(context, textViewRessourceId) { }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View result = convertView;
            if (convertView == null)
            {
                result = LayoutInflater.From(Context).Inflate(Resource.Layout.lvVisite, parent, false);
            }
            Visite visite = GetItem(position);
            TextView id = (TextView)result.FindViewById(Resource.Id.idVisite);
            TextView date = (TextView)result.FindViewById(Resource.Id.dateVisite);
            TextView compteRendu = (TextView)result.FindViewById(Resource.Id.CRVisite);
            TextView practicien = (TextView)result.FindViewById(Resource.Id.practicienVisite);

            id.SetText(visite.id, null);
            date.SetText(visite.date, null);
            compteRendu.SetText(visite.compteRendu, null);
            practicien.SetText(visite.practicien, null);

            return result;
        }

        public void updateData()
        {
            NotifyDataSetChanged();
        }
    }
}