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
        //StepTimeViewModel StepTime = new StepTimeViewModel();
        DispatcherTimer timer = new DispatcherTimer();

        // Constructor
        public Game(Canvas canvas)
        {
            this.canvas = canvas;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            CreatePBT();
            LoadAudio();
            LoadMoveAudio();
            LoadMoveAudio2();
            LoadMoveAudio3();

            // Key Listeners
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
        }

        private void Timer_Tick(object sender, object e)
        {
           // StepTime.time++;
        }

        // Level creation --------------------------------------------------------------------------
        // List of GameObjects for map generation: 0=Empty, 1=player, 2=Boulder, 3=terrain, 4=wall and 5=exit
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
        public int[,] pBT2 =
             {
                 { 4,4,4,4,4,5,4,4,4,4,4 },
                 { 4,4,4,4,0,0,0,4,4,4,4 },
                 { 4,4,4,2,2,0,2,2,4,4,4 },
                 { 4,4,4,0,2,2,2,0,4,4,4 },
                 { 4,4,4,2,0,0,0,2,4,4,4 },
                 { 4,4,4,0,2,2,2,0,4,4,4 },
                 { 4,4,4,0,0,0,0,0,4,4,4 },
                 { 4,4,4,0,0,0,0,0,4,4,4 },
                 { 4,4,4,0,0,0,0,0,4,4,4 },
                 { 4,4,4,0,0,1,0,0,4,4,4 },
                 { 4,4,4,4,4,4,4,4,4,4,4 }
             };
        public int[,] pBT3 =
            {
                 { 4,4,4,4,4,5,4,4,4,4,4 },
                 { 4,4,0,0,3,0,0,0,0,4,4 },
                 { 4,4,0,0,0,3,3,0,0,4,4 },
                 { 4,4,2,0,2,0,2,0,0,4,4 },
                 { 4,4,0,2,0,2,0,3,3,4,4 },
                 { 4,4,0,0,2,0,2,3,3,4,4 },
                 { 4,4,3,2,0,2,0,0,0,4,4 },
                 { 4,4,0,0,0,0,0,0,0,4,4 },
                 { 4,4,0,0,0,0,0,0,0,4,4 },
                 { 4,4,0,0,0,1,0,0,0,4,4 },
                 { 4,4,4,4,4,4,4,4,4,4,4 }
             };
        public int[,] pBT4 =
            {
                 { 4,4,4,4,4,5,4,4,4,4,4 },
                 { 4,4,4,4,0,0,0,4,4,4,4 },
                 { 4,4,4,0,2,0,2,0,4,4,4 },
                 { 4,4,4,0,2,2,2,0,4,4,4 },
                 { 4,4,4,2,0,2,0,2,4,4,4 },
                 { 4,4,4,0,2,0,2,0,4,4,4 },
                 { 4,4,4,2,0,0,0,2,4,4,4 },
                 { 4,4,4,0,2,2,2,0,4,4,4 },
                 { 4,4,4,0,0,0,0,0,4,4,4 },
                 { 4,4,4,0,0,1,0,0,4,4,4 },
                 { 4,4,4,4,4,4,4,4,4,4,4 }
             };
        public int[,] pBT5 =
            {
                 { 4,4,4,4,4,5,4,4,4,4,4 },
                 { 4,4,0,0,3,0,0,0,0,4,4 },
                 { 4,2,0,0,0,3,3,0,0,4,4 },
                 { 4,4,2,0,2,0,2,0,0,4,4 },
                 { 4,4,0,2,0,2,0,3,3,4,4 },
                 { 4,4,0,0,2,0,0,3,3,4,4 },
                 { 4,4,3,2,0,0,0,0,0,4,4 },
                 { 4,4,0,0,0,0,0,0,0,4,4 },
                 { 4,4,0,0,0,0,0,0,0,4,4 },
                 { 4,4,0,0,0,1,0,0,0,4,4 },
                 { 4,4,4,4,4,4,4,4,4,4,4 }
             };

        // Level switching 
        // if player is in Exit coordinates Level switch method adds level and 
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
        }

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

        // Print level
        // Checks what element is in the (i,j) coordinates of the level's 2D integer list
        // Generates the appropriate entity
        public void CreatePBT()
        {
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
                    else if (pBT[j, i] == 4) // Generate Wall
                    {
                        wall = new GameObject.Wall(pBT[j, i], x, y, i, j);
                        canvas.Children.Add(wall);
                        Walls.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 5) // Generate Wall
                    {
                        wall = new GameObject.Wall(pBT[j, i], x, y, i, j);
                        canvas.Children.Add(wall);
                        Walls.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 6) // Generate Wall
                    {
                        wall = new GameObject.Wall(pBT[j, i], x, y, i, j);
                        canvas.Children.Add(wall);
                        Walls.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 7) // Generate Wall
                    {
                        wall = new GameObject.Wall(pBT[j, i], x, y, i, j);
                        canvas.Children.Add(wall);
                        Walls.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 8) // Generate Wall
                    {
                        wall = new GameObject.Wall(pBT[j, i], x, y, i, j);
                        canvas.Children.Add(wall);
                        Walls.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 9) // Generate Exit
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
                    //.step++;
                    bPMove.Play();
                    LevelSwitch();                    
                    break;

                case Windows.System.VirtualKey.Left:
                    player.MoveLeft(Boulds, Terrs, Walls, Door);
                    // StepTime.step++;
                    bPMove2.Play();
                    break;

                case Windows.System.VirtualKey.Right:
                    player.MoveRight(Boulds, Terrs, Walls, Door);
                    //StepTime.step++;
                    bPMove3.Play();
                    break;

                case Windows.System.VirtualKey.Down:
                    player.MoveDown(Boulds, Terrs, Walls, Door);
                    // StepTime.step++;
                    bPMove.Play();
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
        public async void LoadMoveAudio()
        {

            bPMove = new MediaElement();
            bPMove.AutoPlay = false;
                StorageFolder folder =
    await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
                StorageFile file =
    await folder.GetFileAsync("BOOP.wav");
                var stream = await file.OpenAsync(FileAccessMode.Read);
            bPMove.SetSource(stream, file.ContentType);
                        
        }
        public async void LoadMoveAudio2()
        {

            bPMove2 = new MediaElement();
            bPMove2.AutoPlay = false;
            StorageFolder folder =
await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            StorageFile file =
await folder.GetFileAsync("BIIP.wav");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            bPMove2.SetSource(stream, file.ContentType);

        }
        public async void LoadMoveAudio3()
        {

            bPMove3 = new MediaElement();
            bPMove3.AutoPlay = false;
            StorageFolder folder =
await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            StorageFile file =
await folder.GetFileAsync("BUUP.wav");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            bPMove3.SetSource(stream, file.ContentType);

        }


        // End of class
    }


    // End of namespace
}