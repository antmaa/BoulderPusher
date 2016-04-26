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
    /// Wall GameObject. Contains the wall's X and Y coordinates and location on the canvas.
    /// </summary>

    public sealed partial class Wall : UserControl
    {
        public double LocationX { get; set; }
        public double LocationY { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
       
        // Relay Wall Position to Canvas
        public void UpdatePosition()
        {
            SetValue(Canvas.LeftProperty, LocationX);
            SetValue(Canvas.TopProperty, LocationY);
        }

        // Receives the wall's location's int[,] value. Used for determining which wall is generated
        // Also receives the X and Y coordinates as well as the canvas location
        public Wall( int Position, int xX, int yY, int iI, int jJ )
        {
            this.InitializeComponent();
            Height = 50;
            Width = 50;
            // Checks which wall is being created and adjust the SpriteSheetOffset to match the desired wall
            SpriteSheetOffset.Y = (Position - 4) * 50 * (-1);
            LocationX = xX;
            LocationY = yY;
            X = iI;
            Y = jJ;
        }
    }
}
