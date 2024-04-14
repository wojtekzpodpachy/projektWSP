using System.ComponentModel;

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
                }
            }
        }

        public double VelocityX { get; set; }
        public double VelocityY { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
