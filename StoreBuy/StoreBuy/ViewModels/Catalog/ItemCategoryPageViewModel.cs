using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using StoreBuy.Models;
using StoreBuy.Views.Catalog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace StoreBuy.ViewModels.Catalog
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ItemCategoryPageViewModel : BaseViewModel
    {
       
        private static ISettings AppSettings =>
        CrossSettings.Current;
        private ObservableCollection<ItemCategory> categories;
        private Command categorySelectedCommand;
        private Command searchCommand;
        private Command backButtonCommand;
        private INavigation Navigation;

        public ItemCategoryPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }

        public async void GetCategories()
        {
            using (var client = new HttpClient())
            {
                var uri = Resources.APIPrefix+Resources.GetCategoryItems;
                var response = await client.GetStringAsync(uri);
                var CategoryList = JsonConvert.DeserializeObject<List<ItemCategory>>(response);
                Categories = new ObservableCollection<ItemCategory>(CategoryList);
            }
        }

        [DataMember(Name = "categories")]
        public ObservableCollection<ItemCategory> Categories
        {
            get { return this.categories; }
            set
            {
                if (this.categories == value)
                {
                    return;
                }

                this.categories = value;
                this.NotifyPropertyChanged();
            }
        }


        public Command CategorySelectedCommand
        {
            get { return categorySelectedCommand ?? (categorySelectedCommand = new Command(CategorySelected)); }
        }


        public Command SearchCommand
        {
            get { return searchCommand ?? (searchCommand = new Command(this.SearchClicked)); }
        }

        private async void CategorySelected(object obj)
        {
            ItemCategory ItemCategory = (ItemCategory)obj;
            
            AppSettings.AddOrUpdateValue("CategoryId", ItemCategory.CategoryId.ToString());
            await Navigation.PushAsync(new CatalogListPage());
        }

        private async void SearchClicked(object obj)
        {
            await Navigation.PushAsync(new SearchItemsListPage());
        }

    }
}

