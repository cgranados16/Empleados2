using System;

using EmpleadosUWP.ViewModels;

using Windows.UI.Xaml.Controls;

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
            this.Frame.Navigate(typeof(EmployeeDetailsPage));
        }
    }
}
