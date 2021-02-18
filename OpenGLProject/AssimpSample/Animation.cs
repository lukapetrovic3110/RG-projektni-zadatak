using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace PF1S8v1
{
    class Animation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private World world = null;
        private DispatcherTimer leftBolidTimer;
        private DispatcherTimer rightCarTimer;
        private DispatcherTimer camTimer;
        public Animation(World w)
        {
            world = w;
        }
        public void LeftBolidAnimation(object sender, EventArgs e)
        {
            world.LeftTranslateZ -= 2.0f;
            world.LightTranslate -= 2.0f;
            if (world.LeftTranslateZ <= -40.0f)
            {
                leftBolidTimer.Stop();
            }
        }

        public void RightCarAnimation(object sender, EventArgs e)
        {
            world.RightTranslateZ -= 2.0f;
            if (world.RightTranslateZ <= -40.0f)
            {
                rightCarTimer.Stop();
                camTimer.Stop();
                AnimationNotActive = true;
                world.CamBackAndRefresh();
                
            }
        }

        public void CamAnimation(object sender, EventArgs e)
        {
            if (world.EyeZ > (world.CenterZ + 70.0f))
                world.EyeZ -= 15.0f;
            else
                world.EyeY += 4.0f;

        }

        private bool animationNotActive = true;

        public bool AnimationNotActive
        {
            get
            {
                return animationNotActive;
            }
            set
            {
                animationNotActive = value;
                OnPropertyChanged("AnimationNotActive");
            }
        }


        public void StartAnimation()
        {
            AnimationNotActive = false;
            world.CamAnimation();
            leftBolidTimer = new DispatcherTimer();
            leftBolidTimer.Interval = TimeSpan.FromMilliseconds(10);
            leftBolidTimer.Tick += new EventHandler(LeftBolidAnimation);

            rightCarTimer = new DispatcherTimer();
            rightCarTimer.Interval = TimeSpan.FromMilliseconds(25);
            rightCarTimer.Tick += new EventHandler(RightCarAnimation);

            camTimer = new DispatcherTimer();
            camTimer.Interval = TimeSpan.FromMilliseconds(25);
            camTimer.Tick += new EventHandler(CamAnimation);

            leftBolidTimer.Start();

            rightCarTimer.Start();

            camTimer.Start();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }    
        }
    }
}
