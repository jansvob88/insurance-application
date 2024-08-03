using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApplication
{
	/// <summary>
	/// The class represents a user interface used to communicate with a user via console.
	/// </summary>
	internal class UserInterface
	{
		private int columnIdWidth = 7;
		private int columnFirstNameWidth = 15;
		private int columnLastNameWidth = 20;
		private int columnAgeWidth = 5;
		private int columnPhoneNumberWidth = 20;

		public string ColumnId { get { return " Id".PadRight(columnIdWidth); } }
		public string ColumnFirstName { get { return " Jméno".PadRight(columnFirstNameWidth); } }
		public string ColumnLastName { get { return " Příjmení".PadRight(columnLastNameWidth); } }
		public string ColumnAge { get { return " Věk".PadRight(columnAgeWidth); } }
		public string ColumnPhoneNumber { get { return " Telefonní číslo".PadRight(columnPhoneNumberWidth); } }

		//Public methods

		/// <summary>
		/// Writes the main menu of the application.
		/// </summary>
		public void MainMenu()
		{
			Console.Clear();
			Headline();
			ChooseOptionMenu();
		}

		/// <summary>
		/// Writes the line informing a user to press any key to continue and waits for the user to press any key.
		/// </summary>
		public void PressToContinue()
		{
			Console.WriteLine();
			Console.WriteLine("Pokračuj stiskem libovolné klávesy...");
			Console.ReadKey();
		}

		/// <summary>
		/// Writes a <paramref name="message"/>, followed by the line informing a user to press any key to continue and waits for the user to press any key.
		/// </summary>
		/// <param name="message"></param>
		public void PressToContinue(string message)
		{
			Console.WriteLine();
			Console.WriteLine(message);
			PressToContinue();
		}

		/// <summary>
		/// Writes an <paramref name="errorMessage"/> into the console, followed by the line informing a user to re-enter requested value.
		/// </summary>
		/// <param name="errorMessage">An error message that is to be displayed.</param>
		public void InvalidArgument(string errorMessage)
		{
			Console.WriteLine();
			Console.WriteLine(errorMessage);
			Console.Write("Zadejte znovu: ");
		}

		/// <summary>
		/// Writes a list of all <see cref="InsuredPerson"/>s contained in the given <paramref name="list"/> into the console.
		/// </summary>
		/// <param name="list"></param>
		/// <returns><see langword="true"/> if <paramref name="list"/> contains any elements; otherwise <see langword="false"/>.</returns>
		public bool WritePersonList(List<InsuredPerson> list)
		{
			Console.Clear();
			Headline();
			Console.WriteLine();

			if (list.Count > 0)
			{
				Console.WriteLine($"|{ColumnId}|{ColumnFirstName}|{ColumnLastName}|{ColumnAge}|{ColumnPhoneNumber}|");

				foreach (InsuredPerson p in list)
				{
					Console.WriteLine($"├{UnderLine(columnIdWidth)}┼{UnderLine(columnFirstNameWidth)}┼{UnderLine(columnLastNameWidth)}┼{UnderLine(columnAgeWidth)}┼{UnderLine(columnPhoneNumberWidth)}┤");

					Console.WriteLine(
						$"| {p.Id.ToString().PadRight(columnIdWidth - 1)}" +
						$"| {p.Firstname.PadRight(columnFirstNameWidth - 1)}" +
						$"| {p.LastName.PadRight(columnLastNameWidth - 1)}" +
						$"| {p.Age.ToString().PadRight(columnAgeWidth - 1)}" +
						$"| {p.PhoneNumber.PadRight(columnPhoneNumberWidth - 1)}|"
						);
				}
				Console.WriteLine($"┴{UnderLine(columnIdWidth)}┴{UnderLine(columnFirstNameWidth)}┴{UnderLine(columnLastNameWidth)}┴{UnderLine(columnAgeWidth)}┴{UnderLine(columnPhoneNumberWidth)}┴");
				return true;
			}

			Console.WriteLine("Nebyly nalezeny žádné záznamy.");
			return false;
		}

		/// <summary>
		/// Obtains data from a user via console,
		///  where every input is going through <see cref="InputValidationProcess(string, InputValidator, InputValidator.ValidationMethod)"/>.
		/// </summary>
		/// <param name="validator">Instance of <see cref="InputValidator"/> used to validate user inputs.</param>
		/// <returns>Obtained and validated data from a user.</returns>
		public (string firstName, string lastName, int age, string phoneNumber) AddPersonMenu(InputValidator validator)
		{
			Console.Clear();
			Headline();
			Console.WriteLine();
			Console.WriteLine("Přidat nového pojištěnce");
			Console.WriteLine("------------------------");

			//Firstname
			string firstName = InputValidationProcess("Zadejte křestní jméno pojištěnce: ", validator, validator.ValidateName);
			if (firstName.Length + 2 > columnFirstNameWidth)
				columnFirstNameWidth = firstName.Length + 2;

			//Surname
			string lastName = InputValidationProcess("Zadejte příjmení pojištěnce: ", validator, validator.ValidateName);
			if (lastName.Length + 2 > columnLastNameWidth)
				columnLastNameWidth = lastName.Length + 2;

			//Age
			string strAge = InputValidationProcess("Zadejte věk pojištěnce: ", validator, validator.ValidateAge);
			int intAge = int.Parse(strAge);

			//Phone number
			string phoneNumber = InputValidationProcess("Zadejte telefonní číslo pojištěnce: ", validator, validator.ValidatePhoneNumber);
			phoneNumber = string.Join(string.Empty, phoneNumber.Split(' ', StringSplitOptions.RemoveEmptyEntries));
			if (phoneNumber.Length + 2 > columnPhoneNumberWidth)
				columnPhoneNumberWidth = phoneNumber.Length + 2;

			return (firstName, lastName, intAge, phoneNumber);
		}
		/// <summary>
		/// Writes a list of all <see cref="InsuredPerson"/>s in the <paramref name="list"/> and asks a user to choose a person to delete.
		/// </summary>
		/// <param name="list"></param>
		/// <returns><see langword="int"/> id of the person to be deleted. Returns -1 if a user chose to cancel the action
		/// or if <paramref name="list"/> doesn't contain any elements.</returns>
		public int DeletePersonMenu(List<InsuredPerson> list)
		{
			if (WritePersonList(list))
			{
				Console.WriteLine();
				Console.Write("Zadejte ID pojištěného k vymazání: ");
				int id = -1;
				while (!int.TryParse(Console.ReadLine().Trim(), out id) || !list.Any(person => person.Id == id))
				{
					InvalidArgument("Chyba! Zadaná hodnota ID nebyla nalezena.");
				}

				Console.WriteLine();
				WritePersonList(new List<InsuredPerson> { list.Find(person => person.Id == id) });
				Console.WriteLine();
				Console.WriteLine("Jste si jisti, že chcete tento záznam vymazat?");
				Console.Write("ano/ne: ");
				string option = Console.ReadLine().Trim().ToLower();
				while (option != "ano" && option != "ne")
				{
					InvalidArgument("Chyba! Zadejte \"ano\" pro smazání záznamu, \"ne\" pro zrušení akce.");
					option = Console.ReadLine().Trim().ToLower();
				}
				if (option == "ano")
					return id;
			}
			PressToContinue();
			return -1;
		}

		/// <summary>
		/// <inheritdoc cref="UserInterface.AddPersonMenu(InputValidator)"/>
		/// </summary>
		/// <param name="validator"><inheritdoc cref="UserInterface.AddPersonMenu(InputValidator)"/></param>
		/// <returns><inheritdoc cref="UserInterface.AddPersonMenu(InputValidator)"/></returns>
		public (string firstName, string lastName) FindPersonMenu(InputValidator validator)
		{
			Console.Clear();
			Headline();
			Console.WriteLine();
			Console.WriteLine("Vyhledat pojištěného");
			Console.WriteLine("--------------------");

			string firstName = InputValidationProcess("Zadejte křestní jméno pojištěnce: ", validator, validator.ValidateName);
			string lastName = InputValidationProcess("Zadejte příjmení pojištěnce: ", validator, validator.ValidateName);

			return (firstName, lastName);
		}

		//Private methods

		/// <summary>
		/// Step 1 - Method informs a user what kind of input is expected.<br />
		/// Step 2 - Method reads the user input and passes it over to <paramref name="validationMethod"/>.<br />
		/// Step 3 - Depending on the <paramref name="validationMethod"/> result, method either informs the user of invalid input and asks the user to re-enter requested value,
		///  or returns validated input in case of the <paramref name="validationMethod"/> result was evaluated as <see cref="ValidationEnums.Correct"/>.
		/// </summary>
		/// <param name="inputRequest">Information about what kind of input is requested. (Example: "Enter name: ", "Enter phone number: ").</param>
		/// <param name="validator">Instance of <see cref="InputValidator"/>.</param>
		/// <param name="validationMethod">A method used to validate the user input.</param>
		/// <returns>Validated user input <see langword="string"/>.</returns>
		private string InputValidationProcess(string inputRequest, InputValidator validator, InputValidator.ValidationMethod validationMethod)
		{
			string input;
			ValidationEnums en;

			Console.WriteLine();
			Console.Write(inputRequest);
			input = Console.ReadLine().Trim();
			while ((en = validationMethod(input)) != ValidationEnums.Correct)
			{
				InvalidArgument(validator.errorMessages[en]);
				input = Console.ReadLine().Trim();
			}

			return input;
		}

		/// <summary>
		/// Writes the headline of the application.
		/// </summary>
		private void Headline()
		{
			Console.WriteLine("--------------------");
			Console.WriteLine("Evidence pojištěných");
			Console.WriteLine("--------------------");
		}

		/// <summary>
		/// Writes the options menu.
		/// </summary>
		private void ChooseOptionMenu()
		{
			Console.WriteLine();
			Console.WriteLine("Vyberte akci:");
			Console.WriteLine("1 - Přidat nového pojištěného");
			Console.WriteLine("2 - Vypsat všechny pojištěné");
			Console.WriteLine("3 - Vyhledat pojištěného");
			Console.WriteLine("4 - Vymazat pojištěného");
			Console.WriteLine("5 - Konec");
			Console.WriteLine();
			Console.Write("Volba: ");
		}

		/// <summary>
		/// Returns a <see langword="string"/> that consists of '─' characters only. The total length of the string is defined by <paramref name="length"/> argument.
		/// </summary>
		/// <param name="length">The total length of the string.</param>
		/// <returns></returns>
		private string UnderLine(int length)
		{
			string s = "";
			for (int i = 0; i < length; i++)
				s += '─';
			return s;
		}
	}
}
