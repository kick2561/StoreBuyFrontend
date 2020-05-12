using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace StoreBuy.Models
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ItemCategory
    {
        [DataMember(Name = "CategoryId")]
        public virtual long CategoryId { get; set; }



        [DataMember(Name = "CategoryName")]
        public virtual string CategoryName { get; set; }


        [DataMember(Name = "CategoryImage")]
        public virtual byte[] CategoryImage { get; set; }



    }
}
