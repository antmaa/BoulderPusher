using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Boulder_Pusher
{
    class StepTimeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int step;
        private int time;

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

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}