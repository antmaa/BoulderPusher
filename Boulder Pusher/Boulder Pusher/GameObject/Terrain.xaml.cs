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
    public sealed partial class Terrain : UserControl
    {
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        
        public int X { get; set; }
        public int Y { get; set; }

        // Relay Terrain Position to Canvas
        public void UpdatePosition()
        {
            Debug.WriteLine("Boulder loc: " + Canvas.LeftProperty + " " + Canvas.TopProperty);
            SetValue(Canvas.LeftProperty, LocationX);
            SetValue(Canvas.TopProperty, LocationY);
        }
        public Terrain()
        {
            this.InitializeComponent();

            Width = 50;
            Height = 50;
        }
    }
}
