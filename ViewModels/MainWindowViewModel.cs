using System;
using System.Collections.Generic;
using System.Linq;
using IT_Project.ClassLibrary;
using IT_Project.Entity;

//Leah Oswald 7/20/2021

namespace IT_Project.ViewModels
{
    //Class that controls the logic behind the main window.
    public class MainWindowViewModel : ViewModelBase
    {

        private string _LoginMenuHeader = "Login";
        private User _CurrentUser = new User();
        private Database.Service_Request _Ticket = new Database.Service_Request();

        //Changes login button value.
        public string LoginMenuHeader
        {
            get { return _LoginMenuHeader; }
            set
            {
                _LoginMenuHeader = value;
                RaisePropertyChanged("LoginMenuHeader");
            }
        }

        //Gets user object. Also sets user object.
        public User CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                _CurrentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        //Get/sets service request object.
        public Database.Service_Request Ticket
        {
            get { return _Ticket; }
            set
            {
                _Ticket = value;
                RaisePropertyChanged("Ticket");
            }
        }

        //Method that retrieves all records that match current user from database and puts them into a list.
        public List<Database.Service_Request> LoadTechnicianData()
        {
            var serviceRequests = new List<Database.Service_Request>();

            string technicianName = CurrentUser.FirstName + " " + CurrentUser.LastName;

            using (var context = new Database.IT_DatabaseEntities())
            {
                serviceRequests = context.Service_Request.Where(sr => sr.technician_name.Equals(technicianName)).ToList();
            }
            return serviceRequests;

        }

        //Method that adds a note in the message column.
        public void AddMessageToTicket(string msg)
        {
            Ticket.message = string.Format("{0} \nMessage added {1: MM/dd/yyyy} \n{2}", Ticket.message, DateTime.Now, msg);

            using (var context = new Database.IT_DatabaseEntities())
            {
                var dbTicket = context.Service_Request.First(t => t.Id.Equals(Ticket.Id));
                context.Database.Log = Console.Write;
                Console.WriteLine(dbTicket.ToString());
                dbTicket.message = Ticket.message;
                context.SaveChanges();
            }
        }
    }
}
