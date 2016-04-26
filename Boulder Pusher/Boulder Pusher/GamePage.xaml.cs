using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Boulder_Pusher
{
    /// <summary>
    /// Contains the canvas, which is used for presenting the game to the player.
    /// Also displays the player's step- and time counters.
    /// Has a level reset button and an exit game button.
    /// </summary>

    public sealed partial class GamePage : Page
    {
        // Game
        public Game game { get; set; }

        public GamePage()
        {
            this.InitializeComponent();

            // Window Size
            ApplicationView.PreferredLaunchWindowingMode
                = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);

            // Create a new game
            game = new Game(MyCanvas, this);
        }

        // Reset button for reseting the level if the player gets stuck
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            game.LevelReset();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to MainPage
            this.Frame.Navigate(typeof(MainPage));
            game.bPTheme.Stop();
        }

        // Method used when the player beats the game. Takes the player to the screen which displays their final score
        internal void EndGame(StepTimeViewModel stepTime)
        {
            this.Frame.Navigate(typeof(EndPage), stepTime);
        }
    }
}
