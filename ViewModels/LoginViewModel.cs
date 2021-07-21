using IT_Project.ClassLibrary;
using IT_Project.Entity;
using System;
using System.Data.Common;
using System.Linq;
using System.Windows;

//Leah Oswald 7/20/2021

namespace IT_Project.ViewModels
{
    //Class that controls logic behind login window.
    public class LoginViewModel : ViewModelBase
    {
        #region LoginViewModel Constructor
        public LoginViewModel() : base()
        {
            //Create new user.
            CurrentUser = new User { UserName = Environment.UserName };
        }
        #endregion

        //Message box text to notify user of login issues.
        #region Login Error MessageBox
        MessageBoxButton mBtn = MessageBoxButton.OK;
        string mBoxMessage = "Invalid username or password.";
        string mBoxTitle = "Login Error";
        #endregion

        #region User Object 
        private User _CurrentUser;
        public User CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                _CurrentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }
        #endregion

        #region Login method 
        public void Login()
        {
            
            //Call method to verify user login.
            if (VerifyUserCredentials(CurrentUser.UserName, CurrentUser.Password))
            {

                Messanger.Instance.SendMessage(Messages.LOGIN_SUCCESS, CurrentUser);

                Close(false);
            }
            //Display possible login errors when credentials do not match.
            else 
            {
                _ = MessageBox.Show(mBoxMessage, mBoxTitle, mBtn);
            }

        }
        #endregion

        //Method that verifies username and password with the database.
        #region Verify User Credentials
        public bool VerifyUserCredentials(string username, string password)
        {
            bool ret = false;
            //Attempt to access database.
            try
            {
                using (Database.IT_DatabaseEntities context = new Database.IT_DatabaseEntities())
                {
                    Database.User DbUser = context.Users.FirstOrDefault(u => u.Username == username);
                    if (DbUser != null && (DbUser.Password.Equals(password) && DbUser.Username.Equals(username)))
                    {
                        //Set new user to login data.
                        CurrentUser = new User(DbUser.ID, DbUser.Username, DbUser.Password, DbUser.Firstname, DbUser.Lastname, true);

                        ret = true;
                    }
                    return ret;
                }
            }
            //Exception thrown if not able to access database.
            catch (DbException ex)
            {
                Console.WriteLine("Exception thrown: " + ex);
            }


            return ret;
        }
        #endregion



    }
}
