using StoreBuy.ViewModels.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreBuy.Views.Catalog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchItemsListPage : ContentPage
    {
		SearchItemsPageViewModel SearchItemViewModel = null;
		public SearchItemsListPage()
		{
			InitializeComponent();
			SearchItemViewModel = new SearchItemsPageViewModel(Navigation);
			BindingContext = SearchItemViewModel;

		}
		void OnTextChanged(object sender, EventArgs e)
		{
			SearchBar searchBar = (SearchBar)sender;
			SearchItemViewModel.GetItems(searchBar.Text);
		}
		protected override void OnAppearing()
		{
			SearchItemViewModel.SetCartCount();
		}
	}
}