using System.Collections.ObjectModel;
using StoreBuy.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Runtime.Serialization;
using StoreBuy.Controls;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using StoreBuy.Views.ErrorAndEmpty;
using Plugin.Settings.Abstractions;
using Plugin.Settings;
using StoreBuy.Views;
using StoreBuy.Views.Catalog;

namespace StoreBuy.ViewModels.Bookmarks
{

    [Preserve(AllMembers = true)]
    [DataContract]
    public class CartPageViewModel : BaseViewModel
    {

        private static ISettings AppSettings => CrossSettings.Current;


        private ObservableCollection<Product> cartDetails;

        private double totalPrice;

        private long cartItemCount;

        private double discountPrice;

        private double discountPercent;

        private double percent;

        private ObservableCollection<Product> produts;

        private Command placeOrderCommand;

        private Command removeCommand;

        private Command quantitySelectedCommand;

        private Command variantSelectedCommand;

        private Command applyCouponCommand;

        private Command backButtonCommand;

        private INavigation Navigation;


        public CartPageViewModel(INavigation _navigation)
        {
            this.Navigation = _navigation;
        }
        #region Public properties

        public ObservableCollection<Product> CartDetails
        {
            get
            {
                return this.cartDetails;
            }

            set
            {
                if (this.cartDetails == value)
                {
                    return;
                }

                this.cartDetails = value;
                this.NotifyPropertyChanged();
            }
        }

        public double TotalPrice
        {
            get
            {
                return this.totalPrice;
            }

            set
            {
                if (this.totalPrice == value)
                {
                    return;
                }

                this.totalPrice = value;
                this.NotifyPropertyChanged();
            }
        }


        public double DiscountPrice
        {
            get
            {
                return this.discountPrice;
            }

            set
            {
                if (this.discountPrice == value)
                {
                    return;
                }

                this.discountPrice = value;
                this.NotifyPropertyChanged();
            }
        }

        public double DiscountPercent
        {
            get
            {
                return this.discountPercent;
            }

            set
            {
                if (this.discountPercent == value)
                {
                    return;
                }

                this.discountPercent = value;
                this.NotifyPropertyChanged();
            }
        }

        [DataMember(Name = "products")]
        public ObservableCollection<Product> Products
        {
            get
            {
                return this.produts;
            }

            set
            {
                if (this.produts == value)
                {
                    return;
                }
                this.produts = value;
                this.NotifyPropertyChanged();
                this.GetProducts(Products);
                this.UpdatePrice();
            }
        }
       
        public Command RemoveCommand
        {
            get { return this.removeCommand ?? (this.removeCommand = new Command(this.RemoveClicked)); }
        }


        public Command QuantitySelectedCommand
        {
            get { return this.quantitySelectedCommand ?? (this.quantitySelectedCommand = new Command(this.QuantitySelected)); }
        }

        private ObservableCollection<ItemCatalogueModel> cartItems;
        [DataMember(Name = "CartItems")]
        public ObservableCollection<ItemCatalogueModel> CartItems
        {
            get { return this.cartItems; }
            set
            {
                if (this.cartItems == value)
                {
                    return;
                }



                this.cartItems = value;
                this.NotifyPropertyChanged();
            }
        }

        public long CartItemsCount
        {
            get { return this.cartItemCount; }
            set
            {
                if (this.cartItemCount == value)
                {
                    return;
                }



                this.cartItemCount = value;
                this.NotifyPropertyChanged();
            }
        }
        public async void GetCartItems()
        {
            using (var client = new HttpClient())
            {
                var UserId = AppSettings.GetValueOrDefault("UserId","0");
                
                var uri = Resources.APIPrefix+Resources.GetCartItems+UserId;
                var response = await client.GetStringAsync(uri);
                var ItemCatalogue = JsonConvert.DeserializeObject<List<ItemCatalogueModel>>(response);
                CartItems = new ObservableCollection<ItemCatalogueModel>(ItemCatalogue);
                this.CartItemsCount = CartItems.Count;
            }
        }
     
        private async void RemoveClicked(object obj)
        {
            var SelectedItem = (ItemCatalogueModel)obj;
            var UserId = AppSettings.GetValueOrDefault("UserId", "0");
            var formcontent = new FormUrlEncodedContent(new[]
              {               
                        new KeyValuePair<string,string>("UserId",UserId),
                        new KeyValuePair<string, string>("ItemId",SelectedItem.ItemId.ToString()),
                    });
            using (var client = new HttpClient())
            {
                var uri =Resources.APIPrefix+Resources.DeleteCartItem;
                HttpResponseMessage response = await client.PostAsync(uri, formcontent);
                var status = response.StatusCode;



                if (status == HttpStatusCode.OK)
                {
                    this.CartItems.Remove(SelectedItem);
                    this.CartItemsCount =CartItems.Count;
                    if(this.CartItemsCount==0)
                    {
                        await Navigation.PushAsync(new EmptyCartPage());
                    }
                    
                }
                else
                {
                     await Navigation.PushAsync(new Unsucess());
                }
            }
        }



       
        private async void QuantitySelected(object selectedItem)
        {
            var SelectedItem = (ItemCatalogueModel)selectedItem;
            var UserId = AppSettings.GetValueOrDefault("UserId", "0");
            var formcontent = new FormUrlEncodedContent(new[]
              {
                        new KeyValuePair<string,string>("UserId",UserId),
                        new KeyValuePair<string, string>("ItemId",SelectedItem.ItemId.ToString()),
                        new KeyValuePair<string, string>("Quantity",SelectedItem.Quantity.ToString())
                    });
            using (var client = new HttpClient())
            {
                var uri = Resources.APIPrefix+Resources.UpdateItemQuantity;
                HttpResponseMessage response = await client.PostAsync(uri, formcontent);
                var status = response.StatusCode;



                if (status == HttpStatusCode.OK)
                {
                     await Navigation.PushAsync(new CategoryTilePage());
                }
                else
                {
                    await Navigation.PushAsync(new Unsucess());
                }
            }
        }      



        private void GetProducts(ObservableCollection<Product> Products)
        {
            this.CartDetails = new ObservableCollection<Product>();
            if (Products != null && Products.Count > 0)
                this.CartDetails = Products;
        }

      
        private void UpdatePrice()
        {
            this.ResetPriceValue();

            if (this.CartDetails != null && this.CartDetails.Count > 0)
            {
                foreach (var cartDetail in this.CartDetails)
                {
                    if (cartDetail.TotalQuantity == 0)
                        cartDetail.TotalQuantity = 1;
                    this.TotalPrice += (cartDetail.ActualPrice * cartDetail.TotalQuantity);
                    this.DiscountPrice += (cartDetail.DiscountPrice * cartDetail.TotalQuantity);
                    this.percent += cartDetail.DiscountPercent;
                }

                this.DiscountPercent = this.percent > 0 ? this.percent / this.CartDetails.Count : 0;
            }
        }


        private void ResetPriceValue()
        {
            this.TotalPrice = 0;
            this.DiscountPercent = 0;
            this.DiscountPrice = 0;
            this.percent = 0;
        }

    }
}
