using System;
using System.Diagnostics;
using Empleados.Models;
using EmpleadosUWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EmpleadosUWP.Views
{
    public sealed partial class EmployeesGridPage : Page
    {
        public EmployeesGridViewModel ViewModel { get; } = new EmployeesGridViewModel();

        // TODO WTS: Change the grid as appropriate to your app.
        // For help see http://docs.telerik.com/windows-universal/controls/raddatagrid/gettingstarted
        // You may also want to extend the grid to work with the RadDataForm http://docs.telerik.com/windows-universal/controls/raddataform/dataform-gettingstarted
        public EmployeesGridPage()
        {
            
            InitializeComponent();

        }

        
        private void ViewDetails_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ViewModel.SelectedEmployee == null) return;
            GoToDetailsPage(ViewModel.SelectedEmployee);
        }

        private void ViewDetails_Click(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            if (ViewModel.SelectedEmployee == null) return;
            GoToDetailsPage(ViewModel.SelectedEmployee);
        }

        private void AddEmployee_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GoToDetailsPage(null);
        }


        /// <summary>
        ///  Loads the specified order in the order details page. 
        /// </summary>
        private void GoToDetailsPage(EmployeeViewModel employee) =>
            Frame.Navigate(typeof(EmployeeDetailsPage), employee);
    }
}
