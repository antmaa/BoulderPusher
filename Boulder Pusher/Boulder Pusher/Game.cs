using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Boulder_Pusher
{
    class Game
    {
        // Timer
        private DispatcherTimer timer;

        // Canvas
        private Canvas canvas;

        // Game Objects
        GameObject.Player player;
        GameObject.Boulder boulder;
        GameObject.Terrain terrain;
        GameObject.Wall wall;
        GameObject.Exit exit;
        List<UserControl> Entities = new List<UserControl>();

        // Audio
        public MediaElement bPTheme;

        // Debug timer
        DispatcherTimer debtim;

        // Movement initialisation
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.Up:
                    Debug.WriteLine("Up pressed!");
                    player.MoveUp();
                    break;

                case Windows.System.VirtualKey.Left:
                    player.MoveLeft();
                    break;

                case Windows.System.VirtualKey.Right:
                    player.MoveRight();
                    
                    break;

                case Windows.System.VirtualKey.Down:
                    player.MoveDown();
                    break;

                default:
                    break;
            }
        }
        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.Up:
                    Debug.WriteLine("Up Released!");
                    break;

                case Windows.System.VirtualKey.Left:
                    break;

                case Windows.System.VirtualKey.Right:
                    break;

                case Windows.System.VirtualKey.Down:
                    break;

                default:
                    break;
            }
        }



        public int[,] pBT =
            {
                { 4,4,4,4,4,5,4,4,4,4,4 },
                { 4,0,0,0,0,2,0,0,0,0,4 },
                { 4,0,0,0,0,0,0,0,0,0,4 },
                { 4,0,3,0,3,0,0,3,0,0,4 },
                { 4,0,3,3,3,0,0,3,0,0,4 },
                { 4,0,3,0,3,0,0,3,0,0,4 },
                { 4,0,0,0,0,0,0,0,0,0,4 },
                { 4,0,0,0,0,0,0,0,0,0,4 },
                { 4,0,0,0,0,0,0,0,0,0,4 },
                { 4,0,0,0,0,1,0,0,0,0,4 },
                { 4,4,4,4,4,4,4,4,4,4,4 }
            };

        // List of GameObjects for map generation: 0=Empty, 1=player, 2=Boulder, 3=terrain...
        //public List<int>[,] pBT; pBT = playerBoulderTerrain

        // Constructor
        public Game(Canvas canvas)
        {
            this.canvas = canvas;
            CreatePBT();
            LoadAudio();
            debtim = new DispatcherTimer();
            //StartGame();
            debtim.Tick += Debtim_Tick;
            debtim.Interval = new TimeSpan(0,0,0,1);
            debtim.Start();

            // Key Listeners
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
        }

        private void Debtim_Tick(object sender, object e)
        {
            Debug.WriteLine(player.X + " " + player.Y);
        }

        // Print level
        public void CreatePBT()
        {


            for (int i = 0; i <= 10; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    // Block position
                    int x = (50) * i; // 0, 50, 100...
                    int y = (50) * j; // 0, 50, 100...
                    // What to generate?
                    if (pBT[j, i] == 1) // Player
                    {
                        player = new GameObject.Player
                        {
                            LocationX = x,
                            LocationY = y,
                            X = i,
                            Y = j
                        };
                        canvas.Children.Add(player);
                        Entities.Add(player);
                        player.UpdatePosition();
                    }
                    else if (pBT[j, i] == 2) // Boulder
                    {
                        boulder = new GameObject.Boulder
                        {
                            LocationX = x,
                            LocationY = y,
                            X = i,
                            Y = j
                        };
                        canvas.Children.Add(boulder);
                        Entities.Add(boulder);
                        boulder.UpdatePosition();
                    }
                    else if (pBT[j, i] == 3) // Terrain
                    {
                        terrain = new GameObject.Terrain
                        {
                            LocationX = x,
                            LocationY = y,
                            X = i,
                            Y = j
                        };
                        canvas.Children.Add(terrain);
                        Entities.Add(terrain);
                        terrain.UpdatePosition();
                    }
                    else if (pBT[j, i] == 4) // Wall
                    {
                        wall = new GameObject.Wall
                        {
                            LocationX = x,
                            LocationY = y,
                            X = i,
                            Y = j
                        };
                        canvas.Children.Add(wall);
                        Entities.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 5) // Exit
                    {
                        exit = new GameObject.Exit
                        {
                            LocationX = x,
                            LocationY = y,
                            X = i,
                            Y = j
                        };
                        canvas.Children.Add(exit);
                        Entities.Add(exit);
                        exit.UpdatePosition();
                    }// if 0, generate nothing
                }
            }
        }

        // Start game
        /*public void StartGame()
        {
            while(true)
            {
                if (UpPressed) player.MoveUp();

                if (DownPressed) player.MoveDown();

                if (LeftPressed) player.MoveLeft();

                if (RightPressed) player.MoveRight();

                await player.UpdatePosition();
            }
        }*/

        // Load Audio
        public async void LoadAudio()
        {
            bPTheme = new MediaElement();
            bPTheme.IsLooping = true;
            bPTheme.AutoPlay = true;
            StorageFolder folder =
                await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            StorageFile file =
                await folder.GetFileAsync("BPTheme.wav");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            bPTheme.SetSource(stream, file.ContentType);
        }



    }
}