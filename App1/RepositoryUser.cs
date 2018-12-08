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
using SQLite;
namespace App1
{
    #region RepositoryUser
    public class RepositoryUser
    {
        //   public static List<User> users = new List<User>() { new User { Username = "admin", Password = "admin" } };

        private static RepositoryUser _instance = null;
        private static readonly object padlock = new object(); 
        private  RepositoryUser() { }
        public static RepositoryUser Instance
        {

            get
            {

                if (_instance == null)
                {
                    lock (padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new RepositoryUser();
                        }
                    }
                }
                return _instance;
            }
        }







        static string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public static bool createDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Users.db")))
                {
                    connection.CreateTable<User>();

                    return true;
                }
            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }



        public  List<User> GetAllUsers()
        {

            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Users.db")))
                {
                    return connection.Table<User>().ToList();
                }
            }
            catch (SQLiteException ex)
            {

                return null;
            }


            //   return users;
        }

        public void AddUser(User user)
        {

            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Users.db")))
                {
                    connection.Insert(user);

                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex);
                return;
            }

            //  users.Add(user);
        }

        public static void DeleteUser(User user)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Users.db")))
                {
                    connection.Delete(user);

                }
            }
            catch (SQLiteException ex)
            {
                return;
            }
        }

        public User GetUserById(int Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Users.db")))
                {
                    return connection.Query<User>("SELECT * FROM User WHERE Id = ? ", Id).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
} 
#endregion