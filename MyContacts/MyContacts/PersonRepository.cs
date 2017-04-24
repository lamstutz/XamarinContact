using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using System.Threading.Tasks;

namespace MyContacts
{
	public class PersonRepository
	{
		public SQLiteAsyncConnection conn;

		public string StatusMessage { get; set; }

		public PersonRepository(string dbPath)
		{
			conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Person>().Wait();            
        }

		public async Task AddNewPersonAsync(string firstName, string lastName, string headshotUrl, string email, DateTime dob, Gender gender, bool isFavorite)
		{
		    try
			{
				//basic validation to ensure a name was entered
				if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                    throw new Exception("Valid name required");

				//insert a new person into the Person table
				var result = await conn.InsertAsync(new Person { FirstName = firstName, LastName = lastName, HeadshotUrl = headshotUrl, Email= email, Dob=dob,Gender = gender, IsFavorite = isFavorite }).ConfigureAwait(continueOnCapturedContext: false);
				//StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, firstName);
			}
			catch (Exception ex)
			{
				//StatusMessage = string.Format("Failed to add {0}. Error: {1}", firstName, ex.Message);
			}
		}

        public async Task pushPersonAsync(Person npers)
        {
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(npers.FirstName))
                    throw new Exception("Valid first name required");

                var result = -1;
                //insert or maj a person into the Person table
                if ( npers.Id == 0)
                {
                    result = await conn.InsertAsync(npers).ConfigureAwait(continueOnCapturedContext: false);
                }
                else
                {
                    result = await conn.UpdateAsync(npers).ConfigureAwait(continueOnCapturedContext: false);
                }
                
               // StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, npers.FirstName);
                
            }
            catch (Exception ex)
            {
                //StatusMessage = string.Format("Failed to add {0}. Error: {1}", npers.FirstName, ex.Message);
            }
        }

        public async Task deletePersonAsync(Person npers)
        {
            try
            {
                

                //insert a new person into the Person table
                var result = await conn.DeleteAsync(npers).ConfigureAwait(continueOnCapturedContext: false);
               // StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, npers.FirstName);
            }
            catch (Exception ex)
            {
               // StatusMessage = string.Format("Failed to delete {0}. Error: {1}", npers.FirstName, ex.Message);
            }
        }

        public Task<List<Person>> GetAllPeopleAsync()
		{
            //return a list of people saved to the Person table in the database
            return conn.Table<Person>().ToListAsync();
        }
	}
}