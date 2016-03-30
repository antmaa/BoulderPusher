﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        // List of GameObjects for map generation: 0=Empty, 1=player, 2=Boulder, 3=terrain...
        //public List<int>[,] pBT; pBT = playerBoulderTerrain

        // Constructor
        public Game(Canvas canvas)
        {
            this.canvas = canvas;
            CreatePBT();
        }

        // Print level
        private void CreatePBT()
        {
            int[,] pBT =
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

            for (int i = 0; i <= 10; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    // Block position
                    int x = (50) * i; // 0, 50, 100...
                    int y = (50) * j; // 0, 50, 100...
                    Debug.WriteLine(i + " " + j);
                    // What to generate?
                    if (pBT[j, i] == 1) // Player
                    {
                        GameObject.Player player = new GameObject.Player
                        {
                            LocationX = x,
                            LocationY = y
                        };
                        canvas.Children.Add(player);
                        player.UpdatePosition();
                    }
                    else if (pBT[j, i] == 2) // Boulder
                    {
                        GameObject.Boulder boulder = new GameObject.Boulder
                        {
                            LocationX = x,
                            LocationY = y
                        };
                        canvas.Children.Add(boulder);
                        boulder.UpdatePosition();
                    }
                    else if (pBT[j, i] == 3) // Terrain
                    {
                        GameObject.Terrain terrain = new GameObject.Terrain
                        {
                            LocationX = x,
                            LocationY = y
                        };
                        canvas.Children.Add(terrain);
                        terrain.UpdatePosition();
                    }
                    else if (pBT[j, i] == 4) // Wall
                    {
                        GameObject.Wall wall = new GameObject.Wall
                        {
                            LocationX = x,
                            LocationY = y
                        };
                        canvas.Children.Add(wall);
                        wall.UpdatePosition();
                    }
                    else if (pBT[j, i] == 5) // Exit
                    {
                        GameObject.Exit exit = new GameObject.Exit
                        {
                            LocationX = x,
                            LocationY = y
                        };
                        canvas.Children.Add(exit);
                        exit.UpdatePosition();
                    }// if 0, generate nothing
                }
            }
        }

        // Start game
        public void StartGame()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0,1,0); // 60 fps
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            Debug.WriteLine("Game");
            // Include player and boulder movement later!!!                      <---------------------------
            CreatePBT();
        }

        // Game loop
        /*private void Timer_Tick(object sender, object e)
        {
            Debug.WriteLine("Game");
            ball.Move();
            CheckCollision();
            IsGameOver();
        }*/

        // Game Over
        /*private void IsGameOver()
        {
            // All blocks rekt
            if (blocks.Count == 0)
            {
                Debug.WriteLine("Game over - Good job!");
                timer.Stop();
            }
            // Ball below paddle
            if (ball.LocationY > paddle.LocationY)
            {
                Debug.WriteLine("Game over - Better luck next time!");
                timer.Stop();
            }
        }*/

        /*private void CheckCollision()
        {
            // Ball and paddle
            Rect rect = ball.GetRect();
            rect.Intersect(paddle.GetRect());
            if (!rect.IsEmpty)
            {
                // Paddle 100 px
                // ball position 0-100
                double ballPosition = ball.LocationX - paddle.LocationX;
                // -0.5 <-> 0.5
                double hitPercent = (ballPosition / paddle.Width) - 0.5;
                // Set ball speed
                ball.SetSpeed(hitPercent);
            }

            // Ball and blocks
            foreach (Block block in blocks)
            {
                Rect ballRect = ball.GetRect();
                ballRect.Intersect(block.GetRect());
                if (!ballRect.IsEmpty)
                {
                    // Remove block from list-collection
                    blocks.Remove(block);
                    // Remove from canvas
                    canvas.Children.Remove(block);
                    // Ball SpeedX and SpeedY?
                    double ballCenter = ball.LocationX + (ball.Width / 2);
                    if (ballCenter < block.LocationX || ballCenter > block.LocationX + block.Width)
                    {
                        ball.SpeedX *= -1;
                    }
                    else
                    {
                        ball.SpeedY *= -1;
                    }
                    break;
                }
            }
        }*/
    }
}