using Plugin.Settings;
using Plugin.Settings.Abstractions;
using StoreBuy.Views;
using StoreBuy.Views.Forms;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StoreBuy.ViewModels.Forms
{
    [Preserve(AllMembers = true)]
    class OTPVerificationViewModel: BaseViewModel
    {
        private string oTP;
        private static ISettings AppSettings => CrossSettings.Current;

        private string misMatchText;
        private INavigation Navigation { get; set; }

        

        public OTPVerificationViewModel(INavigation _navigation)
        {
            
            this.Navigation = _navigation;
            this.SubmitCommand = new Command(this.SubmitClicked);
        }



        public Command SubmitCommand { get; set; }

        #region Public property

        public string OTP
        {
            get
            {
                return this.oTP;
            }

            set
            {
                if (this.oTP == value)
                {
                    return;
                }

                this.oTP = value;
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

       
        private async void SubmitClicked(object obj)
        {
            if (string.IsNullOrEmpty(OTP))
            {
                MisMatchText = "Fields Can't be null";
                return;
            }
            if (CheckOTP())
            {
                var FormContent = MakeFormContent();
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(Resources.APIPrefix + Resources.OTPVerifyURI, FormContent);
                var status = response.StatusCode;

                if (status == HttpStatusCode.OK)
                {
                    await Navigation.PushAsync(new ResetPasswordPage());
                }
                else if (status == HttpStatusCode.Found)
                {
                    MisMatchText = "Enter the correct code";
                }
                else
                {
                    MisMatchText = "OTP Expired";
                }
            }
            else
            {
                MisMatchText = "Enter correct format";
            }
            

        }

        

        private bool CheckOTP()
        {
            try
            {
                var convertedOTP = Int64.Parse(OTP);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        private FormUrlEncodedContent MakeFormContent()
        {
            var formcontent = new FormUrlEncodedContent(new[]
            {
                        new KeyValuePair<string,string>(Resources.OTP,OTP),
                        new KeyValuePair<string,string>(Resources.Email,AppSettings.GetValueOrDefault("ForgotPasswordMail","null"))
            });
            return formcontent;
        }
        #endregion

    }
}
