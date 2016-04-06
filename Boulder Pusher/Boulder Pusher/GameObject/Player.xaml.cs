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
    public sealed partial class Player : UserControl
    {
        public double LocationX { get; set; }
        public double LocationY { get; set; }

        private bool canMove;

        public int X { get; set; }
        public int Y { get; set; }

        private int currentframe = 0;
        private int frameheight = 50;
        // Relay Player Position to Canvas
        public void UpdatePosition()
        {
            SetValue(Canvas.LeftProperty, LocationX);
            SetValue(Canvas.TopProperty, LocationY);
        }

        public Player()
        {
            this.InitializeComponent();

            Width = 50;
            Height = 50;
        }

        // Move methods for the player. First checks the players

        public void MoveUp(
            List<GameObject.Boulder> boulds,
            List<GameObject.Terrain> terrs,
            List<GameObject.Wall> walls,
            List<GameObject.Exit> door)
        {
            canMove = MovePlayer(X, Y - 1, boulds, terrs, walls, door);
            if (canMove == true)
            {
                Y--;
                LocationY = Y * 50;

                SpriteSheetOffset.Y = 0;
                UpdatePosition();
            } else
            {
                return;
            }
        }

        public void MoveDown(
            List<GameObject.Boulder> boulds,
            List<GameObject.Terrain> terrs,
            List<GameObject.Wall> walls,
            List<GameObject.Exit> door)
        {
            canMove = MovePlayer(X, Y + 1, boulds, terrs, walls, door);
            if (canMove == true)
            {
                Y++;
                LocationY = Y * 50;

                SpriteSheetOffset.Y = 0;
                UpdatePosition();
            }
            else
            {
                return;
            }
        }
        public void MoveLeft(
            List<GameObject.Boulder> boulds,
            List<GameObject.Terrain> terrs,
            List<GameObject.Wall> walls,
            List<GameObject.Exit> door)
        {
            canMove = MovePlayer(X - 1, Y, boulds, terrs, walls, door);
            if (canMove == true)
            {
                X--;
                LocationX = X * 50;

                SpriteSheetOffset.Y = 0;
                UpdatePosition();
            }
            else
            {
                return;
            }
        }
        public void MoveRight(
            List<GameObject.Boulder> boulds,
            List<GameObject.Terrain> terrs,
            List<GameObject.Wall> walls,
            List<GameObject.Exit> door)
        {
            canMove = MovePlayer(X +1 , Y, boulds, terrs, walls, door);
            if (canMove == true)
            {
                X++;
                LocationX = X * 50;

                SpriteSheetOffset.Y = 0;
                UpdatePosition();
            }
            else
            {
                return;
            }
        }

        // Moving or pushing boulders, possible to be unable to move
        private bool MovePlayer(int DestX, int DestY,
            List<GameObject.Boulder> boulds,
            List<GameObject.Terrain> terrs,
            List<GameObject.Wall> walls,
            List<GameObject.Exit> door)
        {
            bool Clear = true;
            for (;;)
            {
                foreach (Boulder boulder in boulds)
                {
                    if (DestX == boulder.X && DestY == boulder.Y)
                    {
                        Clear = MoveBoulder(DestX + (DestX - X), (DestY + (DestY - Y)), boulds, terrs, walls, door);
                        if (Clear == true)
                        {
                            boulder.Push(DestX + (DestX - X), (DestY + (DestY - Y)));
                        }
                        return Clear;
                    }


                }

                foreach (Terrain terrain in terrs)
                {
                    if (DestX == terrain.X && DestY == terrain.Y)
                    {
                        Clear = false;
                        return Clear;
                    }
                }

                foreach (Wall wall in walls)
                {
                    if (DestX == wall.X && DestY == wall.Y)
                    {
                        Clear = false;
                        return Clear;
                    }
                }

                foreach (Exit exit in door)
                {
                    if (DestX == exit.X && DestY == exit.Y)
                    {
                        Clear = false;
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
                        Clear = false;
                        return Clear;
                    }
                }

                foreach (Terrain terrain in terrs)
                {
                    if (PathX == terrain.X && PathY == terrain.Y)
                    {
                        Clear = false;
                        return Clear;
                    }
                }

                foreach (Wall wall in walls)
                {
                    if (PathX == wall.X && PathY == wall.Y)
                    {
                        Clear = false;
                        return Clear;
                    }
                }

                /*if (Clear == true)
                {
                    foreach (Boulder boulder in ents)
                    {
                        if (PathX - (PathX - X) == boulder.X && PathY - (PathY - Y) == boulder.Y)
                        {
                            Boulder.Push(PathX, PathY);
                        }
                    }
                    return Clear;
                }*/
                return Clear;
            }
            return Clear;
        }
    }
}/* else foreach (var boulder in ents)
                        {
                            if (PathX - (PathX - X) == boulder.X && PathY - (PathY - Y) == boulder.Y)
                            {
                                Boulder.Push(PathX, PathY);
                            }
                        }*/