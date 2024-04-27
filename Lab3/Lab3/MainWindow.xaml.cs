using Lab3.Data;
using System.Windows;
using System.Windows.Controls;

namespace Lab3
{
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
            try
            {
                var birthDate = birthDatePicker.SelectedDate ?? throw new Exception("Date cannot be null!");

                ArgumentException.ThrowIfNullOrWhiteSpace(firstNameTextBox.Text, "First name cannot be empty");
                ArgumentException.ThrowIfNullOrWhiteSpace(lastNameTextBox.Text, "Last name cannot be empty");
                ArgumentException.ThrowIfNullOrWhiteSpace(emailTextBox.Text, "Email cannot be empty");

                var person = new Person(firstNameTextBox.Text, lastNameTextBox.Text, emailTextBox.Text, birthDate);

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
            catch (Exception ex)
            {
                textBlockFirstName.Text = "";
                textBlockLastName.Text = "";
                textBlockEmail.Text = "";
                textBlockBirthday.Text = "";
                textBlockIsAdult.Text = "";
                textBlockWesternZodiac.Text = "";
                textBlockChineseZodiac.Text = "";
                textBlockIsBirthday.Text = "";

                string message = ex.Message;
                string caption = "Error";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBox.Show(message, caption, buttons);

                return;
            }
        }
    }
}