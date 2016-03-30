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


        // Relay Player Position to Canvas
        public void UpdatePosition()
        {
            Debug.WriteLine("Player loc: " + Canvas.LeftProperty + " " + Canvas.TopProperty);
            SetValue(Canvas.LeftProperty, LocationX);
            SetValue(Canvas.TopProperty, LocationY);
        }

        public Player()
        {
            this.InitializeComponent();

            Width = 50;
            Height = 50;
        }

        public int a = 5;
        public int b = 9;


        public int[,] MoveUp( int[,] pBT)
        {
            int c;
            int e;
            
            c = b - 1;
            if (pBT[a,c] == 2)
            {   c = b - 2;
                e = b;
                b = b - 1;
                
                pBT[a, b] = 1;
                pBT[a, c] = 2;
                pBT[a, e] = 0;

            }
            else if (pBT[a, c] == 0)
            {
                pBT[a, b] = 0;
                
                pBT[a, c] = 1;
            }

            return pBT;



        }
        public void MoveY(int v)
        {

        }
    }
}
