namespace Lab2.Data
{
    public class Person
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }

        private bool? isAdult = null;
        private string westernZodiac = null;
        private string chineseZodiac = null;
        private bool? isBirthday = null;

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.BirthDate = birthDate;
            CalculateProperties();
        }

        public Person(string firstName, string lastName, string email) : this(firstName, lastName, email, DateTime.MinValue)
        {
        }

        public Person(string firstName, string lastName, DateTime birthDate) : this(firstName, lastName, null, birthDate)
        {
        }

        public bool IsAdult => isAdult ?? false;
        public string WesternZodiac => westernZodiac;
        public string ChineseZodiac => chineseZodiac;
        public bool IsBirthday => isBirthday ?? false;

        public void CalculateVariableProperties()
        {
            var age = CalculateAge();
            if (age < 0)
                throw new Exception("Person hasn't born yet");
            if (age > 135)
                throw new Exception("Person cannot be older then 135 years");
            isAdult = age >= 18;

            isBirthday = DateTime.Now.Month == BirthDate.Month && DateTime.Now.Day == BirthDate.Day;
        }

        private int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            if (BirthDate.Date > today.AddYears(-age)) age--;

            return age;
        }

        private void CalculateProperties()
        {
            CalculateVariableProperties();

            westernZodiac = GetWesternZodiac();

            chineseZodiac = GetChineseZodiac();
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
