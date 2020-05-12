using Plugin.Settings;
using Plugin.Settings.Abstractions;
using StoreBuy.DataService;
using StoreBuy.ViewModels.Bookmarks;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace StoreBuy.Views.Bookmarks
{
    
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage
    {
         private static ISettings AppSettings =>
        CrossSettings.Current;

        public CartPage()
        {
            InitializeComponent();
            this.BindingContext = new CartPageViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            (this.BindingContext as CartPageViewModel).GetCartItems();
        }

    }
}