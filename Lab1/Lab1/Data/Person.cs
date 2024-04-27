using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Data
{
    public class Person
    {
        public DateTime BirthDate { get; private set; }
        public int Age { get; private set; }
        public string WesternZodiac { get; private set; }
        public string ChineseZodiac { get; private set; }



        public Person(DateTime birthdayDate)
        {
            BirthDate = birthdayDate;
            Age = CalculateAge();

            if (Age < 0)
                throw new Exception("Person hasn't born yet");
            if (Age > 135)
                throw new Exception("Person cannot be older then 135 years");

            WesternZodiac = GetWesternZodiac();
            ChineseZodiac = GetChineseZodiac();
        }

        public bool IsBirthday()
        {
            var today = DateTime.Today;
            return BirthDate.Day == today.Day && BirthDate.Month == today.Month;
        }

        private int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            if (BirthDate.Date > today.AddYears(-age)) age--;

            return age;
        }

        private string GetWesternZodiac()
        {
            int[] zodiacEndDates = { 19, 49, 79, 110, 140, 171, 204, 235, 266, 296, 326, 356 };
            string[] zodiacSigns = { "Capricorn", "Aquarius", "Pisces", "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn" };

            int dayOfYear = BirthDate.DayOfYear - (DateTime.IsLeapYear(BirthDate.Year) && BirthDate.Month > 2 ? 1 : 0);
            int sign = zodiacEndDates.TakeWhile(endDate => dayOfYear > endDate).Count();

            return zodiacSigns[sign];
        }

        private string GetChineseZodiac()
        {
            string[] chineseZodiacSigns = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };
            int zodiacIndex = (BirthDate.Year - 1900) % 12;
            return chineseZodiacSigns[zodiacIndex];
        }
    }
}
