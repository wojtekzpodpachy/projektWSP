using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Dane;
using Logika;

namespace Project.ViewModel
{
    public class BallViewModel 
    {
     

        public ObservableCollection<Ball> Balls { get; set; } = new ObservableCollection<Ball>();
        private BallLogic ballLogic = new BallLogic();
        private DispatcherTimer timer = new DispatcherTimer();
        private Random random = new Random();

        public BallViewModel()
        {
            //InitializeBalls(5);
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += async (s, e) => await MoveBallsAsync2();
            //timer.Start();
        }

        public void InitializeBalls(int numberOfBalls)  //generowanie gejowych kul
        {
            Balls.Clear();
            for (int i = 0; i < numberOfBalls; i++)
            {
                Balls.Add(ballLogic.CreateBall());
            }
            if (!timer.IsEnabled)
            {
                timer.Start();
            }
        }

        private static readonly object collisionLock = new object();
        private async Task MoveBallsAsync2()
        {
            var moveTasks = new List<Task>();
            foreach (var ball in Balls)
            {
                moveTasks.Add(Task.Run(() =>
                {
                    ballLogic.Move(ball);
                    lock (collisionLock)
                    {
                        ballLogic.CheckAndHandleCollision(ball, Balls);
                    }
                }));
            }
            await Task.WhenAll(moveTasks);
        }

       
    }


}
