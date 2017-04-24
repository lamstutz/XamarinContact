using Xamarin.Forms;
using System.Linq;

namespace MyContacts
{
    public class App : Application
    {

        public static PersonRepository PersonRepo { get; set; }
        public App(string dbPath)
        {

            PersonRepo = new PersonRepository(dbPath);
            MainPage = new NavigationPage(new AllContacts())
            {
                BarBackgroundColor = Color.FromHex("#fe8e00"),
                BarTextColor = Color.White,
            };
        }
    }
}
