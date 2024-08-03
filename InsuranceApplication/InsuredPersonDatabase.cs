using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApplication
{
	/// <summary>
	/// The class represents a database of insured persons.
	/// </summary>
	internal class InsuredPersonDatabase
	{
		private List<InsuredPerson> databaseList;
		private int lastId;

		/// <summary>
		/// Creates new instance of <see cref="InsuredPersonDatabase"/>. This constructor is used by <see cref="DataManager"/>.
		/// </summary>
		/// <param name="list"></param>
		public InsuredPersonDatabase(List<InsuredPerson> list)
		{
			databaseList = list;
			if (databaseList.Count > 0)
				lastId = list.OrderBy(person => person.Id).Last().Id;
			else
				lastId = 0;
		}

		/// <summary>
		/// Adds a new instance of <see cref="InsuredPerson"/> into database.
		/// </summary>
		/// <param name="firstName">First name of a person.</param>
		/// <param name="lastName">Surname of a person.</param>
		/// <param name="age">Person's age.</param>
		/// <param name="phoneNumber">Person's phone number.</param>
		public void AddNewPerson(string firstName, string lastName, int age, string phoneNumber)
		{
			lastId += 1;
			databaseList.Add(new InsuredPerson(lastId, firstName, lastName, age, phoneNumber, DateTime.Now));
		}

		/// <summary>
		/// Deletes a person from database.
		/// </summary>
		/// <param name="id">Id of <see cref="InsuredPerson"/> to be deleted.</param>
		/// <returns><see langword="true"/> if <see cref="InsuredPerson"/> was found in database; otherwise <see langword="false"/>.</returns>
		public bool DeletePerson(int id)
		{
			InsuredPerson person = databaseList.Find(p => p.Id == id);
			if (person != null)
			{
				databaseList.Remove(person);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns database list.
		/// </summary>
		/// <returns>Database <see langword="private"/> <see cref="List{InsuranceApplication.InsuredPerson}"/>.</returns>
		public List<InsuredPerson> GetDatabaseList()
		{
			return databaseList;
		}

		/// <summary>
		/// Retrieves all the elements that match by <paramref name="firstName"/> and <paramref name="lastName"/> arguments. The method is not case-sensitive.
		/// </summary>
		/// <param name="firstName">First name of a person.</param>
		/// <param name="lastName">Surname of a person.</param>
		/// <returns> <see cref="List{T}"/> of <see cref="InsuredPerson"/>s found in the database.</returns>
		public List<InsuredPerson> FindAll(string firstName, string lastName)
		{
			List<InsuredPerson> list = new List<InsuredPerson>();
			foreach (InsuredPerson person in databaseList)
			{
				if (person.Firstname.ToLower() == firstName.ToLower() && person.LastName.ToLower() == lastName.ToLower())
					list.Add(person);
			}
			return list;
		}
	}
}
