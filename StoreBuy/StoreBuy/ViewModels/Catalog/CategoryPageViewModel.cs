using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using StoreBuy.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StoreBuy.ViewModels.Catalog
{
    
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CategoryPageViewModel : BaseViewModel
    {

        private ObservableCollection<Category> categories;

        private Command categorySelectedCommand;

        private Command notificationCommand;

        private Command backButtonCommand;


        [DataMember(Name = "categories")]
        public ObservableCollection<Category> Categories
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

    }
}