using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FINALEXAM_2013829
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentInfoPage : ContentPage
    {
        private Student selectedStudent;

        public StudentInfoPage(Student selectedStudent = null)
        {
            InitializeComponent();

            if (selectedStudent == null) //Insert mode
            {
                idLabels.IsVisible = false;
                btnDelete.IsVisible = false;
            }
            else  //Edit mode = selectedEmployee != null
            {
                this.selectedStudent = selectedStudent;
                lblId.Text = selectedStudent.Uid;
                edtFirstName.Text = selectedStudent.FirstName;
                edtLastName.Text = selectedStudent.LastName;
                edtAge.Text = selectedStudent.Age;
            }
        }

        private void showMessage(string message)
        {
            lblMessage.Text = message;
        }

        private async void OnSave_Clicked(object sender, EventArgs e)
        {
            if (this.selectedStudent == null)
            {
                this.selectedStudent = new Student();
            }

            if (String.IsNullOrWhiteSpace(edtFirstName.Text) || String.IsNullOrEmpty(edtFirstName.Text))
            {
                showMessage("First name cannot be empty!");
                edtFirstName.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(edtLastName.Text) || String.IsNullOrEmpty(edtLastName.Text))
            {
                showMessage("Last name cannot be empty!");
                edtLastName.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(edtAge.Text) || String.IsNullOrEmpty(edtAge.Text))
            {
                showMessage("Age name cannot be empty!");
                edtAge.Focus();
                return;
            }

            if (edtFirstName.Text.Length < 2)
            {
                showMessage("First name should be at least two chars length!");
                edtFirstName.Focus();
                return;
            }
            else
            {
                this.selectedStudent.FirstName = edtFirstName.Text.ToUpper();
            }

            if (edtLastName.Text.Length < 2)
            {
                showMessage("Last name should be at least two chars length!");
                edtLastName.Focus();
                return;
            }
            else
            {
                this.selectedStudent.LastName = edtLastName.Text.ToUpper();
            }

            if (Convert.ToInt32(edtAge.Text) < 16 || Convert.ToInt32(edtAge.Text) > 100)
            {
                showMessage("Please enter an age from 16 to 100!");
                edtAge.Focus();
                return;
            }
            else
            {
                this.selectedStudent.Age = edtAge.Text;
            }

            var success = await this.selectedStudent.Save();

            if (success)
            {
                await Navigation.PopAsync();
            }
            else
            {
                var result = DisplayAlert("Error", "Error saving Employee!", "Ok");
            }
        }

        private void entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblMessage.Text = "";
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            var success = await this.selectedStudent.Delete();

            if (success)
            {
                await Navigation.PopAsync();
            }
        }
    }
}