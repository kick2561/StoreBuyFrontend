using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using StoreBuy.Models;
using StoreBuy.Views;
using StoreBuy.Views.Catalog;
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
    class EmailVerificationViewModel : BaseViewModel
    {
        private string oTP;        

        public string misMatchText;
        public INavigation Navigation { get; set; }

        private static ISettings AppSettings => CrossSettings.Current;

        public EmailVerificationViewModel(INavigation _navigation)
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
            if (!CheckOTPs())
            {
                MisMatchText = "Enter correct format";
            }
            else
            {
                MisMatchText = "";
                var FormContent = MakeFormContent();
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(Resources.APIPrefix + Resources.OTPVerifyURI, FormContent);
                var status = response.StatusCode;

                if (status == HttpStatusCode.OK)
                {
                    Users NewUser = CreateUser();                   

                    string JsonUser = JsonConvert.SerializeObject(NewUser);
                    StringContent Content = new StringContent(JsonUser, Encoding.UTF8, "application/json");
                    HttpResponseMessage Response=await client.PostAsync(Resources.APIPrefix+Resources.UserRegisterURI, Content);
                    var statusRegister = Response.StatusCode;
                    if (statusRegister == HttpStatusCode.OK)
                    {
                        var URI = Resources.APIPrefix + Resources.GetUserByUserNameURI + NewUser.Email;
                        var Userresponse = await client.GetStringAsync(URI);
                        var User = JsonConvert.DeserializeObject<Users>(Userresponse);

                        AppSettings.AddOrUpdateValue("UserName", User.Email);
                        AppSettings.AddOrUpdateValue("UserId", User.UserId);
                        await Navigation.PushAsync(new CategoryTilePage());
                        DependencyService.Get<IMessage>().LongAlert("Successfully Registered");


                    }
                    else
                        await Navigation.PushAsync(new SignUpPage());
                }
                else if(status==HttpStatusCode.Found)
                {
                    MisMatchText = "Enter the correct code";
                }
                else
                {
                    MisMatchText = "OTP Expired";
                }
            }

        }
        private Users CreateUser()
        {
            Users User = new Users();
            User.FirstName = AppSettings.GetValueOrDefault("FirstName", "null");
            User.LastName = AppSettings.GetValueOrDefault("LastName", "null");
            User.Phone = AppSettings.GetValueOrDefault("Phone", "null");
            User.UserPassword = AppSettings.GetValueOrDefault("UserPassword", "null");
            User.Email = AppSettings.GetValueOrDefault("UserName", "null");
            return User;
        }
        private bool CheckOTPs()
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
                        new KeyValuePair<string,string>(Resources.Email,AppSettings.GetValueOrDefault("UserName","null"))
            });
            return formcontent;
        }
        #endregion

    }
}
