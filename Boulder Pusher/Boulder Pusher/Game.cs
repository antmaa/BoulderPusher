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
    /// <summary>
    /// Contains the main components of the game itself.
    /// Responsible for loading audio, generating the levels and game objects.
    /// Also listens to player's input on the keyboard and keeps tally of steps taken as well as the time in seconds
    /// </summary>

    public class Game
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
        public List<GameObject.Boulder> Boulds;
        public List<GameObject.Terrain> Terrs;
        public List<GameObject.Wall> Walls;
        public List<GameObject.Exit> Door;

        // level
        private int level = 0;

        // Audio
        public MediaElement bPTheme;
        public MediaElement bPMove;
        public MediaElement bPMove2;
        public MediaElement bPMove3;


        // Step counter and timer
        public StepTimeViewModel StepTime;
        DispatcherTimer timer = new DispatcherTimer();

        public GamePage gamePage;

        // Constructor
        public Game(Canvas canvas, GamePage gamePage)
        {
            this.canvas = canvas;
            this.gamePage = gamePage;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            CreatePBT();
            LoadAudio();
            StepTime = new StepTimeViewModel();

            // Key Listeners
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
        }

        private void Timer_Tick(object sender, object e)
        {
            StepTime.Time++;
            if (level == 6)
            {
                timer.Stop();
            }
        }

        // Level creation --------------------------------------------------------------------------
        // 2D int lists of GameObjects for map generation: 0 = Empty, 1 = Player, 2 = Boulder, 3 = terrain,
        // 4-10 = Various Walls, 11 = Back wall (empty but has collision) and 12 = Exit
        public int[,] pBT =
            {
                { 7,4,4,4,4,12,4,4,4,4,8 },
                { 5,0,0,0,0,2,0,0,0,0,6 },
                { 5,0,0,0,0,3,0,0,0,0,6 },
                { 5,0,0,0,3,3,3,0,0,0,6 },
                { 5,0,0,3,0,3,0,3,0,0,6 },
                { 5,0,0,0,0,3,0,0,0,0,6 },
                { 5,0,0,0,0,3,0,0,0,0,6 },
                { 5,0,0,0,0,3,0,0,0,0,6 },
                { 5,0,0,0,0,0,0,0,0,0,6 },
                { 5,0,0,0,0,1,0,0,0,0,6 },
                { 11,11,11,11,11,11,11,11,11,11,11 }
            };
        public int[,] pBT2 =
             {
                 { 11,11,11,7,4,12,4,8,11,11,11 },
                 { 11,11,7,9,0,0,0,10,8,11,11 },
                 { 11,11,5,2,2,0,2,2,6,11,11 },
                 { 11,11,5,0,2,2,2,0,6,11,11 },
                 { 11,11,5,2,0,0,0,2,6,11,11 },
                 { 11,11,5,0,2,2,2,0,6,11,11 },
                 { 11,11,5,0,0,0,0,0,6,11,11 },
                 { 11,11,5,0,0,0,0,0,6,11,11 },
                 { 11,11,5,0,0,0,0,0,6,11,11 },
                 { 11,11,5,0,0,1,0,0,6,11,11 },
                 { 11,11,11,11,11,11,11,11,11,11,11 }
             };
        public int[,] pBT3 =
            {
                 { 11,7,4,4,4,12,4,4,4,8,11 },
                 { 11,5,0,0,3,0,0,0,0,6,11 },
                 { 11,5,0,0,0,3,3,0,0,6,11 },
                 { 11,5,2,0,2,0,2,0,0,6,11 },
                 { 11,5,0,2,0,2,0,3,3,6,11 },
                 { 11,5,0,0,2,0,2,3,3,6,11 },
                 { 11,5,3,2,0,2,0,0,0,6,11 },
                 { 11,5,0,0,0,0,0,0,0,6,11 },
                 { 11,5,0,0,0,0,0,0,0,6,11 },
                 { 11,5,0,0,0,1,0,0,0,6,11 },
                 { 11,11,11,11,11,11,11,11,11,11,11 }
             };
        public int[,] pBT4 =
            {
                 { 11,11,11,7,4,12,4,8,11,11,11 },
                 { 11,11,7,9,0,0,0,10,8,11,11 },
                 { 11,11,5,0,2,0,2,0,6,11,11 },
                 { 11,11,5,0,2,2,2,0,6,11,11 },
                 { 11,11,5,2,0,2,0,2,6,11,11 },
                 { 11,11,5,0,2,0,2,0,6,11,11 },
                 { 11,11,5,2,0,0,0,2,6,11,11 },
                 { 11,11,5,0,2,2,2,0,6,11,11 },
                 { 11,11,5,0,0,0,0,0,6,11,11 },
                 { 11,11,5,0,0,1,0,0,6,11,11 },
                 { 11,11,5,11,11,11,11,11,6,11,11 }
             };
        public int[,] pBT5 =
            {
                 { 11,11,11,7,4,12,4,8,11,11,11 },
                 { 7,4,4,9,0,2,0,10,4,4,8 },
                 { 5,0,0,0,2,0,2,0,0,0,6 },
                 { 5,0,2,0,2,0,2,0,2,0,6 },
                 { 5,3,0,3,0,3,0,3,0,3,6 },
                 { 5,0,2,0,2,0,2,0,2,0,6 },
                 { 5,3,0,3,0,3,0,3,0,3,6 },
                 { 5,0,2,0,2,0,2,0,2,0,6 },
                 { 5,0,0,2,2,2,2,2,0,0,6 },
                 { 5,0,0,0,0,1,0,0,0,0,6 },
                 { 11,11,11,11,11,11,11,11,11,11,11 }
             };
        public int[,] pBT6 =
            {
                 { 11,11,11,7,4,12,4,8,11,11,11 },
                 { 11,11,7,9,0,0,0,10,8,11,11 },
                 { 11,11,5,2,0,2,0,2,6,11,11 },
                 { 7,4,9,0,2,3,2,0,10,4,8 },
                 { 5,0,2,0,0,3,0,0,2,0,6 },
                 { 5,0,3,2,2,0,2,2,3,0,6 },
                 { 5,0,3,0,2,0,2,0,3,0,6 },
                 { 5,0,3,0,0,0,0,0,3,0,6 },
                 { 5,0,3,3,3,3,3,3,3,0,6 },
                 { 5,0,0,0,0,1,0,0,0,0,6 },
                 { 11,11,11,11,11,11,11,11,11,11,11 }
             };

        // Level switching 
        // if player is in Exit coordinates LevelSwitch method adds level and reloads the map
        // All existing entities are destroyed and the canvas is wiped clean before the new map generates
        public void LevelSwitch()
        {
            if(player.Switch == true)
            {
                
                level++;
                if (level == 1)
                {
                    pBT = pBT2;
                }
                if (level == 2)
                {
                    pBT = pBT3;
                }
                if (level == 3)
                {
                    pBT = pBT4;
                }
                if (level == 4)
                {
                    pBT = pBT5;
                }
                if (level == 5)
                {
                    pBT = pBT6;
                }

                if (level == 6)
                {
                    bPTheme.Stop();
                    gamePage.EndGame(StepTime);
                }

                // Deleting all entities and emptying the entity lists
                Boulds = null;
                Terrs = null;
                Walls = null;
                exit = null;

                player = null;
                boulder = null;
                terrain = null;
                wall = null;
                exit = null;

                // Removing existing items from the canvas
                canvas.Children.Clear();

                // Creating the new level
                CreatePBT();
            }
        }

        // LevelReset resets the map if the player is stuck and cannot progress
        // Activates from the player clicking the reset button on screen
        public void LevelReset()
        {
            // Deleting all entities and emptying the entity list
            Boulds = null;
            Terrs = null;
            Walls = null;
            exit = null;

            player = null;
            boulder = null;
            terrain = null;
            wall = null;
            exit = null;

            // Removing existing items from the canvas
            canvas.Children.Clear();

            // Creating the new level
            CreatePBT();
        }

        // Create level
        // Checks what element is in the (i,j) coordinates of the level's 2D integer list
        // Generates the appropriate entity
        public void CreatePBT()
        {
            // Lists of GameObjects introduced. Used for collision detection
            Boulds = new List<GameObject.Boulder>();
            Terrs = new List<GameObject.Terrain>();
            Walls = new List<GameObject.Wall>();
            Door = new List<GameObject.Exit>();
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
                    else if (pBT[j, i] == 4) // Generate Wall(top)
                    {
                        wall = new GameObject.Wall(pBT[j, i], x, y, i, j);
                        canvas.Children.Add(wall);
                        Walls.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 5) // Generate Wall(left)
                    {
                        wall = new GameObject.Wall(pBT[j, i], x, y, i, j);
                        canvas.Children.Add(wall);
                        Walls.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 6) // Generate Wall(right)
                    {
                        wall = new GameObject.Wall(pBT[j, i], x, y, i, j);
                        canvas.Children.Add(wall);
                        Walls.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 7) // Generate Wall(top-left corner)
                    {
                        wall = new GameObject.Wall(pBT[j, i], x, y, i, j);
                        canvas.Children.Add(wall);
                        Walls.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 8) // Generate Wall(top-right corner)
                    {
                        wall = new GameObject.Wall(pBT[j, i], x, y, i, j);
                        canvas.Children.Add(wall);
                        Walls.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 9) // Generate Wall(top-left reverse corner)
                    {
                        wall = new GameObject.Wall(pBT[j, i], x, y, i, j);
                        canvas.Children.Add(wall);
                        Walls.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 10) // Generate Wall(top-right reverse corner)
                    {
                        wall = new GameObject.Wall(pBT[j, i], x, y, i, j);
                        canvas.Children.Add(wall);
                        Walls.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 11) // Generate Wall without a texture. Used for the bottom wall
                    {
                        wall = new GameObject.Wall(pBT[j, i], x, y, i, j);
                        canvas.Children.Add(wall);
                        Walls.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 12) // Generate Exit
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
        // Sends the lists containing the other interactable objects (Walls, boulders etc...) to the player's move functions
        // The player entity then determines whether movement is possible
        // After a move either succeeds or fails, the step counter will increase by one
        // When a key is pressed...
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.Up:
                    player.MoveUp(Boulds, Terrs, Walls, Door);
                    StepTime.Step++;
                    LevelSwitch();                    
                    break;

                case Windows.System.VirtualKey.Left:
                    player.MoveLeft(Boulds, Terrs, Walls, Door);
                    StepTime.Step++;
                    break;

                case Windows.System.VirtualKey.Right:
                    player.MoveRight(Boulds, Terrs, Walls, Door);
                    StepTime.Step++;
                    break;

                case Windows.System.VirtualKey.Down:
                    player.MoveDown(Boulds, Terrs, Walls, Door);
                    StepTime.Step++;
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
                await folder.GetFileAsync("BoulderPusherMixdown(2).wav");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            bPTheme.SetSource(stream, file.ContentType);
        }

       
        // End of class
    }


    // End of namespace
}