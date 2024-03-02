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
        private readonly List<Interval> zodiacWest;
        private readonly List<(string, byte)> zodiacChinese;
        public MainWindow()
        {
            zodiacWest = new List<Interval>()
            {
                new ("Aries", new DateOnly(1, 3, 21), new DateOnly(1, 4, 20)),
                new ("Taurus", new DateOnly(1, 4, 21), new DateOnly(1, 5, 20)),
                new ("Gemini", new DateOnly(1, 5, 21), new DateOnly(1, 6, 20)),
                new ("Cancer", new DateOnly(1, 6, 21), new DateOnly(1, 7, 22)),
                new ("Leo", new DateOnly(1, 7, 23), new DateOnly(1, 8, 22)),
                new ("Virgo", new DateOnly(1, 8, 23), new DateOnly(1, 9, 22)),
                new ("Libra", new DateOnly(1, 9, 23), new DateOnly(1, 10, 22)),
                new ("Scorpio", new DateOnly(1, 10, 23), new DateOnly(1, 11, 22)),
                new ("Saggitarius", new DateOnly(1, 11, 23), new DateOnly(1, 12, 21)),
                new ("Capricorn", new DateOnly(1, 12, 22), new DateOnly(2, 1, 19)),
                new ("Aquarius", new DateOnly(2, 1, 20), new DateOnly(2, 2, 19)),
                new ("Capricorn", new DateOnly(2, 2, 20), new DateOnly(2, 3, 20))
            };
            zodiacChinese = new List<(string, byte)>()
            {
                ("Monkey", 0),
                ("Rooster", 1),
                ("Dog", 2),
                ("Pig", 3),
                ("Rat", 4),
                ("Ox", 5),
                ("Tiger", 6),
                ("Hare", 7),
                ("Dragon", 8),
                ("Snake", 9),
                ("Horse", 10),
                ("Ram", 11)
            };
            InitializeComponent();
        }

        private void ButtonPick_Click(object sender, RoutedEventArgs e)
        {
            var date = Picker.SelectedDate;

            if (date == null) { ShowError("Date cannot be null!"); return; }
            if (date > DateTime.Now) { ShowError($"{date} is bigger then now"); return; }
            if (date.Value.AddYears(135) < DateTime.Now) { ShowError($"You cannot be older than 135 years"); return; }

            if (date.Value.Day == DateTime.Now.Day && date.Value.Month == DateTime.Now.Month) ShowInfo("Happy birthday!!!");
            else ShowInfo("");

            var text = "";
            var firstDate = DateOnly.FromDateTime(date.Value.AddYears(-date.Value.Year + 1));
            var secondDate = DateOnly.FromDateTime(date.Value.AddYears(-date.Value.Year + 2));
            foreach (var interval in zodiacWest)
            {
                if ((firstDate > interval.Start && firstDate < interval.End) ||
                    (secondDate > interval.Start && secondDate < interval.End))
                    text = $"West: {interval.Name}";
            }
            var k = (byte)(date.Value.Year % 12);
            foreach (var zodiac in zodiacChinese)
            {
                if (k == zodiac.Item2)
                    text += $"\r\nChinese: {zodiac.Item1}";
            }
            ShowZodiac(text);
        }

        private void ShowError(string message)
        {
            Zodiac.Text = "";
            TextMain.Foreground = Brushes.Red;
            TextMain.Text = message;
        }

        private void ShowInfo(string message)
        {
            Zodiac.Text = "";
            TextMain.Foreground = Brushes.Black;
            TextMain.Text = message;
        }

        private void ShowZodiac(string message)
        {
            Zodiac.Text = message;
        }

        private class Interval(string name, DateOnly start, DateOnly end)
        {
            public string Name { get; } = name;
            public DateOnly Start { get; } = start;
            public DateOnly End { get; } = end;
        }
    }
}