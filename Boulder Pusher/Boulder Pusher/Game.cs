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
        // Constructor and the main variables ------------------------------------------------------
        // Canvas
        private Canvas canvas;

        // Game Objects
        GameObject.Player player;
        GameObject.Boulder boulder;
        GameObject.Terrain terrain;
        GameObject.Wall wall;
        GameObject.Exit exit;
        public List<GameObject.Boulder> Boulds = new List<GameObject.Boulder>();
        public List<GameObject.Terrain> Terrs = new List<GameObject.Terrain>();
        public List<GameObject.Wall> Walls = new List<GameObject.Wall>();
        public List<GameObject.Exit> Door = new List<GameObject.Exit>();

        // Audio
        public MediaElement bPTheme;

        // Constructor
        public Game(Canvas canvas)
        {
            this.canvas = canvas;
            CreatePBT();
            LoadAudio();

            // Key Listeners
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
        }

        // Level creation --------------------------------------------------------------------------
        // List of GameObjects for map generation: 0=Empty, 1=player, 2=Boulder, 3=terrain...
        // pBT = playerBoulderTerrain
        // Change level names later!!!
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
                { 4,0,0,0,0,0,0,2,0,0,4 },
                { 4,0,0,0,0,1,0,0,0,0,4 },
                { 4,4,4,4,4,4,4,4,4,4,4 }
            };

        // Print level
        // Checks what element is in the (i,j) coordinates of the level's 2D integer list
        // Generates the appropriate entity
        public void CreatePBT()
        {
            for (int i = 0; i <= 10; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    // Block position on the canvas
                    int x = (50) * i; // 0, 50, 100...
                    int y = (50) * j; // 0, 50, 100...
                    // What to generate?
                    if (pBT[j, i] == 1) // Generate Player
                    {
                        player = new GameObject.Player
                        {
                            LocationX = x,
                            LocationY = y,
                            X = i,
                            Y = j
                        };
                        canvas.Children.Add(player);
                        player.UpdatePosition();
                    }
                    else if (pBT[j, i] == 2) // Generate Boulder
                    {
                        boulder = new GameObject.Boulder
                        {
                            LocationX = x,
                            LocationY = y,
                            X = i,
                            Y = j
                        };
                        canvas.Children.Add(boulder);
                        Boulds.Add(boulder);
                        boulder.UpdatePosition();
                    }
                    else if (pBT[j, i] == 3) // Generate Terrain
                    {
                        terrain = new GameObject.Terrain
                        {
                            LocationX = x,
                            LocationY = y,
                            X = i,
                            Y = j
                        };
                        canvas.Children.Add(terrain);
                        Terrs.Add(terrain);
                        terrain.UpdatePosition();
                    }
                    else if (pBT[j, i] == 4) // Generate Wall
                    {
                        wall = new GameObject.Wall
                        {
                            LocationX = x,
                            LocationY = y,
                            X = i,
                            Y = j
                        };
                        canvas.Children.Add(wall);
                        Walls.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 5) // Generate Exit
                    {
                        exit = new GameObject.Exit
                        {
                            LocationX = x,
                            LocationY = y,
                            X = i,
                            Y = j
                        };
                        canvas.Children.Add(exit);
                        Door.Add(exit);
                        exit.UpdatePosition();
                    }// if 0, generate nothing
                }
            }
        }

        // Movement --------------------------------------------------------------------------------
        // Sends the player's Move functions, the lists containing the other interactable objects (Walls, boulders etc...)
        // The player entity then determines whether movement is possible
        // After a move either succeeds or fails, the step counter will increase by one (to be implemented)
        // When a key is pressed...
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.Up:
                    player.MoveUp(Boulds, Terrs, Walls, Door);
                    break;

                case Windows.System.VirtualKey.Left:
                    player.MoveLeft(Boulds, Terrs, Walls, Door);
                    break;

                case Windows.System.VirtualKey.Right:
                    player.MoveRight(Boulds, Terrs, Walls, Door);
                    
                    break;

                case Windows.System.VirtualKey.Down:
                    player.MoveDown(Boulds, Terrs, Walls, Door);
                    break;

                default:
                    break;
            }
        }

        // When a key is no longer pressed...
        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.Up:
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
        
        // -----------------------------------------------------------------------------------------
        // Load Audio function
        // Prepares the song and plays it on a loop
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


        // End of class
    }


    // End of namespace
}