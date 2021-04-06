using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;

namespace Exercise_1
{
    abstract class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Person(string FirstName, string LastName, string Email, string PhoneNumber)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.FullName = FirstName + " " + LastName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
        }
        public static bool ValidateFirstName(string firstName)
        {
            if (firstName.Length >= 1)
            {
                if (isNumeric(firstName)) return true;
            }
            return false;
        }
        public static bool ValidateLastName(string lastname)
        {
            if (lastname.Length >= 1)
            {
                if (isNumeric(lastname)) return true;
            }
            return false;
        }
        public static bool ValidateEmail(string email)
        {
            if (email.Contains("@")) return true;
            else if (email == null) return false;
            return false;
        }
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length < 1) return false;
            foreach (char c in phoneNumber)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ValidateCourse(string course)
        {
            if (string.IsNullOrEmpty(course)) return false;
            else return true;
        }
        public static bool isNumeric(string s)
        {
            foreach (char c in s)
            {
                if ((c >= '0' && c <= '9'))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
