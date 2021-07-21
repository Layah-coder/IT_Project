using IT_Project.ViewModels;
using System.Windows;
using System.Windows.Controls;

//Leah Oswald 7/20/2021

namespace IT_Project.UserControls
{
    // Interaction logic for ReportControl.xaml

    public partial class ReportControl : UserControl
    {
        public ReportControl()
        {
            InitializeComponent();
            _viewModel = (ReportViewModel)this.Resources["viewModel"];
        }

        private ReportViewModel _viewModel = null;

        // Calls view model method to populate data grid.
        public void PopulateReportDataGrid()
        {
            
            reportDataGrid.ItemsSource = _viewModel.LoadReportData();
        }

        // Closes the view.
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Close();

        }
        
        // Calls a method to generate the report when xaml is loaded.
        private void LoadOnStartUp(object sender, RoutedEventArgs e)
        {
            PopulateReportDataGrid();
            

        }
    }
}
