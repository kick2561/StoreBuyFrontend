using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace StoreBuy.Models
{
    [Preserve(AllMembers = true)]
    [DataContract]
    class CartItemModel
    {

        [DataMember(Name = "ItemId")]
        public virtual long ItemId { get; set; }



        [DataMember(Name = "UserId")]
        public virtual long UserId { get; set; }



        [DataMember(Name = "CartId")]
        public virtual long CartId { get; set; }




        [DataMember(Name = "Quantity")]
        public virtual long Quantity { get; set; }

    }
}
