using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using StoreBuy.Models;
using StoreBuy.Views;
using StoreBuy.Views.Catalog;
using StoreBuy.Views.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
namespace StoreBuy.ViewModels.Forms
{
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : LoginViewModel
    {
        #region Fields
        public INavigation Navigation { get; set; }


        private static ISettings AppSettings =>  CrossSettings.Current;

        #endregion

        #region Constructor

        public LoginPageViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
        }

        #endregion

       

        #region Command


        public Command SignUpCommand { get; set; }

        public Command ForgotPasswordCommand { get; set; }

        
        public Command SocialMediaLoginCommand { get; set; }

        #endregion

        #region methods

        public async void LoginClicked()
        {

            var formcontent = MakeFormContent();

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(Resources.APIPrefix+Resources.LoginURI, formcontent);
            var status = response.StatusCode;

            if (status == HttpStatusCode.Found)
            {
                var URI = Resources.APIPrefix + Resources.GetUserByUserNameURI + Email;        
                var Userresponse = await client.GetStringAsync(URI);
                var User = JsonConvert.DeserializeObject<Users>(Userresponse);

                AppSettings.AddOrUpdateValue("UserName", User.Email);
                AppSettings.AddOrUpdateValue("UserId", User.UserId.ToString());
                
                await Navigation.PushAsync(new CategoryTilePage());
            }
            else
            {
                await Navigation.PushAsync(new Unsucess());
            }
        }



        private FormUrlEncodedContent MakeFormContent()
        {
            var formcontent = new FormUrlEncodedContent(new[]
              {
                        new KeyValuePair<string,string>(Resources.Email,Email),
                        new KeyValuePair<string, string>(Resources.Password,Password)
                    });
            return formcontent;
        }

        private async void SignUpClicked(object obj)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        private async void ForgotPasswordClicked(object obj)
        {
            var label = obj as Label;
            label.BackgroundColor = Color.FromHex("#70FFFFFF");
            await Task.Delay(100);
            label.BackgroundColor = Color.Transparent;
            await Navigation.PushAsync(new ForgotPasswordPage());
        }

        private void SocialLoggedIn(object obj)
        {
           
        }

        #endregion
    }
}