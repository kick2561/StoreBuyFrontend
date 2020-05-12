using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace StoreBuy.Models
{
   
    [Preserve(AllMembers = true)]
    [DataContract]
    public class Review
    {
        #region Field

        /// <summary>
        /// Gets or sets the images
        /// </summary>
        private List<string> images;

        /// <summary>
        /// Gets or sets the profile image
        /// </summary>
        private string customerImage;

        #endregion

       
        [DataMember(Name = "profileimage")]
        public string CustomerImage
        {
            get { return App.BaseImageUrl + this.customerImage; }
            set { this.customerImage = value; }
        }

        
        [DataMember(Name = "images")]
        public List<string> Images
        {
            get
            {
                if (images != null)
                {
                    for (var i = 0; i < this.images.Count; i++)
                    {
                        this.images[i] = this.images[i].Contains(App.BaseImageUrl) ? this.images[i] : App.BaseImageUrl + this.images[i];
                    }
                }
                return this.images;
            }

            set
            {
                this.images = value;
            }
        }

       
        [DataMember(Name = "customername")]
        public string CustomerName { get; set; }

        [DataMember(Name = "revieweddate")]
        public string ReviewedDate { get; set; }

       
        [DataMember(Name = "rating")]
        public double Rating { get; set; }

       
        [DataMember(Name = "comment")]
        public string Comment { get; set; }
    }
}
