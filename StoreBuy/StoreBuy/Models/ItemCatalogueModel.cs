using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace StoreBuy.Models
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ItemCatalogueModel : INotifyPropertyChanged
    {
        
        [DataMember(Name = "ItemId")]
        public virtual long ItemId { get; set; }



        [DataMember(Name = "ItemName")]
        public virtual string ItemName { get; set; }



        [DataMember(Name = "Price")]
        public virtual float Price { get; set; }



        [DataMember(Name = "ItemDescription")]
        public virtual string ItemDescription { get; set; }



        [DataMember(Name = "CategoryName")]
        public virtual string CategoryName { get; set; }



        [DataMember(Name = "Quantity")]
        public virtual long Quantity { get; set; }


        [DataMember(Name = "ItemImage")]
        public virtual byte[] ItemImage { get; set; }


        [DataMember(Name = "quantities")]
        public List<object> Quantities { get; set; } = new List<object> { 1, 2, 3, 4, 5 };

        public event PropertyChangedEventHandler PropertyChanged;



       
        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

