using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PPE4.Model
{
    public class Visite
    {
        public string id { get; set; }
        public int idVisiteur { get; set; }
        public string date { get; set; }
        public string compteRendu { get; set; }
        public int idPracticien { get; set; }
    }
}