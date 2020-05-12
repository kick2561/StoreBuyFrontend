using Plugin.Settings;
using Plugin.Settings.Abstractions;
using StoreBuy.Views;
using StoreBuy.Views.Catalog;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StoreBuy.ViewModels.Forms
{
    
    [Preserve(AllMembers = true)]
    public class ResetPasswordViewModel : BaseViewModel
    {
        #region Fields
        private static ISettings AppSettings => CrossSettings.Current;


        private string newPassword;

        private string confirmNewPassword;

        private string misMatchText;

        INavigation Navigation { get; set; }
        

        #endregion

        #region Constructor

        public ResetPasswordViewModel(INavigation _navigation)
        {
            this.Navigation = _navigation;
            this.SignUpCommand = new Command(this.SignUpClicked);
        }

        #endregion


        #region Command

        
        public Command SubmitCommand { get; set; }

      
        public Command SignUpCommand { get; set; }

        #endregion

        #region Public property

       
        public string NewPassword
        {
            get
            {
                return this.newPassword;
            }

            set
            {
                if (this.newPassword == value)
                {
                    return;
                }

                this.newPassword = value;
                this.NotifyPropertyChanged();
            }
        }

       
        public string ConfirmNewPassword
        {
            get
            {
                return this.confirmNewPassword;
            }

            set
            {
                if (this.confirmNewPassword == value)
                {
                    return;
                }

                this.confirmNewPassword = value;
                this.NotifyPropertyChanged();
            }
        }

        public string MisMatchText
        {
            get
            {
                return this.misMatchText;
            }

            set
            {
                if (this.misMatchText == value)
                {
                    return;
                }

                this.misMatchText = value;
                this.NotifyPropertyChanged();
            }
        }


        #endregion

        #region Methods

        
        public async void SubmitClicked()
        {
            var IsMatch=CheckPasswords();
            if(IsMatch!=true)
            {
                MisMatchText = "Passwords do not match";
            }
            else
            {
                MisMatchText = "";
                var FormContent = MakeFormContent();
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(Resources.APIPrefix + Resources.ResetPasswordURI, FormContent);
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

        private bool CheckPasswords()
        {
            if (NewPassword == ConfirmNewPassword)
                return true;
            return false;
        }


        private FormUrlEncodedContent MakeFormContent()
        {
            var formcontent = new FormUrlEncodedContent(new[]
            {
                        new KeyValuePair<string,string>(Resources.Email,AppSettings.GetValueOrDefault("ForgotPasswordMail","null")),
                        new KeyValuePair<string, string>(Resources.NewPassword,NewPassword)
            });
            return formcontent;
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            // Do something
        }

        #endregion
    }
}