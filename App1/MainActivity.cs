using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using Android.Content.PM;

namespace App1
{
    public static class Ina
      {

        public static readonly double EuroDIESEL_BS = 10.42;
        public static readonly double Eurodizel = 10.51;
        public static readonly double Eurosuper_100 = 11.14 ;
        public static readonly double Eurosuper_95 = 10.60;
        public static readonly double EurodizelPlus = 10.51;
        public static readonly double Autoplin = 5.09;
        public static readonly double Loz_Ulje = 6.13;
    }

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        

        //view variable
        Button button;
        EditText text1;
        EditText text2;
        TextView textView;
        Spinner spinner;
        ListView mylistView;

        //data variable
        double cijenaBenzina = 0;
        List<KeyValuePair<string, double>> gorivo = new List<KeyValuePair<string, double>>
            {   new KeyValuePair<string, double> ("Eurodizel BS", Ina.EuroDIESEL_BS),
                new KeyValuePair<string, double>("Eurodizel",Ina.Eurodizel),
                new KeyValuePair<string, double>("Eurosuper 100",Ina.Eurosuper_100),
                new KeyValuePair<string, double>("Eurosuper 95",Ina.Eurosuper_95),
                new KeyValuePair<string, double>("EurodizelPlus", Ina.EurodizelPlus),
                new KeyValuePair<string, double>("Autoplin", Ina.Autoplin),
                new KeyValuePair<string, double>("Lož ulje", Ina.Loz_Ulje)
            };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            RequestedOrientation = ScreenOrientation.Portrait;
            SetContentView(Resource.Layout.activity_main);

            text1 = FindViewById<EditText>(Resource.Id.editText1);
            text2 = FindViewById<EditText>(Resource.Id.editText2);
            button = FindViewById<Button>(Resource.Id.button1);
            textView = FindViewById<TextView>(Resource.Id.textView1);
            spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            mylistView = FindViewById<ListView>(Resource.Id.listView);
            


            List<string> nameFluet = new List<string>();
            foreach (var item in gorivo)
            {
                nameFluet.Add(item.Key);
            }



            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner1_ItemSelected);
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, nameFluet);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            button.Click += OnNumberClick;

            //Add item to list
            MyListViewAdapter adapterListView = new MyListViewAdapter(this, gorivo);
            mylistView.Adapter = adapterListView;

        }

        private void OnNumberClick(object sender, System.EventArgs e)
        {
            double broj1 = 0;
            double broj2 = 0;
            

            try
            {
                //Validation for input
                if(string.IsNullOrWhiteSpace(text1.Text))
                {
                   text1.Error = "Please input value";
                         throw new ArgumentNullException("Please, Insert value");
                }
                else if (string.IsNullOrWhiteSpace(text2.Text))
                {
                   text2.Error = "Please input value";
                         throw new ArgumentNullException("Please, Insert value");
                 }

                if (!Regex.IsMatch(text1.Text, @"^[+-]?\d*\d+$|^[+-]?\d+(\,\d*)?$"))
                {
                    text1.Error = "Value must be  Numeric";
                        throw new ArgumentException("Input value must be DIGITAL number");
                }


                else if (!Regex.IsMatch(text2.Text, @"^[+-]?\d*\d+$|^[+-]?\d+(\,\d*)?$"))
                {
                    text2.Error = "Value must be  Numeric";
                       throw new ArgumentException("Input value must be DIGITAL number");
                }
            
            broj1 = double.Parse(text1.Text);
            broj2 = double.Parse(text2.Text);
            textView.Text = string.Format("{0:N2} HRK", ((broj1 / 100) * broj2) * cijenaBenzina);

            }

            catch (ArgumentNullException ex)
            {
                Toast.MakeText(this, "Dogodila se greška-> " + ex.Message, ToastLength.Long).Show();

            }

            catch (ArgumentException ex)
            {
                Toast.MakeText(this, "Dogodila se greška-> " + ex.Message, ToastLength.Long).Show();

            }

            catch (Exception ex)
            {
                Toast.MakeText(this, "Dogodila se greška-> " + ex.Message, ToastLength.Long).Show();

            }
        }

        private void Spinner1_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Cijena goriva po litri:  {0} je {1:N2} HKn",
            spinner.GetItemAtPosition(e.Position), gorivo[e.Position].Value);
            cijenaBenzina = gorivo[e.Position].Value;
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }


    }
}