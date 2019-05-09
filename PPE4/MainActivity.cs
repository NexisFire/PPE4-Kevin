using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using PPE4.SQLite;

namespace PPE4
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private DAODB dba;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.main);
            dba = new DAODB(this);
            dba.open();
            Button validerButton = (Button)this.FindViewById(Resource.Id.button_valid);
            validerButton.Click += delegate
            {
                string login = ((EditText)FindViewById(Resource.Id.txt_login)).Text;
                string password = ((EditText)FindViewById(Resource.Id.txt_password)).Text;
                dba.syncBase();
                dba.close();
                Intent i;
                i = new Intent(this, typeof(HomeDelegue));
                StartActivity(i);
            };
        }
    }
}