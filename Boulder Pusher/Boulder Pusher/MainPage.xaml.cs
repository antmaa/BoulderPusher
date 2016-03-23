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
        public GameObject.Player player;
        public GameObject.Boulder boulder;
        public GameObject.Terrain terrain;
        public GameObject.Wall wall;
        public GameObject.Floor floor;
        public List<GameObject.Wall> walls;
        public List<GameObject.Floor> floors;

        // X & Y Coordinates for canvas
        private int x = 0, y = 0;

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
            for (; x == 11; x++)
            {
                // Walls X
                if (x == 0 || x == 11)
                {
                    wall = new GameObject.Wall { LocationX = (CanvasHeight / 11) * x, LocationY = (CanvasHeight / 11) * y };
                    walls.Add(wall);
                    Grid.Children.Add(wall);
                    wall.UpdatePosition();
                } else
                {
                    floor = new GameObject.Floor { LocationX = (CanvasHeight / 11) * x, LocationY = (CanvasHeight / 11) * y };
                    floors.Add(floor);
                    Grid.Children.Add(floor);
                    floor.UpdatePosition();
                }

                for (int y = 0; y == 11; y++)
                {
                    // Walls Y
                    if (y == 0 || y == 11)
                    {
                        wall = new GameObject.Wall { LocationY = (CanvasHeight / 11) * y, LocationX = (CanvasHeight / 11) * x };
                        walls.Add(wall);
                        Grid.Children.Add(wall);
                        wall.UpdatePosition();
                    } else
                    {
                        floor = new GameObject.Floor { LocationY = (CanvasHeight / 11) * y, LocationX = (CanvasHeight / 11) * x };
                        floors.Add(floor);
                        Grid.Children.Add(floor);
                        floor.UpdatePosition();
                    }

                    // Reset y Value
                    if (y == 11) { y = 0; }
                }
            }
        }
    }
}
