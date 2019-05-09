using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;
using PPE4.Model;

namespace PPE4.SQLite
{
    public class DAODB
    {
        private SQLiteDatabase sqlitedb;
        private readonly Context context;
        private readonly GestionDBHelper dbHelper;

        public DAODB(Context c)
        {
            context = c;
            dbHelper = new GestionDBHelper(context, C.DATABASE_NAME, null, C.DATABASE_VERSION);
        }

        public void syncBase()
        {
            sqlitedb.ExecSQL("DELETE FROM " + C.TABLE_VISITES);
            sqlitedb.ExecSQL("DELETE FROM " + C.TABLE_PRACTICIENS);

            JObject json = null;
            using (WebClient client = new WebClient())
            {
                string jsonString = client.DownloadString("http://kevin.batobleu.xyz/PPE3/ws.php?action=syncDataBase");
                json = JObject.Parse(jsonString);
            }

            foreach(JToken uneVisite in json["Visites"])
            {
                Visite v = new Visite();
                v.id = uneVisite["IDVISITE"].ToString();
                v.idVisiteur = Convert.ToInt32(uneVisite["IDVISITEUR"].ToString());
                v.date = uneVisite["DATEVISITE"].ToString();
                v.compteRendu = uneVisite["MOTIF"].ToString();
                v.idPracticien = Convert.ToInt32(uneVisite["IDPRATICIEN"].ToString());
                System.Diagnostics.Debug.WriteLine("Insert visite");
                insertVisit(v);
            }

            foreach (JToken unPraticien in json["Practiciens"])
            {
                int idPraticien = Convert.ToInt32(unPraticien["IDPRATICIEN"].ToString());
                string nom = unPraticien["NOM"].ToString();
                string prenom = unPraticien["PRENOM"].ToString();
                System.Diagnostics.Debug.WriteLine("Insert Praticien");
                insertPracticien(idPraticien, nom, prenom);
            }
        }

        public void close()
        {
            sqlitedb.Close();
        }

        public void open()
        {
            sqlitedb = dbHelper.WritableDatabase;
            if (sqlitedb == null) { sqlitedb = dbHelper.ReadableDatabase; }
        }

        public long insertVisit(Visite v)
        {
            try
            {
                ContentValues values = new ContentValues();
                values.Put(C.VISITES_KEY_ID, v.id);
                values.Put(C.VISITES_DATE, v.date);
                values.Put(C.VISITES_COMPTERENDU, v.compteRendu);
                values.Put(C.VISITES_IDPRACTICIEN, v.idPracticien);
                values.Put(C.VISITES_IDVISITEUR, v.idVisiteur);
                return sqlitedb.Insert(C.TABLE_VISITES, null, values);
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine("Insert: " + ex.Message);
                return -1;
            }
        }

        public long insertVisit(int visiteur, string date, string compteRendu, int practicien)
        {
            try
            {
                ContentValues values = new ContentValues();
                values.Put(C.VISITES_DATE, date);
                values.Put(C.VISITES_COMPTERENDU, compteRendu);
                values.Put(C.VISITES_IDPRACTICIEN, practicien);
                values.Put(C.VISITES_IDVISITEUR, visiteur);
                return sqlitedb.Insert(C.TABLE_VISITES, null, values);
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine("Insert: " + ex.Message);
                return -1;
            }
        }

        public ICursor getAllVisit()
        {
            ICursor c = sqlitedb.Query(C.TABLE_VISITES, null, null, null, null, null, null);
            return c;
        }

        public Visite getUneVisite(int id)
        {
            ICursor c = sqlitedb.Query(C.TABLE_VISITES, new string[] { C.VISITES_KEY_ID, C.VISITES_DATE, C.VISITES_COMPTERENDU, C.VISITES_IDPRACTICIEN, C.VISITES_IDVISITEUR }, C.VISITES_KEY_ID + "=?", new string[] { id.ToString() }, null, null, null, null);
            Visite v = null;
            if (c!= null)
            {
                c.MoveToFirst();

                v = new Visite();
                v.id = c.GetString(0);
                v.date = c.GetString(1);
                v.compteRendu = c.GetString(2);
                v.idPracticien = c.GetInt(3);
                v.idVisiteur = c.GetInt(4);
            }
            c.Close();
            return v;
        }

        public int updateVisite(Visite v)
        {
            int count = 0;
            ContentValues values = new ContentValues();
            values.Put(C.VISITES_DATE, v.date);
            values.Put(C.VISITES_COMPTERENDU, v.compteRendu);
            values.Put(C.VISITES_IDPRACTICIEN, v.idPracticien);
            values.Put(C.VISITES_IDVISITEUR, v.idVisiteur);
            count = sqlitedb.Update(C.TABLE_VISITES, values, C.VISITES_KEY_ID + "=?", new string[] { v.id });
            return count;
        }

        public int delVisite(int id)
        {
            int count = 0;
            count = sqlitedb.Delete(C.TABLE_VISITES, C.VISITES_KEY_ID + "=?", new string[] { id.ToString() });
            return count;
        }

        public long insertPracticien(int id, string nom, string prenom)
        {
            try
            {
                ContentValues values = new ContentValues();
                values.Put(C.PRACTICIENS_KEY_ID, id);
                values.Put(C.PRACTICIENS_NOM, nom);
                values.Put(C.PRACTICIENS_PRENOM, prenom);
                return sqlitedb.Insert(C.TABLE_PRACTICIENS, null, values);
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return -1;
            }
        }

        public ICursor getAllPracticien()
        {
            ICursor c = sqlitedb.Query(C.TABLE_PRACTICIENS, null, null, null, null, null, null);
            return c;
        }

        public string getUnPracticien(int id)
        {
            ICursor c = sqlitedb.Query(C.TABLE_PRACTICIENS, new string[] { C.PRACTICIENS_NOM, C.PRACTICIENS_PRENOM }, C.PRACTICIENS_KEY_ID + "=?", new string[] { id.ToString() }, null, null, null, null);
            string value = null;
            if (c != null)
            {
                c.MoveToFirst();
                value = c.GetString(0) + " " + c.GetString(1);
            }
            c.Close();
            return value;
        }

        public int getPracticienByName(string practicien)
        {
            int indice = practicien.IndexOf(' ');
            string nom = practicien.Substring(0, indice);
            string prenom = practicien.Substring(indice + 1);
            System.Diagnostics.Debug.WriteLine("pNom: " + nom + " pPrenom: " + prenom);

            ICursor c = sqlitedb.Query(C.TABLE_PRACTICIENS, new string[] { C.PRACTICIENS_KEY_ID }, C.PRACTICIENS_NOM + "=? AND " + C.PRACTICIENS_PRENOM + "=?", new string[] { nom, prenom }, null, null, null, null);
            int value = -1;
            if (c != null)
            {
                c.MoveToFirst();
                value = c.GetInt(0);
            }
            c.Close();
            return value;
        }
    }
}