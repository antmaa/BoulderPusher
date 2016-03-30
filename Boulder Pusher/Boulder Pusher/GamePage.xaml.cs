﻿using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Boulder_Pusher
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        // Canvas values
        private double CanvasWidth = 550;
        private double CanvasHeight = 550;

        // Control Booleans
        private bool UpPressed;
        private bool LeftPressed;
        private bool RightPressed;
        private bool DownPressed;

        // Game
        private Game game;

        public GamePage()
        {
            this.InitializeComponent();

            // Window Size
            ApplicationView.PreferredLaunchWindowingMode
                = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);

            // Used in Ball and Paddle
            CanvasWidth = MyCanvas.Width;
            CanvasHeight = MyCanvas.Height;

            // Create a new game
            game = new Game(MyCanvas);
            game.StartGame();
        }
    }
}