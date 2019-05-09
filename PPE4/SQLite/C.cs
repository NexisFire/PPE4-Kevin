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

namespace PPE4.SQLite
{
    public class C
    {
        public static readonly string DATABASE_NAME = "PPE4";
        public static int DATABASE_VERSION=1;

        //Table visites
        public static readonly string TABLE_VISITES="visites";
        public static readonly string VISITES_IDPRACTICIEN = "idPracticien";
        public static readonly string VISITES_IDVISITEUR = "idVisiteur";
        public static readonly string VISITES_COMPTERENDU = "compteRendu";
        public static readonly string VISITES_DATE = "dateVisite";
        public static readonly string VISITES_KEY_ID = "_id";

        //Table practiciens
        public static readonly string TABLE_PRACTICIENS = "practiciens";
        public static readonly string PRACTICIENS_KEY_ID = "_id";
        public static readonly string PRACTICIENS_NOM = "nom";
        public static readonly string PRACTICIENS_PRENOM = "prenom";
    }
}