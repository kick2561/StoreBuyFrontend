using StoreBuy.ViewModels.Forms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace StoreBuy.Views.Forms
{
    
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageViewModel(Navigation);
        }

        public void CheckFields(object sender, EventArgs args)
        {
            var EmailValidation = ValidationEmail.IsVisible;
            var EmailText = Email.Text;
            var PasswordText = PasswordEntry.Text;

            if(string.IsNullOrEmpty(PasswordText) ||string.IsNullOrEmpty(EmailText))
            {
                DependencyService.Get<IMessage>().LongAlert("Fileds cannot be Empty");

            }
            else if(EmailValidation)
            {
                DependencyService.Get<IMessage>().LongAlert("Please fill the fields in correct format");
            }
            else
            {
                (this.BindingContext as LoginPageViewModel).LoginClicked();
            }
        }
    }
}