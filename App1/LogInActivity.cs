using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
    [Activity(Label = "LogIn", MainLauncher = true)]
    public class LogInActivity : Activity
    {

     
        
        

        TextView textView;
        EditText username;
        EditText password;
        Button buttonLogIn;
        Button buttonRegister;

        protected override void OnCreate(Bundle savedInstanceState)
        {                    
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);

            RepositoryUser.createDatabase();

            username = FindViewById<EditText>(Resource.Id.editUser);
            password = FindViewById<EditText>(Resource.Id.editPassword);
            buttonLogIn = FindViewById<Button>(Resource.Id.buttonLogIn);
            buttonRegister = FindViewById<Button>(Resource.Id.buttonRegister);
            textView = FindViewById<TextView>(Resource.Id.textError);
            //Password view
            password.InputType = Android.Text.InputTypes.TextVariationPassword |
                          Android.Text.InputTypes.ClassText;

           
            //LogIn buttuon
            buttonLogIn.Click += CheckUser;
            // Register button
            buttonRegister.Click += RegisterUser;

           User u = RepositoryUser.Instance.GetUserById(1);
        }
            
         
        private void CheckUser(object sender, EventArgs e)
        {
            bool haveUser = true;
            haveUser = DoesExistUser(username: username.Text,password: password.Text);

            if (haveUser)
            {
                textView.SetTextColor(Android.Graphics.Color.Cyan);
                textView.Text = "Ok";
                Intent nextActivity = new Intent(this, typeof(MainActivity));
                StartActivity(nextActivity);

            }
            else
            {
                textView.Text = "Username or Password is not correct";

            }
        }

        private void RegisterUser(object sender, EventArgs e)
        {
            if (!DoesExistUser(username: username.Text.Trim(), password: password.Text.Trim()))
            {
                User user = new User();
                user.Username = username.Text.Trim();
                user.Password = password.Text.Trim();
                RepositoryUser.Instance.AddUser(user);
                textView.SetTextColor(Android.Graphics.Color.Cyan);
                textView.Text = "Thank you for register";
            }
            else
            {
                textView.SetTextColor(Android.Graphics.Color.Crimson);
                textView.Text = "Username and password is already exists";
            }
        }

        private bool DoesExistUser(string username, string password )
        {
            return RepositoryUser.Instance.GetAllUsers().Any(u => u.Username == username && u.Password == password);

          
        }

       

    }
}