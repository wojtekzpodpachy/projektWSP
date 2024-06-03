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
    public class BallViewModel /*: INotifyPropertyChanged*/
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Ball> Balls { get; set; } = new ObservableCollection<Ball>();
        private BallLogic ballLogic = new BallLogic();
        private DispatcherTimer timer = new DispatcherTimer();
        private Random random = new Random();
        private Logger logger;
        private DispatcherTimer loggingTimer = new DispatcherTimer();

        public BallViewModel()
        {
            //InitializeBalls(5);
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += async (s, e) => await MoveBallsAsync2();
            loggingTimer.Interval = TimeSpan.FromSeconds(1);
            loggingTimer.Tick += async (s, e) => await LogBallsAsync();
            logger = new Logger("C:\\Users\\mluza\\source\\repos\\ProjektWsp\\ball_log.json");
            //timer.Start();
        }

        public void InitializeBalls(int numberOfBalls)
        {
            Balls.Clear();
            for (int i = 0; i < numberOfBalls; i++)
            {
                /*Balls.Add(new Ball
                {
                    X = random.Next(0, 828 - 76), 
                    Y = random.Next(0, 457 - 76),
                    VelocityX = 5 * (random.Next(2) == 0 ? 1 : -1),
                    VelocityY = 5 * (random.Next(2) == 0 ? 1 : -1)
                });*/
                Balls.Add(ballLogic.CreateBall());
            }
            if (!timer.IsEnabled)
            {
                timer.Start();
            }
            if (!loggingTimer.IsEnabled)
            {
                loggingTimer.Start();
            }
            /* StartLogging();*/
        }

        /*private void MoveBalls()
        {
            foreach (var ball in Balls)
            {
                ballLogic.Move(ball);
            }
            //OnPropertyChanged(nameof(Balls));// to chyba nie jest potrzebne 
        }
        private async Task MoveBallsAsync()
        {
            await Task.Run(() =>
            {
                foreach (var ball in Balls)
                {
                    ballLogic.Move(ball);
                }
            });
        }*/
        private static readonly object collisionLock = new object();
        private async Task MoveBallsAsync2()
        {
           var moveTasks =new List<Task>(); 
            foreach (var ball in Balls)
            {
                moveTasks.Add(Task.Run(() =>
                {
                    ballLogic.Move(ball);
                    lock (collisionLock)
                    {
                        ballLogic.CheckAndHandleCollision(ball,Balls);
                    }
                }));
            }
            await Task.WhenAll(moveTasks);
        }
        private async void StartLogging()
        {
            while (true)
            {
                await logger.LogAsync(Balls);
                await Task.Delay(1000);
            }
        }
        /* protected virtual void OnPropertyChanged(string propertyName)
         {
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
         }*/
        private async Task LogBallsAsync()
        {
            await logger.LogAsync(Balls);
        }
    }


}
