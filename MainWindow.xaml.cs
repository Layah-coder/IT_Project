using System;
using System.Windows;
using System.Windows.Controls;
using IT_Project.ClassLibrary;
using IT_Project.Entity;
using IT_Project.ViewModels;

//Leah Oswald 7/20/2021

namespace IT_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainWindowViewModel)this.Resources["viewModel"];
            Messanger.Instance.MessageRecieved += Instance_MessageRecieved;
        }

        // Receives messages that are being sent from view model
        private void Instance_MessageRecieved(object carrier, MessangerEventArguments e)
        {

            switch (e.MessageName)
            {
                case Messages.LOGIN_SUCCESS:
                    _viewModel.CurrentUser = (User)e.MessagePackage;
                    _viewModel.LoginMenuHeader = "Logout " + _viewModel.CurrentUser.FirstName;
                    PopulateHomeDataGrid();
                    break;

                case Messages.LOGOUT:
                    _viewModel.CurrentUser.IsLoggedIn = false;
                    _viewModel.LoginMenuHeader = "Login";

                    break;

                case Messages.CLOSE_USER_CONTROL:
                    CloseView();
                    break;
            }
        }

        private MainWindowViewModel _viewModel = null; 

        // Handles menu events.
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            switch (item.Tag.ToString())
            {
                // Checks to see if user is logged in.
                case "login":
                    if (_viewModel.CurrentUser.IsLoggedIn)
                    {
                        CloseView();
                        _viewModel.CurrentUser = new User();
                        _viewModel.LoginMenuHeader = "Login";
                        ClearDataGrid();
                    }
                    else
                    {
                        LoadView("IT_Project.UserControls.LoginControl");
                    }
                    break;
                    // Generates the report of all open tickets.
                case "genReport":
                    LoadView("IT_Project.UserControls.ReportControl");
                    break;
                    // Closes the program.
                case "exit":
                    this.Close();
                    break;
            }

        }

        private void PopulateHomeDataGrid() 
        {
            serviceRequestsDataGrid.ItemsSource = _viewModel.LoadTechnicianData();
        }

        private void ClearDataGrid()
        {
            serviceRequestsDataGrid.ItemsSource = null;
            
        }

        // Creates an instance of the user control.
        private void LoadView(string viewName)
        {
            Type ucType = null;
            UserControl uc = null;
            ucType = Type.GetType(viewName);
            if (ucType == null)
            {
                MessageBox.Show("The view: " + viewName + " does not exits.");
            }
            else
            {
                CloseView();

                uc = (UserControl)Activator.CreateInstance(ucType);
                if (uc != null)
                {
                    ShowView(uc);
                }
            }

        }

        private void ShowView(UserControl uc)
        {
            contentArea.Children.Add(uc);
        }

        private void CloseView()
        {
            contentArea.Children.Clear();
        }

        // Opens a ticket to view and modify details.
        private void openServiceTicketBtn_Click(object sender, RoutedEventArgs e)
        {
            // Ensures that selected values are assigned to the ticket object.
            try
            {
                if (!string.IsNullOrEmpty(_viewModel.Ticket.technician_name))
                {
                    TicketPopup.IsOpen = true;
                }
                else {
                    MessageBoxButton mBtn = MessageBoxButton.OK;
                    string mBoxMessage = "Please Make a Selection.";
                    string mBoxTitle = "Selection Error";
                    MessageBox.Show(mBoxMessage, mBoxTitle, mBtn);
                }
            }
            // Catches exception to avoid program from crashing.
            catch(NullReferenceException er)
            {
                Console.WriteLine(er);
            }
        }

        // When user selects a row, assign selected row data to ticket object.
        private void GetCells(object sender, SelectedCellsChangedEventArgs e)
        {
            _viewModel.Ticket = serviceRequestsDataGrid.SelectedItem as Database.Service_Request;
        }

        // Closes window if no changes are going to be made.
        private void CancelBtn_Click (object sender, RoutedEventArgs e)
        {
            TicketPopup.IsOpen = false;
            _viewModel.Ticket = new Database.Service_Request();
        }

        // Save button to update ticket message.
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddMessageToTicket(addMessage.Text);
            addMessage.Clear();
            TicketPopup.IsOpen = false;
            ReloadDataGrid();
        }

        //Reloads the data grid after changes have been saved.
        private void ReloadDataGrid()
        {
            ClearDataGrid();
            PopulateHomeDataGrid();
        }
    }
}
