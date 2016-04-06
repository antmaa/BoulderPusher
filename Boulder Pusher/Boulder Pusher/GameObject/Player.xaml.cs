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

        public void MoveUp()
        {  
            Y--;
            LocationY = (Y + 1) * 50;
            UpdatePosition();
            SpriteSheetOffset.Y = 0;
        }

        public void MoveDown()
        {
            Y++;
            LocationY = (Y + 1) * 50;
            currentframe = 1;
            SpriteSheetOffset.Y = -50;
            UpdatePosition();

        }
        public void MoveLeft()
        {
            X--;
            LocationX = (X + 1) * 50;
            SpriteSheetOffset.Y = -100;
            UpdatePosition();

        }
        public void MoveRight()
        {
            X++;
            LocationX = (X + 1) * 50;
            SpriteSheetOffset.Y = -150;
            UpdatePosition();

        }
    }
}

