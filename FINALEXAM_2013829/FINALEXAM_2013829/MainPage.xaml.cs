using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FINALEXAM_2013829
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Student> allStudents { get; set; } = new ObservableCollection<Student>();

        public MainPage()
        {
            InitializeComponent();
            listView.ItemsSource = allStudents;
            Student.StartObserving(allStudents);
        }

        private async void OnLogOff_Clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

        private async void OnAddNewStudent_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentInfoPage());
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedStudent = allStudents[e.SelectedItemIndex];

            listView.SelectedItem = null;

            await Navigation.PushAsync(new StudentInfoPage(selectedStudent));
        }
    }
}