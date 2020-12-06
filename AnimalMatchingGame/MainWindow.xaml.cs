using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AnimalMatchingGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            timeTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0s");

            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmojisStore = new List<string>()
            {
                "🐮", "🦄", "🐔", "🐴", "🐷", "🐼", "🐶", "🐹", "🐱", "🦁", "🐯", "🦒", "🦊", "🐗", "🐰", "🐸", "🦓", "🐨", "🐲"
            };

            List<string> animalEmojis = FillListOfAnimalEmojis(animalEmojisStore);

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    int index = random.Next(animalEmojis.Count);

                    string nextEmoji = animalEmojis[index];

                    textBlock.Text = nextEmoji;

                    textBlock.Visibility = Visibility.Visible;

                    animalEmojis.RemoveAt(index);
                }
            }

            timer.Start();
            tenthOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        private List<string> FillListOfAnimalEmojis(List<string> animals)
        {
            Random random = new Random();
            int counter = 0;
            List<string> animalEmojis = new List<string>();

            do
            {
                int index = random.Next(animals.Count);

                string nextEmoji = animals[index];

                animalEmojis.Add(nextEmoji);
                animalEmojis.Add(nextEmoji);

                animals.RemoveAt(index);

                counter++;
            } while (counter < 8);

            return animalEmojis;
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text ==lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}