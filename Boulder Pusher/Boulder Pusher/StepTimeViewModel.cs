using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// ViewModel for displaying the player's current score on the screen while game is playing
/// </summary>

namespace Boulder_Pusher
{
    public class StepTimeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Step and time variables for keeping tally of the step count and time taken
        private int step;
        private int time;

        // Constructor. Sets the score to zero at the start of the game
        public StepTimeViewModel()
        {
            Step = 0;
            Time = 0;
        }

        public int Step
        {
            get
            {
                return step;
            }
            set
            {
                step = value;
                RaisePropertyChanged();
            }
        }
        public int Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                RaisePropertyChanged();
            }
        }

        // Updates the values on screen when they have changed
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}