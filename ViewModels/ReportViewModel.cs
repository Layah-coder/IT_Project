using IT_Project.ClassLibrary;
using System.Collections.Generic;
using System.Linq;

//Leah Oswald 7/20/2021

namespace IT_Project.ViewModels
{
    //Class to generate report of all open tickets.
    class ReportViewModel : ViewModelBase
    {
        public ReportViewModel() : base()
        {
        }

        //Method returns a list of all open tickets.
        public List<Database.Service_Request> LoadReportData()
        {
            var reportRequests = new List<Database.Service_Request>();
            using(var context = new Database.IT_DatabaseEntities())
            {
                reportRequests = context.Service_Request.Where(sr => sr.ticket_status.Equals("Open")).ToList();
            }
            return reportRequests;
        }
    }
}
