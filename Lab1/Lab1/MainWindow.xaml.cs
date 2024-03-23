using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            Person person;
            try
            {
                var birthDate = datePicker.SelectedDate;
                if (birthDate == null)
                {
                    throw new Exception("Date cannot be null!");
                }

                person = new Person(birthDate.Value);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string caption = "Error";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBox.Show(message, caption, buttons);
                return;
            }

            textBlockAge.Text = $"Age: {person.Age}";

            if (person.IsBirthday())
                textBlockBirthday.Text = "Happy Birthday!";
            else
                textBlockBirthday.Text = string.Empty;

            textBlockWesternZodiac.Text = $"Western Zodiac: {person.WesternZodiac}";
            textBlockChineseZodiac.Text = $"Chinese Zodiac: {person.ChineseZodiac}";
        }
    }

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