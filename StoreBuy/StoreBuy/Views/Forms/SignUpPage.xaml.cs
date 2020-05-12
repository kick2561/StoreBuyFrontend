using StoreBuy.ViewModels.Forms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace StoreBuy.Views.Forms
{
   
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage
    {
        
        public SignUpPage()
        {
            InitializeComponent();
            BindingContext = new SignUpPageViewModel(Navigation);
        }

        public void CheckFields(object sender, EventArgs args)
        {
            var FirstName = FirstNameEntry.Text;
            var LastName = LastNameEntry.Text;
            var Phone = PhoneEntry.Text;
            var Password = PasswordEntry.Text;
            var ConfirmPassword = ConfirmPasswordEntry.Text;
            var Email =  EmailIdEntry.Text;

            var EmailValidation = EmailValidationEntry.IsVisible;
            var PhoneValidation = PhoneValidationEntry.IsVisible;
            var PasswordValidation = PasswordValidationEntry.IsVisible;
            var ConfirmValidation = ConfirmValidationEntry.IsVisible;

            if (string.IsNullOrEmpty(FirstName) ||
                string.IsNullOrEmpty(LastName) ||
                string.IsNullOrEmpty(Phone) ||
                string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(ConfirmPassword) ||
                string.IsNullOrEmpty(Email))
            {
                DependencyService.Get<IMessage>().LongAlert("Fileds cannot be Empty");
            }
            else if (EmailValidation ||
                PhoneValidation ||
                PasswordValidation ||
                ConfirmValidation)
            {
                DependencyService.Get<IMessage>().LongAlert("Please fill the fields in correct format");

            }

            else
            {
                (this.BindingContext as SignUpPageViewModel).SignUpClicked();
            }
        }
    }
}