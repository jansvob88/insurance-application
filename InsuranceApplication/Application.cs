using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApplication
{
	/// <summary>
	/// The main class of the application, containing only one method Run(), which contains all the basic logic of the application.
	/// </summary>
	internal class Application
	{
		private UserInterface ui;
		private InputValidator validator;
		private DataManager dataManager;
		private InsuredPersonDatabase database;

		public Application()
		{
			ui = new UserInterface();
			validator = new InputValidator();
			dataManager = new DataManager();
			database = new InsuredPersonDatabase(dataManager.LoadData());
		}

		public void Run()
		{
			int option = 0;

			while (option != 5)
			{
				ui.MainMenu();
				while (!int.TryParse(Console.ReadLine().Trim(), out option) || option < 1 || option > 5)
				{
					ui.InvalidArgument("Neplatná volba. Hodnota musí být v rozmezí 1 - 5");
				}
				switch (option)
				{
					//Add new person
					case 1:
						(string firstName, string lastName, int age, string phoneNumber) newPerson = ui.AddPersonMenu(validator);
						database.AddNewPerson(newPerson.firstName, newPerson.lastName, newPerson.age, newPerson.phoneNumber);
						ui.PressToContinue("Nový záznam byl úspěšně přidán.");
						break;
					//Write person list
					case 2:
						ui.WritePersonList(database.GetDatabaseList());
						ui.PressToContinue();
						break;
					//Search person
					case 3:
						(string firstName, string lastName) searchedPerson = ui.FindPersonMenu(validator);
						ui.WritePersonList(database.FindAll(searchedPerson.firstName, searchedPerson.lastName));
						ui.PressToContinue();
						break;
					//Delete person
					case 4:
						if (database.DeletePerson(ui.DeletePersonMenu(database.GetDatabaseList())))
							ui.PressToContinue("Záznam byl úspěšně smazán.");
						break;
				}
			}

			dataManager.SaveData(database.GetDatabaseList());
		}
	}
}
