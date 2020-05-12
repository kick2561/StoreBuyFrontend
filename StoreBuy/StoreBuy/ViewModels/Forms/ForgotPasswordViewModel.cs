using Plugin.Settings;
using Plugin.Settings.Abstractions;
using StoreBuy.Views;
using StoreBuy.Views.Forms;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StoreBuy.ViewModels.Forms
{
    

    [Preserve(AllMembers = true)]
    public class ForgotPasswordViewModel : LoginViewModel
    {
        private string misMatchText;
        private static ISettings AppSettings =>  CrossSettings.Current;

        #region Constructor
        public INavigation Navigation { get; set; }
       
        public ForgotPasswordViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.SendCommand = new Command(this.SendClicked);
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Send button is clicked.
        /// </summary>
        public Command SendCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        #endregion

        #region Public Properties

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

        private async void SendClicked(object obj)
        {
            if(string.IsNullOrEmpty(Email))
            {
                MisMatchText = "Fields Can't be null";
                return;
            }
            var FormContent = MakeFormContent();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(Resources.APIPrefix + Resources.ForgotPasswordURI, FormContent);
            var status = response.StatusCode;

            if (status == HttpStatusCode.Found)
            {
                AppSettings.AddOrUpdateValue("ForgotPasswordMail",Email);
                await Navigation.PushAsync(new OTPVerificationPage());
            }
            else if(status == HttpStatusCode.BadRequest)
            {
                MisMatchText = "Email Id doesn't exist.";
            }
            else
            {
                MisMatchText = "Unable to send the email";

            }
        }


        private FormUrlEncodedContent MakeFormContent()
        {
            var formcontent = new FormUrlEncodedContent(new[]
            {
                        new KeyValuePair<string,string>(Resources.Email,Email)
            });
            return formcontent;
        }
        private async void SignUpClicked(object obj)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        #endregion
    }
}