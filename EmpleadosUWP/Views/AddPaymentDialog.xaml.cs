//  ---------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
// 
//  The MIT License (MIT)
// 
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
// 
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
// 
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//  ---------------------------------------------------------------------------------

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EmpleadosUWP.Views
{
    /// <summary>
    /// Creates a dialog that gives the users a chance to save changes, discard them, 
    /// or cancel the operation that trigggered the event. 
    /// </summary>
    public sealed partial class AddPaymentDialog : ContentDialog
    {
        public AddPaymentDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the user's choice. 
        /// </summary>
        public AddPaymentDialogResult Result { get; set; } = AddPaymentDialogResult.Cancel;

        /// <summary>
        /// Gets or sets the display message.
        /// </summary>
        public double Monto { get; set; }

        /// <summary>
        /// Fired when the user chooses to save. 
        /// </summary>
        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            if (Monto > 0){
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
