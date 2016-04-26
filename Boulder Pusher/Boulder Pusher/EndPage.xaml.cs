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
    /// Displays the player's final score after beating the game.
    /// Also has buttons for exiting the application, going back to the main menu and restarting the game.
    /// </summary>

    public sealed partial class EndPage : Page
    {
        StepTimeViewModel stepTime;

        public EndPage()
        {
            this.InitializeComponent();

            // Window Size
            ApplicationView.PreferredLaunchWindowingMode
                = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to GamePage
            this.Frame.Navigate(typeof(GamePage));
        }

        private void MmButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to MainPage
            this.Frame.Navigate(typeof(MainPage));
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            // Exits the application
            Application.Current.Exit();
        }

        // Receives the StepTime viewmodel from the game for displaying the score on the screen.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is StepTimeViewModel)
            {
                stepTime = (StepTimeViewModel)e.Parameter;
            }
            base.OnNavigatedTo(e);
        }
    }
}
