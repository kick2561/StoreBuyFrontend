using StoreBuy.DataService;
using StoreBuy.ViewModels.Catalog;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace StoreBuy.Views.Catalog
{

    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryTilePage
    {
        public CategoryTilePage()
        {
            InitializeComponent();
            BindingContext = new ItemCategoryPageViewModel(Navigation);
        }
        protected override void OnAppearing()
        {
            (this.BindingContext as ItemCategoryPageViewModel).GetCategories();
        }

        
    }
}