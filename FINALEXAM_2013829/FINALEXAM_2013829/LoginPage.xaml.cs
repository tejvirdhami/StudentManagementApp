using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FINALEXAM_2013829
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public ObservableCollection<User> allUsers { get; set; } = new ObservableCollection<User>();

        public LoginPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            User.StartObserving(allUsers);
        }

        private void showMessage(string message)
        {
            lblMessage.Text = message;
        }

        private async void btnSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(edtLogin.Text) || String.IsNullOrEmpty(edtLogin.Text))
            {
                showMessage("Please enter a Username!");
                edtLogin.Focus();
                return;
            }
            else if (String.IsNullOrWhiteSpace(edtPassword.Text) || String.IsNullOrEmpty(edtPassword.Text))
            {
                showMessage("Please enter Password!");
                edtPassword.Focus();
                return;
            }
            else
            {
                var email = edtLogin.Text;
                var password = edtPassword.Text;
                foreach (User user in allUsers)
                {
                    //---------------------------------------DECODING PASSWORD FROM DATABASE BEFORE COMPARING----------------------------------------------------
                    if (email == user.Login && password == Decoder(user.Password))
                    {
                        Navigation.InsertPageBefore(new MainPage(), this);
                        await Navigation.PopAsync();
                    }
                    else if (email != user.Login || password != user.Password)
                        showMessage("User does not Exist!");
                }
            }
        }

        private void entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            showMessage("");
        }

        //-----------------------------------------------------DECODER FOR PASSWORD--------------------------------------------------------------------------------
        public string Decoder(string sData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(sData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}