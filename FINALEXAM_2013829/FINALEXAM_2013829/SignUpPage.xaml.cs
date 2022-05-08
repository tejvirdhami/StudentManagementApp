using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FINALEXAM_2013829
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void showMessage(string message)
        {
            lblMessage.Text = message;
        }

        private async void btnSignUp_Clicked(object sender, EventArgs e)
        {
            User user = new User();
            var emailAddress = edtEmailAddress.Text;
            var condition = System.Text.RegularExpressions.Regex.IsMatch(emailAddress, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

            if (String.IsNullOrWhiteSpace(edtEmailAddress.Text) || String.IsNullOrEmpty(edtEmailAddress.Text))
            {
                showMessage("Email cannot be empty!");
                edtEmailAddress.Focus();
                return;
            }
            else if (condition == false)
            {
                showMessage("Please enter a valid email address!");
                edtEmailAddress.Focus();
                return;
            }
            else
            {
                user.Login = edtEmailAddress.Text.ToLower();
            }

            if (String.IsNullOrWhiteSpace(edtFirstName.Text) || String.IsNullOrEmpty(edtFirstName.Text))
            {
                showMessage("First name cannot be empty!");
                edtFirstName.Focus();
                return;
            }
            else if (edtFirstName.Text.Length < 2)
            {
                showMessage("First name should be at least two chars length!");
                edtFirstName.Focus();
                return;
            }
            else
            {
                user.FirstName = edtFirstName.Text.ToUpper();
            }

            if (String.IsNullOrWhiteSpace(edtLastName.Text) || String.IsNullOrEmpty(edtLastName.Text))
            {
                showMessage("Last name cannot be empty!");
                edtLastName.Focus();
                return;
            }
            else if (edtLastName.Text.Length < 2)
            {
                showMessage("Last name should be at least two chars length!");
                edtLastName.Focus();
                return;
            }
            else
            {
                user.LastName = edtLastName.Text.ToUpper();
            }

            if (String.IsNullOrWhiteSpace(edtPasswordCheck.Text) || String.IsNullOrEmpty(edtPasswordCheck.Text))
            {
                showMessage("Password check cannot be empty!");
                edtPasswordCheck.Focus();
                return;
            }

            if (String.IsNullOrWhiteSpace(edtPassword.Text) || String.IsNullOrEmpty(edtPassword.Text))
            {
                showMessage("Password cannot be empty!");
                edtPassword.Focus();
                return;
            }
            else if (edtPassword.Text.Length < 4 || edtPassword.Text.Length > 8)
            {
                showMessage("Password should be 4 to 8 chars long!");
                edtPassword.Focus();
                return;
            }
            else if (edtPasswordCheck.Text != edtPassword.Text)
            {
                showMessage("Passwords do not match!");
                edtPasswordCheck.Focus();
                return;
            }
            else
            {
                //-------------------------------------------ENCODING PASSWORD-----------------------------------------------
                var hashedPass = Encoder(edtPassword.Text);
                user.Password = hashedPass;
            }

            await user.Save();
            await Navigation.PopAsync();
        }

        private void entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            showMessage("");
        }

        //-----------------------------ENCODER FOR PASSWORD------------------------------------------------------------
        public string Encoder(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }
    }
}