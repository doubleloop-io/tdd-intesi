using System;

namespace BirthdayGreetingsKata
{
    public class Employee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        public bool IsBirthday(DateTime today) =>
            DateOfBirth.Month == today.Month && DateOfBirth.Day == today.Day;
    }
}
