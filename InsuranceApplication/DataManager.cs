using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace InsuranceApplication
{
	/// <summary>
	/// The class contains methods for saving and loading data.
	/// </summary>
	internal class DataManager
	{
		private const string fileName = "database.xml";
		private enum XmlNodes
		{
			Root, InsuredPerson, Id, FirstName, LastName, Age, PhoneNumber, RegistrationDate
		}

		private Dictionary<XmlNodes, string> NodeTitles = new Dictionary<XmlNodes, string>
		{
			[XmlNodes.Root] = "database",
			[XmlNodes.InsuredPerson] = "insuredPerson",
			[XmlNodes.Id] = "id",
			[XmlNodes.FirstName] = "firstName",
			[XmlNodes.LastName] = "lastName",
			[XmlNodes.Age] = "age",
			[XmlNodes.PhoneNumber] = "phoneNumber",
			[XmlNodes.RegistrationDate] = "registrationDate"
		};

		/// <summary>
		/// Loads data from a file.
		/// </summary>
		/// <returns></returns>
		public List<InsuredPerson> LoadData()
		{
			if (File.Exists(fileName))
			{
				List<InsuredPerson> list = new List<InsuredPerson>();

				XmlDocument document = new XmlDocument();
				document.Load(fileName);

				XmlNode root = document.DocumentElement;

				foreach (XmlNode node in root.ChildNodes)
				{
					XmlElement person = (XmlElement)node;

					int id = int.Parse(person.GetElementsByTagName(NodeTitles[XmlNodes.Id])[0].InnerText);
					string firstName = person.GetElementsByTagName(NodeTitles[XmlNodes.FirstName])[0].InnerText;
					string lastName = person.GetElementsByTagName(NodeTitles[XmlNodes.LastName])[0].InnerText;
					int age = int.Parse(person.GetElementsByTagName(NodeTitles[XmlNodes.Age])[0].InnerText);
					string phoneNumber = person.GetElementsByTagName(NodeTitles[XmlNodes.PhoneNumber])[0].InnerText;
					DateTime registrationDate = DateTime.Parse(person.GetElementsByTagName(NodeTitles[XmlNodes.RegistrationDate])[0].InnerText);

					list.Add(new InsuredPerson(id, firstName, lastName, age, phoneNumber, registrationDate));					
				}
				return list;
			}
			return new List<InsuredPerson>();
		}

		/// <summary>
		/// Saves data to a file.
		/// </summary>
		/// <param name="list"></param>
		public void SaveData(List<InsuredPerson> list)
		{
			XmlDocument document = new XmlDocument();
			XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "utf-8", null);
			document.AppendChild(declaration);
			XmlElement root = document.CreateElement(NodeTitles[XmlNodes.Root]);

			foreach (InsuredPerson person in list)
			{
				XmlElement personElement = document.CreateElement(NodeTitles[XmlNodes.InsuredPerson]);
				XmlElement id = document.CreateElement(NodeTitles[XmlNodes.Id]);
				XmlElement firstName = document.CreateElement(NodeTitles[XmlNodes.FirstName]);
				XmlElement lastName = document.CreateElement(NodeTitles[XmlNodes.LastName]);
				XmlElement age = document.CreateElement(NodeTitles[XmlNodes.Age]);
				XmlElement phoneNumber = document.CreateElement(NodeTitles[XmlNodes.PhoneNumber]);
				XmlElement registrationDate = document.CreateElement(NodeTitles[XmlNodes.RegistrationDate]);

				id.InnerText = person.Id.ToString();
				firstName.InnerText = person.Firstname;
				lastName.InnerText = person.LastName;
				age.InnerText = person.Age.ToString();
				phoneNumber.InnerText = person.PhoneNumber;
				registrationDate.InnerText = person.RegistrationDate.ToString();

				personElement.AppendChild(id);
				personElement.AppendChild(firstName);
				personElement.AppendChild(lastName);
				personElement.AppendChild(age);
				personElement.AppendChild(phoneNumber);
				personElement.AppendChild(registrationDate);

				root.AppendChild(personElement);
			}

			document.AppendChild(root);
			document.Save(fileName);
		}
	}
}
