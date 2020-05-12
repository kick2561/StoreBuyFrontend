using Xamarin.Forms.Internals;

namespace StoreBuy.ViewModels.Forms
{
    [Preserve(AllMembers = true)]
    public class LoginViewModel : BaseViewModel
    {
        #region Fields

        private string email;

        private string password;

        private string phone;

        private bool isInvalidEmail;

        private bool isInvalidPassword;

        private bool isInvalidPhone;

        

        #endregion

        #region Property

       
        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                if (this.email == value)
                {
                    return;
                }

                this.email = value;
                this.NotifyPropertyChanged();
            }
        }


        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Phone
        {
            get
            {
                return this.phone;
            }

            set
            {
                if (this.phone == value)
                {
                    return;
                }

                this.phone = value;
                this.NotifyPropertyChanged();
            }
        }


        public bool IsInvalidEmail
        {
            get
            {
                return this.isInvalidEmail;
            }

            set
            {
                if (this.isInvalidEmail == value)
                {
                    return;
                }

                this.isInvalidEmail = value;
                this.NotifyPropertyChanged();
            }
        }


        public bool IsInvalidPassword
        {
            get
            {
                return this.isInvalidPassword;
            }

            set
            {
                if (this.isInvalidPassword == value)
                {
                    return;
                }

                this.isInvalidPassword = value;
                this.NotifyPropertyChanged();
            }
        }

        public bool IsInvalidPhone
        {
            get
            {
                return this.isInvalidPhone;
            }

            set
            {
                if (this.isInvalidPhone == value)
                {
                    return;
                }

                this.isInvalidPhone = value;
                this.NotifyPropertyChanged();
            }
        }
        #endregion
    }
}
