using StoreBuy.Models;
using StoreBuy.ViewModels.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreBuy.Views.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmailVerificationPage : ContentPage
    {
        public EmailVerificationPage()
        {
            InitializeComponent();
            BindingContext = new EmailVerificationViewModel(Navigation);
        }
    }
}