using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using StoreBuy.Models;
using StoreBuy.Views;
using StoreBuy.Views.Forms;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StoreBuy.ViewModels.Forms
{
   
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : LoginViewModel
    {
        #region Fields
        private static ISettings AppSettings => CrossSettings.Current;


        private string _firstname;



        private string _lastname;




        private string _userpassword;



        private string _confirmPassword;


        private Users _user;

        INavigation Navigation;

        #endregion

        #region Constructor

       
        public SignUpPageViewModel(INavigation _navigation)
        {
            this.Navigation = _navigation;
            this.LoginCommand = new Command(this.LoginClicked);
         
        }

        #endregion

        #region Property




    public string FirstName
        {
            get
            {
                return this._firstname;
            }


            set
            {
                if (this._firstname == value)
                {
                    return;
                }


                this._firstname = value;
                this.NotifyPropertyChanged();
            }
        }


        public string LastName
        {
            get
            {
                return _lastname;
            }
            set
            {
                if (value != null)
                {
                    _lastname = value;
                    this.NotifyPropertyChanged();


                }
            }
        }


        public string UserPassword
        {
            get
            {
                return _userpassword;
            }
            set
            {
                if (value != null)
                {
                    _userpassword = value;
                    this.NotifyPropertyChanged();


                }
            }
        }


       
        public string ConfirmPassword
        {
            get
            {
                return this._confirmPassword;
            }


            set
            {
                if (this._confirmPassword == value)
                {
                    return;
                }


                this._confirmPassword = value;
                this.NotifyPropertyChanged();
            }
        }

       
        #endregion

        #region Command


        public Command LoginCommand { get; set; }

       
        public Command SignUpCommand { get; set; }     
        

        #endregion

        #region Methods

        private async void LoginClicked(object obj)
        {
            await Navigation.PushAsync(new LoginPage());
        }

       
        public async void SignUpClicked()
        {
            if (_user != null)
            {
                _user.FirstName = FirstName;
                _user.LastName = LastName;
                _user.Email = Email;
                _user.UserPassword = UserPassword;
                _user.Phone = Phone;
            }
            else
            {
                _user = new Users();
                _user.FirstName = FirstName;
                _user.LastName = LastName;
                _user.Email = Email;
                _user.UserPassword = UserPassword;
                _user.Phone = Phone;
            }


            var formContent = MakeFormContent();
            string url = Resources.APIPrefix + Resources.UserCheck;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url,formContent);
            var status = response.StatusCode;
            if (status == HttpStatusCode.Found)
            {
                await Navigation.PushAsync(new LoginPage());
            }
            else if(status==HttpStatusCode.NotFound)
            {

                AppSettings.AddOrUpdateValue("UserName",Email);
                AppSettings.AddOrUpdateValue("UserPassword", UserPassword);
                AppSettings.AddOrUpdateValue("Phone", Phone);
                AppSettings.AddOrUpdateValue("FirstName", FirstName);
                AppSettings.AddOrUpdateValue("LastName", LastName);
                await Navigation.PushAsync(new EmailVerificationPage());
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
                        new KeyValuePair<string,string>(Resources.Email,Email)
            });
            return formcontent;
        }

        #endregion
    }
}