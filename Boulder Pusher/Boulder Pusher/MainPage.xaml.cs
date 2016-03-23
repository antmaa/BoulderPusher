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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // GameObjects
        private GameObject.Player player;
        private GameObject.Boulder boulder;
        private GameObject.Terrain terrain;

        // Canvas values
        private double CanvasWidth = 550;
        private double CanvasHeight = 550;

        // Music
        private MediaElement mediaElement;

        // Control Booleans
        private bool UpPressed;
        private bool LeftPressed;
        private bool RightPressed;
        private bool DownPressed;

        // Timer Game Loop

        private DispatcherTimer timer;
        public MainPage()
        {
            this.InitializeComponent();

            // Window Size
            ApplicationView.PreferredLaunchWindowingMode
                = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);

            // Get Canvas Size
            CanvasWidth = Grid.Width;
            CanvasHeight = Grid.Height;

            // Adding the Player
            player = new GameObject.Player
            {
                LocationX = (CanvasWidth / 11) * 6,
                LocationY = (CanvasWidth / 11) * 10
            };

            // Printing the Floor&Walls onto the Canvas
            for (int x = 0; x == 11; x++)
            {
                for (int y = 0; y == 11; y++)
                {

                }
            }
        }
    }
}
