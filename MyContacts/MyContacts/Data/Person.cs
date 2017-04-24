using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyContacts
{
    [Table("Person")]
    public class Person : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate {};
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        public string FirstName { get; set; }

        [MaxLength(250)]
        public string LastName { get; set; }

        [MaxLength(250)]
        public string Email { get; set; }

        [MaxLength(250)]
        public bool IsFavorite { get; set; }

        [MaxLength(250)]
        public DateTime Dob { get; set; }

        [MaxLength(250)]
        public string PhoneNumber { get; set; }

        [MaxLength(250)]
        public string HeadshotUrl { get; set; }

        [MaxLength(250)]
        public Gender Gender { get; set; }


        public override string ToString() => FirstName +" "+ LastName + " " + PhoneNumber + " " + Email;
    }
    
}
