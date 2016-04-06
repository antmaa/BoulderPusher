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

        public void MoveUp(List<UserControl> entities)
        {
            canMove = MovePlayer(X, Y-1, entities);
            if (canMove == true)
            {
                Y--;
                LocationY = Y * 50;
                UpdatePosition();
            }
        }

        public void MoveDown(List<UserControl> entities)
        {
            canMove = MovePlayer(X, Y + 1, entities);
            if (canMove == true)
            {
                Y++;
                LocationY = Y * 50;
                UpdatePosition();
            }
        }
        public void MoveLeft(List<UserControl> entities)
        {
            canMove = MovePlayer(X - 1, Y, entities);
            if (canMove == true)
            {
                X--;
                LocationY = X * 50;
                UpdatePosition();
            }
        }
        public void MoveRight(List<UserControl> entities)
        {
            canMove = MovePlayer(X + 1, Y, entities);
            if (canMove == true)
            {
                X++;
                LocationY = X * 50;
                UpdatePosition();
            }
        }

        // Moving or pushing boulders, possible to be unable to move
        private bool MovePlayer(int DestX, int DestY, List<UserControl> ents)
        {
            bool Clear = true;
            for (;;)
            {
                foreach (Boulder boulder in ents)
                {
                    if (DestX == boulder.X && DestY == boulder.Y)
                    {
                        Clear = false;
                        return Clear;
                    }
                }

                foreach (Terrain terrain in ents)
                {
                    if (DestX == terrain.X && DestY == terrain.Y)
                    {
                        Clear = false;
                        return Clear;
                    }
                }

                foreach (Wall wall in ents)
                {
                    if (DestX == wall.X && DestY == wall.Y)
                    {
                        Clear = false;
                        return Clear;
                    }
                }


                foreach (Exit exit in ents)
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
        private bool MoveBoulder(int PathX, int PathY, int Amount, List<UserControl> ents)
        {
            bool Clear = true;
            for (;;)
            {
                foreach (Boulder boulder in ents)
                {
                    if (PathX == boulder.X && PathY == boulder.Y)
                    {
                        Clear = false;
                        return Clear;
                    }
                }

                foreach (Terrain terrain in ents)
                {
                    if (PathX == terrain.X && PathY == terrain.Y)
                    {
                        Clear = false;
                        return Clear;
                    }
                }

                foreach (Wall wall in ents)
                {
                    if (PathX == wall.X && PathY == wall.Y)
                    {
                        Clear = false;
                        return Clear;
                    }
                }
                return Clear;
            }
        }
    }
}