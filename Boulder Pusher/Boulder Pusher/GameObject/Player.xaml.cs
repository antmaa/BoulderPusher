using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Boulder_Pusher.GameObject
{
    /// <summary>
    /// Player GameObject. Contains the player's X and Y coordinates and location on the canvas.
    /// Also contains the player's move functions
    /// </summary>

    public sealed partial class Player : UserControl
    {
        // Player's location on the canvas (Example: 300px , 250px)
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        public bool Switch { get; set; }
        public int level { get; set; }

        // Boolean used for determining possibility of movement
        private bool canMove;

        // Player's location on the 2D Map used in level generation (Example: 2 , 6)
        // Also used for calculating movement and collision
        public int X { get; set; }
        public int Y { get; set; }

        // Constructor
        // Sets the player entity's size
        public Player()
        {
            this.InitializeComponent();

            Width = 50;
            Height = 50;
        }

        // Relays player position to canvas
        public void UpdatePosition()
        {
            SetValue(Canvas.LeftProperty, LocationX);
            SetValue(Canvas.TopProperty, LocationY);
        }

        // Move methods for the player
        // Trying to move up sends entity lists to MovePlayer function to check if movement is possible
        // Then acts accordingly
        public void MoveUp
            (
            List<GameObject.Boulder> boulds,
            List<GameObject.Terrain> terrs,
            List<GameObject.Wall> walls,
            List<GameObject.Exit> door
            )
        {
            SpriteSheetOffset.Y = 0;
            canMove = MovePlayer(X, Y - 1, boulds, terrs, walls, door); // Sends the desired location and lists
            if (canMove == true)    // If movement is posible
            {
                Y--;                // Adjusts player's Y value
                LocationY = Y * 50; // Also updates location on canvas

                SpriteSheetOffset.Y = 0;
                UpdatePosition();   // Draws the player in their new location
            } else
            {
                return;             // If movement is not possible, does nothing
            }
        }

        // Follows the same principles described in MoveUp
        public void MoveDown
            (
            List<GameObject.Boulder> boulds,
            List<GameObject.Terrain> terrs,
            List<GameObject.Wall> walls,
            List<GameObject.Exit> door
            )

        {
            SpriteSheetOffset.Y = -50;
            canMove = MovePlayer(X, Y + 1, boulds, terrs, walls, door);
            if (canMove == true)
            {
                Y++;
                LocationY = Y * 50;
                UpdatePosition();
            }
            else
            {
                return;
            }
        }

        // Follows the same principles described in MoveUp
        public void MoveLeft
            (
            List<GameObject.Boulder> boulds,
            List<GameObject.Terrain> terrs,
            List<GameObject.Wall> walls,
            List<GameObject.Exit> door
            )
        {
            SpriteSheetOffset.Y = -100;
            canMove = MovePlayer(X - 1, Y, boulds, terrs, walls, door);
            if (canMove == true)
            {
                X--;
                LocationX = X * 50;
                UpdatePosition();
            }
            else
            {
                return;
            }
        }

        // Follows the same principles described in MoveUp
        public void MoveRight
            (
            List<GameObject.Boulder> boulds,
            List<GameObject.Terrain> terrs,
            List<GameObject.Wall> walls,
            List<GameObject.Exit> door
            )
        {
            SpriteSheetOffset.Y = -150;
            canMove = MovePlayer(X + 1, Y, boulds, terrs, walls, door);
            if (canMove == true)
            {
                X++;
                LocationX = X * 50;
                UpdatePosition();
            }
            else
            {
                return;
            }
        }

        // Function for moving or pushing boulders, possible to be unable to move
        private bool MovePlayer(int DestX, int DestY,
            List<GameObject.Boulder> boulds,
            List<GameObject.Terrain> terrs,
            List<GameObject.Wall> walls,
            List<GameObject.Exit> door)
        {
            // Sets the possibility for movement as true unless obstructed
            bool Clear = true;

            // Begins looping through the entity lists
            while (true)
            {
                // Checks the destination for boulders
                foreach (Boulder boulder in boulds)
                {
                    // If a boulder in the list has the same coordinates as the desired location
                    // Calls the similar function, which determines if the boulders desired location is obstructed
                    if (DestX == boulder.X && DestY == boulder.Y)
                    {
                        // Example: Player is in 2,2, boulder in 2,3. DestX remains 2. DestY = Boulder's Y + (Boulder's Y - Player's Y)
                        // Therefore DestY = 3 + (3 - 2) = 4
                        Clear = MoveBoulder(DestX + (DestX - X), (DestY + (DestY - Y)), boulds, terrs, walls, door);
                        if (Clear == true)
                        {
                            // Tells the boulder to move to said location (2,4 in the example above)
                            boulder.Push(DestX + (DestX - X), (DestY + (DestY - Y))); 
                        }
                        return Clear;
                    } // If the path is obstructed, nothing will happen
                }

                // Similar to boulder movement, however, without the possibility to push
                foreach (Terrain terrain in terrs)
                {
                    if (DestX == terrain.X && DestY == terrain.Y)
                    {
                        Clear = false;
                        return Clear;
                    }
                }

                // Similar to boulder movement, however, without the possibility to push
                foreach (Wall wall in walls)
                {
                    if (DestX == wall.X && DestY == wall.Y)
                    {
                        Clear = false;
                        return Clear;
                    }
                }

                // Similar to boulder movement, however, will take you to the next level
                foreach (Exit exit in door)
                {
                    if (DestX == exit.X && DestY == exit.Y)
                    {
                        Clear = true;
                        Switch = true;
                        return Clear;
                    }
                }
                return Clear;
            }
        }

        // Checks the path of the boulder that is about to be pushed.
        // Returning value "Clear" determines if the boulder is pushed (true) or cannot be pushed (false)
        private bool MoveBoulder(int PathX, int PathY,
            List<GameObject.Boulder> boulds,
            List<GameObject.Terrain> terrs,
            List<GameObject.Wall> walls,
            List<GameObject.Exit> door)
        {
            bool Clear = true;
            while (Clear == true)
            {
                foreach (Boulder boulder in boulds)
                {
                    if (PathX == boulder.X && PathY == boulder.Y)
                    {
                        Clear = false; // The boulder cannot be pushed, when blocked by other boulders
                        return Clear;
                    }
                }

                foreach (Terrain terrain in terrs)
                {
                    if (PathX == terrain.X && PathY == terrain.Y)
                    {
                        // The boulder cannot be pushed, when blocked by terrain
                        Clear = false;
                        return Clear;
                    }
                }

                foreach (Wall wall in walls)
                {
                    if (PathX == wall.X && PathY == wall.Y)
                    {
                        // The boulder cannot be pushed, when blocked by walls
                        Clear = false;
                        return Clear;
                    }
                }
                foreach (Exit exit in door)
                {
                    if (PathX == exit.X && PathY == exit.Y)
                    {
                        // The boulder cannot be pushed on top of the exit
                        Clear = false;
                        return Clear;
                    }
                }
                return Clear;
            }
            return Clear;
        }
    }
}