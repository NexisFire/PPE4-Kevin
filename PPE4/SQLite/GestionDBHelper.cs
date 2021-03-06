﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PPE4.SQLite
{
    public class GestionDBHelper : SQLiteOpenHelper
    {
        private static readonly string CREATE_TABLE_VISITE ="create table " + C.TABLE_VISITES + " (" + C.VISITES_KEY_ID + " integer primary key autoincrement, " + C.VISITES_IDPRACTICIEN +" int not null, " + C.VISITES_IDVISITEUR + " int not null, " + C.VISITES_COMPTERENDU+" text not null, " +  C.VISITES_DATE + " date);";
        private static readonly string CREATE_TABLE_PRACTICIEN = "create table " + C.TABLE_PRACTICIENS + " (" + C.PRACTICIENS_KEY_ID + " integer primary key autoincrement, " + C.PRACTICIENS_NOM + " text not null, " + C.PRACTICIENS_PRENOM + " text not null);";
       
        public GestionDBHelper(Context context, string name, SQLiteDatabase.ICursorFactory factory, int version) : base (context, name, factory, version)
        {

        }

        public override void OnCreate(SQLiteDatabase db)
        {
            try
            {
                db.ExecSQL(CREATE_TABLE_VISITE);
                db.ExecSQL(CREATE_TABLE_PRACTICIEN);
            }
            catch(SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("DROP TABLE IF EXISTS " + C.TABLE_VISITES);
            db.ExecSQL("DROP TABLE IF EXISTS " + C.TABLE_PRACTICIENS);
            OnCreate(db);
        }
    }
}