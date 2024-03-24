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

namespace Lab3
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

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(firstNameTextBox);
                ArgumentNullException.ThrowIfNull(lastNameTextBox);
                ArgumentNullException.ThrowIfNull(emailTextBox);
                ArgumentNullException.ThrowIfNull(birthDatePicker);
                proceedButton.IsEnabled = !string.IsNullOrWhiteSpace(firstNameTextBox.Text) &&
                                          !string.IsNullOrWhiteSpace(lastNameTextBox.Text) &&
                                          !string.IsNullOrWhiteSpace(emailTextBox.Text) &&
                                          birthDatePicker.SelectedDate != null;
            }
            catch
            {
                if (proceedButton != null)
                    proceedButton.IsEnabled = false;
            }
        }

        private void BirthDatePicker_SelectedDateChanged(object sender, EventArgs e)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(firstNameTextBox);
                ArgumentNullException.ThrowIfNull(lastNameTextBox);
                ArgumentNullException.ThrowIfNull(emailTextBox);
                ArgumentNullException.ThrowIfNull(birthDatePicker);
                proceedButton.IsEnabled = !string.IsNullOrWhiteSpace(firstNameTextBox.Text) &&
                                          !string.IsNullOrWhiteSpace(lastNameTextBox.Text) &&
                                          !string.IsNullOrWhiteSpace(emailTextBox.Text) &&
                                          birthDatePicker.SelectedDate != null;
            }
            catch
            {
                if (proceedButton != null)
                    proceedButton.IsEnabled = false;
            }
        }

        private async void ProceedButton_Click(object sender, RoutedEventArgs e)
        {
            Person person;
            try
            {
                var birthDate = birthDatePicker.SelectedDate;
                if (birthDate == null)
                    throw new Exception("Date cannot be null!");

                if (string.IsNullOrWhiteSpace(firstNameTextBox.Text))
                    throw new Exception("First name cannot be empty");

                if (string.IsNullOrWhiteSpace(lastNameTextBox.Text))
                    throw new Exception("Last name cannot be empty");

                if (string.IsNullOrWhiteSpace(emailTextBox.Text))
                    throw new Exception("Email cannot be empty");

                person = new Person(firstNameTextBox.Text, lastNameTextBox.Text, emailTextBox.Text, birthDate.Value);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string caption = "Error";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBox.Show(message, caption, buttons);
                textBlockFirstName.Text = "";
                textBlockLastName.Text = "";
                textBlockEmail.Text = "";
                textBlockBirthday.Text = "";
                textBlockIsAdult.Text = "";
                textBlockWesternZodiac.Text = "";
                textBlockChineseZodiac.Text = "";
                textBlockIsBirthday.Text = "";
                return;
            }

            await Task.Run(() => person.CalculateVariableProperties());

            textBlockFirstName.Text = "First name: " + person.FirstName;
            textBlockLastName.Text = "Last name: " + person.LastName;
            textBlockEmail.Text = "Email: " + person.Email;
            textBlockBirthday.Text = "Birthday: " + person.BirthDate.ToShortDateString();
            textBlockIsAdult.Text = "Is adult: " + person.IsAdult.ToString();
            textBlockWesternZodiac.Text = "Western zodiac: " + person.WesternZodiac;
            textBlockChineseZodiac.Text = "Chinese zodiac: " + person.ChineseZodiac;
            textBlockIsBirthday.Text = person.IsBirthday ? "Today is birthday!!!" : "";
        }
    }

    public class Person 
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Email
        {
            get => email;
            private set
            {
                if (IsValidEmail(value))
                    email = value;
                else
                    throw new InvalidEmailException("Email is not valid");
            }
        }
        public bool IsAdult => isAdult ?? false;
        public string WesternZodiac => westernZodiac;
        public string ChineseZodiac => chineseZodiac;
        public bool IsBirthday => isBirthday ?? false;

        private string email = null;
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

        private bool IsValidEmail(string email)
        {
            if (email.Split("@").Count() != 2)
                return false;

            var s1 = email.Split("@")[1];
            if (s1.Split(".").Count() == 1)
                return false;

            var s2 = email.Split("@")[1].Split(".");
            foreach (var s in s2)
                if (s.Length == 0)
                    return false;

            return true;
        }

        public void CalculateVariableProperties()
        {
            var age = CalculateAge();
            if (age < 0)
                throw new FutureBirthDateException("Person hasn't born yet");
            if (age > 135)
                throw new DistantPastBirthDateException("Person cannot be older then 135 years");
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

    public class FutureBirthDateException : Exception
    {
        public FutureBirthDateException(string message) : base(message)
        {
        }
    }

    public class DistantPastBirthDateException : Exception
    {
        public DistantPastBirthDateException(string message) : base(message)
        {
        }
    }

    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message)
        {
        }
    }
}