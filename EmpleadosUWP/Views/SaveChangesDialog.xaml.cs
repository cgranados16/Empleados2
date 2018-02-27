using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EmpleadosUWP.Views
{
    /// <summary>
    /// Creates a dialog that gives the users a chance to save changes, discard them, 
    /// or cancel the operation that trigggered the event. 
    /// </summary>
    public sealed partial class SaveChangesDialog : ContentDialog
    {
        public SaveChangesDialog(){
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the user's choice. 
        /// </summary>
        public SaveChangesDialogResult Result { get; set; } = SaveChangesDialogResult.Cancel;

        /// <summary>
        /// Gets or sets the display message.
        /// </summary>
        public string Message { get; set; } = "Tiene cambios que podrían perderse. " + 
            "¿Le gustaría guardar los cambios?"; 

        /// <summary>
        /// Fired when the user chooses to save. 
        /// </summary>
        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            Result = SaveChangesDialogResult.Save;
            Hide();
        }

        /// <summary>
        /// Fired when the user chooses to discard changes.
        /// </summary>
        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            Result = SaveChangesDialogResult.DontSave;
            Hide();
        }

        /// <summary>
        /// Fired when the user chooses to cancel the operation that triggered the event.
        /// </summary>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Result = SaveChangesDialogResult.Cancel;
            Hide();
        }
    }

    /// <summary>
    /// Defines the choices available to the user. 
    /// </summary>
    public enum SaveChangesDialogResult
    {
        Save,
        DontSave,
        Cancel
    }
}
