using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace MyContacts
{
    public partial class AllContacts : ContentPage
    {
        bool isEditing;
        public List<Person> contacts { get; set; }
        public AllContacts()
        {
            InitializeComponent();
            contacts = GetContacts();
            BindingContext = contactsToGContact(contacts);
            
        }
               

        private ObservableCollection<Grouping<string, Person>> contactsToGContact(List<Person> contactsParam)
        {
            return new ObservableCollection<Grouping<string, Person>>(
               contactsParam
               .OrderBy(c => {
                   String today = DateTime.Now.ToString("M");
                   String birthday = c.Dob.ToString("M");

                   if (today.Equals(birthday))
                   { return "0"; }

                   if (c.IsFavorite)
                   { return "1"; }

                   return c.FirstName;
               })
               .GroupBy(c => {
                   String today = DateTime.Now.ToString("M");
                   String birthday = c.Dob.ToString("M");

                   if (today.Equals(birthday))
                   { return "Birthday"; }

                   if (c.IsFavorite)
                   { return "Favoris"; }

                   return c.FirstName[0].ToString();
               }, c => c)
               .Select(g => new Grouping<string, Person>(g.Key, g)));
        }

        void OnEdit(object sender, EventArgs e)
        {
            isEditing = !isEditing;
            ((ToolbarItem)sender).Text = isEditing ? "Cancel" : "Delete";
        }

        async void OnAdd(object sender, EventArgs e)
        {
            if (!isEditing)
            {

                await this.Navigation.PushAsync(new ContactDetails());
            }
        }

        async void OnDelete(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            Person person = (Person)item.BindingContext;
            await DeletePersonAsync(person);
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!isEditing)
            {
                Person tappedPerson = (Person)e.Item;
                await this.Navigation.PushAsync(new ContactDetails(tappedPerson));
            }
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (isEditing)
            {
                Person person = (Person)e.SelectedItem;
                if (await DeletePersonAsync(person))
                {
                    OnEdit(editButton, EventArgs.Empty);
                }
            }
        }

        async Task<bool> DeletePersonAsync(Person person)
        {
            if (person != null)
            {
                if (await this.DisplayAlert("Confirm", "Are you sure you want to delete " + person.FirstName, "Yes", "No") == true)
                {
                    await App.PersonRepo.deletePersonAsync(person);

                    refreshList();

                    return true;
                }
            }
            return false;
        }




        void OnRefreshing(object sender, EventArgs e)
        {
            refreshList();
        }

        void refreshList()
        {
            ListView lv = allContacts;
            contacts = GetContacts();
            lv.BindingContext = contactsToGContact(contacts);
            lv.IsRefreshing = false;
        }


        public List<Person> GetContacts()
        {

            List<Person> persons = App.PersonRepo.GetAllPeopleAsync().Result;
            return persons;
        }

        private void OnMail(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("mailto:" + ((Person)((Button)sender).BindingContext).Email));
        }


        private void OnCall(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:" + ((Person)((Button)sender).BindingContext).PhoneNumber));
        }

        private void OnSms(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("smsto:" + ((Person)((Button)sender).BindingContext).PhoneNumber));
        }

        public List<Person> filterContact(string searchText)
        {
            if(string.IsNullOrEmpty(searchText) || string.IsNullOrWhiteSpace(searchText))
            {
                return contacts;
            }
            else
            {
                return contacts.Where(x => {
                    string contactString = x.ToString();
                    return contactString.Contains(searchText);
                }).ToList();
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = ((SearchBar)sender).Text;
            List<Person> contactsFiltered = filterContact(searchText);
            allContacts.BindingContext = contactsToGContact(contactsFiltered);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            refreshList();
        }


    }
}
