using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;
using Plugin.Media;

namespace MyContacts
{
    public partial class ContactDetails : ContentPage
    {

        public Person pers;

        public ContactDetails(Person person = null)
        {
           

            if (person == null)
            {
                person = new Person();
                person.HeadshotUrl = "profil.png";
            }
            pers = person;
			BindingContext = pers;
            InitializeComponent();
        }

        async void save(object sender, EventArgs e)
        {
            await App.PersonRepo.pushPersonAsync(pers);
            goToMainPage();
        }

        void goToMainPage()
        {
            Navigation.PopAsync();
        }

    }
}
