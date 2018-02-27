using EmpleadosUWP.ViewModels;
using Windows.UI.Xaml.Controls;

namespace EmpleadosUWP.Views
{
    public sealed partial class EmployeesGridPage : Page
    {
        public EmployeesGridViewModel ViewModel { get; } = new EmployeesGridViewModel();

        public EmployeesGridPage(){ 
            InitializeComponent();
        }
        
        private void ViewDetails_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e){
            if (ViewModel.SelectedEmployee == null) return;
            GoToDetailsPage(ViewModel.SelectedEmployee);
        }

        private void ViewDetails_Click(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e){
            if (ViewModel.SelectedEmployee == null) return;
            GoToDetailsPage(ViewModel.SelectedEmployee);
        }

        private void AddEmployee_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e){
            GoToDetailsPage(null);
        }

        /// <summary>
        ///  Loads the specified employee in the details page. 
        /// </summary>
        private void GoToDetailsPage(EmployeeViewModel employee) =>
            Frame.Navigate(typeof(EmployeeDetailsPage), employee);
    }
}
