using StoreBuy.ViewModels.Forms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace StoreBuy.Views.Forms
{
   
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPasswordPage
    {
        
        public ResetPasswordPage()
        {
            InitializeComponent();            
            BindingContext = new ResetPasswordViewModel(Navigation);
        }

        public void CheckFields(object sender, EventArgs args)
        {
            var PasswordValidation = PasswordValidationEntry.IsVisible;
            var ConfirmValidation = ConfirmValidationEntry.IsVisible;

            var PasswordText = NewPassword.Text;
            var ConfirmPasswordText = ConfirmNewPassword.Text;

            if (
                string.IsNullOrEmpty(PasswordText) ||
                string.IsNullOrEmpty(ConfirmPasswordText) 
                )
            {
                DependencyService.Get<IMessage>().LongAlert("Fileds cannot be Empty");
            }
            else if (
                PasswordValidation ||
                ConfirmValidation)
            {
                DependencyService.Get<IMessage>().LongAlert("Please fill the fields in correct format");

            }

            else
            {
                (this.BindingContext as ResetPasswordViewModel).SubmitClicked();
            }
        }
        public string Email { get; }
    }
}