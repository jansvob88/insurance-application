using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApplication
{
	/// <summary>
	/// The class represents an insured person.
	/// </summary>
	internal class InsuredPerson
	{
		public int Id { get; init; }
		public string Firstname { get; private set; }
		public string LastName { get; private set; }
		public string PhoneNumber { get; private set; }
		public int Age { get; private set; }
		public DateTime RegistrationDate { get; private set; }

		/// <summary>
		/// Creates a new instance of <see cref="InsuredPerson"/>.
		/// </summary>
		/// <param name="id">Unique Id number of a person</param>
		/// <param name="firstName">First name of a person.</param>
		/// <param name="lastName">Surname of a person.</param>
		/// <param name="age">Person's age.</param>
		/// <param name="phoneNumber">Person's phone number.</param>
		public InsuredPerson(int id, string firstName, string lastName, int age, string phoneNumber)
		{
			Id = id;
			Firstname = firstName;
			LastName = lastName;
			Age = age;
			PhoneNumber = phoneNumber;
			RegistrationDate = DateTime.Now;
		}

		/// <summary>
		/// Creates a new instance of <see cref="InsuredPerson"/>. This constructor is used by <see cref="DataManager"/>.
		/// </summary>
		/// <param name="firstName">First name of a person.</param>
		/// <param name="lastName">Surname of a person.</param>
		/// <param name="age">Person's age.</param>
		/// <param name="phoneNumber">Person's phone number.</param>
		/// <param name="registrationDate">Date of registration</param>
		public InsuredPerson(int id, string firstName, string lastName, int age, string phoneNumber, DateTime registrationDate)
		{
			Id = id;
			Firstname = firstName;
			LastName = lastName;
			Age = age;
			PhoneNumber = phoneNumber;
			RegistrationDate = registrationDate;
		}
	}
}
