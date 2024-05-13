using System.ComponentModel;
using System.Drawing;

namespace Dane
{
    public class Ball : INotifyPropertyChanged
    {
        private double x;
        private double y;
        public double X
        {
            get => x;
            set
            {
                if (x != value)
                {
                    x = value;
                    OnPropertyChanged(nameof(X));
                    UpdateRect();
                }
            }
        }

        public double Y
        {
            get => y;
            set
            {
                if (y != value)
                {
                    y = value;
                    OnPropertyChanged(nameof(Y));
                    UpdateRect();
                }
            }
        }

        public double VelocityX { get; set; }
        public double VelocityY { get; set; }

        private Rectangle collisionRect;
        public Rectangle CollisionRect => collisionRect;

        public double Mass = 5.0;

        private void UpdateRect()
        {
            collisionRect.X = (int)Math.Round(X);
            collisionRect.Y = (int)Math.Round(Y);
        }
        public Ball()
        {
            collisionRect = new Rectangle((int)Math.Round(X), (int)Math.Round(Y), 76, 76);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
