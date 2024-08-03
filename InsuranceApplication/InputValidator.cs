using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApplication
{
	/// <summary>
	/// Set of enums used by <see cref="InputValidator"> to match and return particular error message.
	/// </summary>
	public enum ValidationEnums
	{
		NameEmpty, NameNotLetter,
		NumberTooShort, NumberShortNotDigit, NumberLongNotDigit,
		AgeIncorrect,
		Correct
	}

	/// <summary>
	/// Class represents validation mechanism for user inputs. It contains several methods where each of them uses different algorithm to validate user input.
	/// </summary>
	internal class InputValidator
	{
		/// <summary>
		/// Delegate to any <see langword="public"/> method within <see cref="InputValidator"/> class.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public delegate ValidationEnums ValidationMethod(string str);

		/// <summary>
		/// Set of key-value pairs containing various <see langword="string"/> error messages matched to <see cref="ValidationEnums"/>.
		/// </summary>
		public Dictionary<ValidationEnums, string> errorMessages = new Dictionary<ValidationEnums, string>
		{
			[ValidationEnums.NameEmpty] = "Chyba! Zadaná hodnota nesmí být prázdná.",
			[ValidationEnums.NameNotLetter] = "Chyba! Zadaná hodnota nesmí obsahovat mezery, číslice ani jiné speciální znaky.",
			[ValidationEnums.NumberTooShort] = "Chyba! Telefonní číslo musí obsahovat minimálně 9 znaků.",
			[ValidationEnums.NumberShortNotDigit] = "Chyba! 9-místné telefonní číslo smí obsahovat pouze číslice 0 - 9.",
			[ValidationEnums.NumberLongNotDigit] = "Chyba! Pro zadání předvolby je povolen znak +, všechny ostatní znaky smí obsahovat pouze číslice 0 - 9.",
			[ValidationEnums.AgeIncorrect] = "Chyba! Zadaná hodnota smí obsahovat pouze číslice v rozmezí 0 - 100."
		};

		//Public methods

		/// <summary>
		/// Validates a <paramref name="name"/>.
		/// </summary>
		/// <param name="name">A string to be validated</param>
		/// <returns>
		/// <see cref="ValidationEnums.NameEmpty"/> if <paramref name="name"/> is null or empty.<br />
		/// <see cref="ValidationEnums.NameNotLetter"/> if <paramref name="name"/> contains a character that is not categorized as a Unicode letter.<br />
		/// Otherwise returns <see cref="ValidationEnums.Correct"/>.
		/// </returns>
		public ValidationEnums ValidateName(string name)
		{
			name = name.Trim();
			if (string.IsNullOrEmpty(name))
			{
				return ValidationEnums.NameEmpty;
			}
			foreach (char c in name)
			{
				if (!char.IsLetter(c))
				{
					return ValidationEnums.NameNotLetter;
				}
			}
			return ValidationEnums.Correct;
		}

		/// <summary>
		/// Validates a <paramref name="phoneNumber"/>.
		/// </summary>
		/// <param name="phoneNumber"></param>
		/// <returns>
		/// <see cref="ValidationEnums.NumberTooShort"/> <see langword="if"/> <paramref name="phoneNumber"/> is less than 9 characters long.<br />
		/// <see cref="ValidationEnums.NumberShortNotDigit"/> <see langword="else if"/> <paramref name="phoneNumber"/> is exactly 9 characters long
		/// and contains any character that is not categorized as a UnicodeCategory.DecimalDigitNumber.<br />
		/// <see cref="ValidationEnums.NumberLongNotDigit"/><br />
		/// 1) <see langword="else if"/> <paramref name="phoneNumber"/> starts with '+'
		/// and any other character except the first one is not categorized as a UnicodeCategory.DecimalDigitNumber.<br />
		/// 2) <see langword="else"/> <paramref name="phoneNumber"/> contains any character that is not categorized as a UnicodeCategory.DecimalDigitNumber.<br />
		/// Otherwise returns <see cref="ValidationEnums.Correct"/>
		/// </returns>
		public ValidationEnums ValidatePhoneNumber(string phoneNumber)
		{
			phoneNumber = string.Join(string.Empty, phoneNumber.Split(' ', StringSplitOptions.RemoveEmptyEntries));

			//If phoneNumber is less than 9 characters long, it's invalid
			if (phoneNumber.Length < 9)
			{
				return ValidationEnums.NumberTooShort;
			}
			//Else if phoneNumber is exactly 9 characters long, it must be digits only
			else if (phoneNumber.Length == 9)
			{
				if (!StringIsDigitOnly(phoneNumber))
				{
					return ValidationEnums.NumberShortNotDigit;
				}
			}
			//Else if phoneNumber's first character is '+', all other characters must be digits only
			else if (phoneNumber.StartsWith('+'))
			{
				if (!StringIsDigitOnly(phoneNumber.Substring(1)))
				{
					return ValidationEnums.NumberLongNotDigit;
				}
			}
			//Else let's presume that country code was defined as "00" instead of '+'
			else
			{
				if (!StringIsDigitOnly(phoneNumber))
				{
					return ValidationEnums.NumberLongNotDigit;
				}
			}
			return ValidationEnums.Correct;
		}

		/// <summary>
		/// Validates an <paramref name="age"/>.
		/// </summary>
		/// <param name="age"></param>
		/// <returns>
		/// <see cref="ValidationEnums.AgeIncorrect"/> if <paramref name="age"/> didn't succeed at <see cref="int.TryParse(string?, out int)"/>
		/// OR value is out of range 0 - 100.<br />
		/// Otherwise returns <see cref="ValidationEnums.Correct"/>
		/// </returns>
		public ValidationEnums ValidateAge(string age)
		{
			if (!int.TryParse(age, out int a) || (a < 0 || a > 100))
			{
				return ValidationEnums.AgeIncorrect;
			}
			return ValidationEnums.Correct;
		}

		//Private methods

		/// <summary>
		/// Validates <paramref name="str"/> if it contains only characters that are members of the UnicodeCategory.DecimalDigitNumber category.
		/// </summary>
		/// <param name="str">A string to be validated</param>
		/// <returns><see langword="true"/> if all of the characters in <paramref name="str"/> are digits; otherwise <see langword="false"/>.</returns>
		private bool StringIsDigitOnly(string str)
		{
			foreach (char c in str)
			{
				if (!char.IsDigit(c))
				{
					return false;
				}
			}
			return true;
		}
	}
}
