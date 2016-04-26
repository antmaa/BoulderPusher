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
    /// Boulder GameObject. Contains the boulder's X and Y coordinates and location on the canvas.
    /// Also contains the boulder's move function,
    /// which updates the boulder's location on the canvas when pushed
    /// </summary>

    public sealed partial class Boulder : UserControl
    {
        public double LocationX { get; set; }
        public double LocationY { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        // Relay Boulder Position to Canvas
        public void UpdatePosition()
        {
            SetValue(Canvas.LeftProperty, LocationX);
            SetValue(Canvas.TopProperty, LocationY);
        }

        public Boulder()
        {
            this.InitializeComponent();

            Width = 50;
            Height = 50;
        }

        // Push function. Called when the player collides with a boulder and its path is not obstructed
        public void Push(int x, int y)
        {
            LocationX = x * 50;
            LocationY = y * 50;
            X = x;
            Y = y;
            UpdatePosition();
        }
    }
}
