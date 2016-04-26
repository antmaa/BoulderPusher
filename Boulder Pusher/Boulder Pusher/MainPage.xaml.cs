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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Boulder_Pusher
{
    /// <summary>
    /// The main page the application starts from. Contains buttons for navigating to the GamePage,
    /// CreditsPage and exiting the application
    /// </summary>

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Window Size
            ApplicationView.PreferredLaunchWindowingMode
                = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to GamePage
            this.Frame.Navigate(typeof(GamePage));
        }

        private void CreditsButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to CreditsPage
            this.Frame.Navigate(typeof(CreditsPage));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Exits the application
            Application.Current.Exit();
        }        
    }
}
