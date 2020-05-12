using System.Collections.Generic;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace StoreBuy.Models
{
   
    [Preserve(AllMembers = true)]
    [DataContract]
    public class Category
    {
        private string icon;
        private bool isExpanded;

        [DataMember(Name = "icon")]
        public string Icon
        {
            get { return App.BaseImageUrl + this.icon; }
            set { this.icon = value; }
        }
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
            }
        }
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "subcategories")]
        public List<string> SubCategories { get; set; }
    }
}