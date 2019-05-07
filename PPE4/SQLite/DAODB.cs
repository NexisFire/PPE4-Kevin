using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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

        public void close()
        {
            sqlitedb.Close();
        }

        public void open()
        {
            sqlitedb = dbHelper.WritableDatabase;
            if (sqlitedb == null) { sqlitedb = dbHelper.ReadableDatabase; }
        }

        public long insertVisit(string date, string compteRendu, string practicien)
        {
            try
            {
                ContentValues values = new ContentValues();
                values.Put(C.VISITES_DATE, date);
                values.Put(C.VISITES_COMPTERENDU, compteRendu);
                values.Put(C.VISITES_PRACTICIENS, practicien);
                return sqlitedb.Insert(C.TABLE_VISITES, null, values);
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
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
            ICursor c = sqlitedb.Query(C.TABLE_VISITES, new string[] { C.VISITES_KEY_ID, C.VISITES_DATE, C.VISITES_COMPTERENDU, C.VISITES_PRACTICIENS }, C.VISITES_KEY_ID + "=?", new string[] { id.ToString() }, null, null, null, null);
            Visite v = null;
            if (c!= null)
            {
                c.MoveToFirst();

                v = new Visite();
                v.id = c.GetString(0);
                v.date = c.GetString(1);
                v.compteRendu = c.GetString(2);
                v.practicien = c.GetString(3);
            }
            c.Close();
            sqlitedb.Close();
            return v;
        }

        public int updateVisite(Visite v)
        {
            int count = 0;
            ContentValues values = new ContentValues();
            values.Put(C.VISITES_DATE, v.date);
            values.Put(C.VISITES_COMPTERENDU, v.compteRendu);
            values.Put(C.VISITES_PRACTICIENS, v.practicien);
            count = sqlitedb.Update(C.TABLE_VISITES, values, C.VISITES_KEY_ID + "=?", new string[] { v.id });
            sqlitedb.Close();
            return count;
        }

        public int delVisite(int id)
        {
            int count = 0;
            count = sqlitedb.Delete(C.TABLE_VISITES, C.VISITES_KEY_ID + "=?", new string[] { id.ToString() });
            sqlitedb.Close();
            return count;
        }
    }
}