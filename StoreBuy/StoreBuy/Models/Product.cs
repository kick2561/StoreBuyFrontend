using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace StoreBuy.Models
{
    
    [Preserve(AllMembers = true)]
    [DataContract]
    public class Product : INotifyPropertyChanged
    {
        #region Fields

        private bool isFavourite;

        private string previewImage;

        private List<string> previewImages;

        private int totalQuantity;

        private double actualPrice;

        private double discountPrice;

        private double discountPercent;

        private ObservableCollection<Review> reviews = new ObservableCollection<Review>();

        #endregion

        #region Event

       
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "previewimage")]
        public string PreviewImage
        {
            get { return App.BaseImageUrl + this.previewImage; }
            set { this.previewImage = value; }
        }

        [DataMember(Name = "previewimages")]
        public List<string> PreviewImages
        {
            get
            {
                for (var i = 0; i < this.previewImages.Count; i++)
                {
                    this.previewImages[i] = this.previewImages[i].Contains(App.BaseImageUrl) ? this.previewImages[i] : App.BaseImageUrl + this.previewImages[i];
                }

                return this.previewImages;
            }

            set
            {
                this.previewImages = value;
            }
        }

      
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "summary")]
        public string Summary { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }


        [DataMember(Name = "actualprice")]
        public double ActualPrice
        {
            get
            {
                return this.actualPrice;
            }

            set
            {
                this.actualPrice = value;
                this.NotifyPropertyChanged(nameof(ActualPrice));
            }
        }

        public double DiscountPrice
        {
            get
            {
                return this.ActualPrice - (this.ActualPrice * (this.DiscountPercent / 100));
            }

            set
            {
                this.discountPrice = value;
                this.NotifyPropertyChanged(nameof(DiscountPrice));
            }
        }

        
        [DataMember(Name = "discountpercent")]
        public double DiscountPercent
        {
            get
            {
                return this.discountPercent;
            }

            set
            {
                this.discountPercent = value;
                this.NotifyPropertyChanged(nameof(DiscountPercent));
            }
        }

      
        [DataMember(Name = "overallrating")]
        public double OverallRating { get; set; }

        [DataMember(Name = "reviews")]
        public ObservableCollection<Review> Reviews
        {
            get
            {
                return this.reviews;
            }

            set
            {
                this.reviews = value;
                this.NotifyPropertyChanged(nameof(Reviews));
            }
        }

       
        public string SellerName { get; set; }

      
        [DataMember(Name = "quantities")]
        public List<object> Quantities { get; set; } = new List<object> { 1, 2, 3, 4, 5 };

     
        [DataMember(Name = "sizevariants")]
        public List<string> SizeVariants { get; set; } = new List<string> { "XS", "S", "M", "L", "XL" };

     
        [DataMember(Name = "isfavourite")]
        public bool IsFavourite
        {
            get
            {
                return this.isFavourite;
            }

            set
            {
                this.isFavourite = value;
                this.NotifyPropertyChanged(nameof(IsFavourite));
            }
        }

        [DataMember(Name = "totalquantity")]
        public int TotalQuantity
        {
            get
            {
                return this.totalQuantity;
            }

            set
            {
                this.totalQuantity = value;
                this.NotifyPropertyChanged(nameof(TotalQuantity));
            }
        }

        #endregion

        #region Methods

      
        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}