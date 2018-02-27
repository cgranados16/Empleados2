using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EmpleadosUWP.Views
{
    /// <summary>
    /// Creates a dialog that gives the users a chance to add payments to an employee or cancel it.
    /// </summary>
    public sealed partial class AddPaymentDialog : ContentDialog
    {
        public AddPaymentDialog(){
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the user's choice. 
        /// </summary>
        public AddPaymentDialogResult Result { get; set; } = AddPaymentDialogResult.Cancel;

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Fired when the user chooses to save. 
        /// </summary>
        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            if (Amount > 0){
                Result = AddPaymentDialogResult.Accept;
                Hide();
            }
        }

        /// <summary>
        /// Fired when the user chooses to cancel the operation that triggered the event.
        /// </summary>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Result = AddPaymentDialogResult.Cancel;
            Hide();
        }
    }

    /// <summary>
    /// Defines the choices available to the user. 
    /// </summary>
    public enum AddPaymentDialogResult
    {
        Accept,
        Cancel
    }
}
