using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

using StoreBuy.Models;
using StoreBuy.Views;
using StoreBuy.Views.Bookmarks;
using StoreBuy.Views.Catalog;
using StoreBuy.Views.ErrorAndEmpty;
using StoreBuy.Views.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StoreBuy.ViewModels.Catalog
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class SearchItemsPageViewModel : BaseViewModel
    {
        private static ISettings AppSettings =>
        CrossSettings.Current;

        private ObservableCollection<ItemCategory> filterOptions;
        private ObservableCollection<string> sortOptions;
        private Command quantitySelectedCommand;
        private Command itemSelectedCommand;
        private Command sortCommand;
        private Command filterCommand;
        private Command addToCartCommand;
        private Command cardItemCommand;
        private Command backButtonCommand;
        private long cartItemCount;
        private readonly INavigation Navigation;


        public SearchItemsPageViewModel(INavigation _navigation)
        {
            this.Navigation = _navigation;
            
        }

        private ObservableCollection<ItemCatalogueModel> items;

        [DataMember(Name = "ItemCatalogueModel")]
        public ObservableCollection<ItemCatalogueModel> ItemCatalogueList
        {
            get { return this.items; }
            set
            {
                if (this.items == value)
                {
                    return;
                }



                this.items = value;
                this.NotifyPropertyChanged();
            }
        }
       
        public ObservableCollection<ItemCategory> FilterOptions
        {
            get
            {
                return this.filterOptions;
            }



            set
            {
                if (this.filterOptions == value)
                {
                    return;
                }



                this.filterOptions = value;
                this.NotifyPropertyChanged();
            }
        }


        public ObservableCollection<string> SortOptions
        {
            get
            {
                return this.sortOptions;
            }



            set
            {
                if (this.sortOptions == value)
                {
                    return;
                }



                this.sortOptions = value;
                this.NotifyPropertyChanged();
            }
        }


        public long CartItemCount
        {
            get
            {
                return this.cartItemCount;
            }
            set
            {
                this.cartItemCount = value;
                this.NotifyPropertyChanged();
            }
        }

        public Command AddToCartCommand
        {
            get { return this.addToCartCommand ?? (this.addToCartCommand = new Command(this.AddToCartClicked)); }
        }


        public Command CardItemCommand
        {
            get { return this.cardItemCommand ?? (this.cardItemCommand = new Command(this.CartClicked)); }
        }

        public Command BackButtonCommand
        {
            get { return this.backButtonCommand ?? (this.backButtonCommand = new Command(this.BackButtonClicked)); }
        }

          
        public async void SetCartCount()
        {
            var UserId = AppSettings.GetValueOrDefault("UserId", "0");
            var URI = Resources.APIPrefix + Resources.GetCartItems + UserId.ToString();
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(URI);
            var CartItems = JsonConvert.DeserializeObject<List<ItemCatalogueModel>>(response);
            this.CartItemCount = CartItems.Count;
        }
        public async void GetItems(string SearchString)
        {
            var URI = Resources.APIPrefix + Resources.ItemList + SearchString;
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(URI);
            var ItemList = JsonConvert.DeserializeObject<List<ItemCatalogueModel>>(response);
            foreach (ItemCatalogueModel Item in ItemList)
            {
                Item.Quantity = 1;
            }
            ItemCatalogueList = new ObservableCollection<ItemCatalogueModel>(ItemList);
            
        }
            

        private async void AddToCartClicked(object obj)
        {
            ItemCatalogueModel Item = (ItemCatalogueModel)obj;
            long Quantity = Item.Quantity;
            var UserId= AppSettings.GetValueOrDefault("UserId","0");

            var formContent = MakeFormContent(UserId,Item.ItemId,Quantity);
            string url = Resources.APIPrefix + Resources.AddToCart;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url, formContent);
            var status = response.StatusCode;
            if (status == HttpStatusCode.OK)
            {
                this.CartItemCount += 1;
                DependencyService.Get<IMessage>().LongAlert("Item added to Cart");
            }
            else if (status == HttpStatusCode.Found)
            {
                DependencyService.Get<IMessage>().LongAlert("Updated Quantity");
            }
            else if (status == HttpStatusCode.NotModified)
            {
                DependencyService.Get<IMessage>().LongAlert("Unable to add to cart");
            }

        }

        
        private FormUrlEncodedContent MakeFormContent(string UserId,long ItemId,long Quantity)
        {
           
            var formcontent = new FormUrlEncodedContent(new[]
            {
                        new KeyValuePair<string,string>(Resources.UserId,UserId),
                        new KeyValuePair<string,string>(Resources.ItemId,ItemId.ToString()),
                        new KeyValuePair<string,string>(Resources.Quantity,Quantity.ToString())
            });
            return formcontent;
        }


        private async void CartClicked(object obj)
        {
            if(this.CartItemCount==0)
            await Navigation.PushAsync(new EmptyCartPage());
            else
            {
                await Navigation.PushAsync(new CartPage());

            }
        }

        private async void BackButtonClicked(object obj)
        {

            await Navigation.PushAsync(new CategoryTilePage());
        }

    }
}