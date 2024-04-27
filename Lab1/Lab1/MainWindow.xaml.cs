using Lab1.Data;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var birthDate = datePicker.SelectedDate ?? throw new Exception("Date cannot be null!");
                var person = new Person(birthDate);

                textBlockAge.Text = $"Age: {person.Age}";

                if (person.IsBirthday())
                    textBlockBirthday.Text = "Happy Birthday!";
                else
                    textBlockBirthday.Text = string.Empty;

                textBlockWesternZodiac.Text = $"Western Zodiac: {person.WesternZodiac}";
                textBlockChineseZodiac.Text = $"Chinese Zodiac: {person.ChineseZodiac}";
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string caption = "Error";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBox.Show(message, caption, buttons);
                return;
            }
        }
    }
}