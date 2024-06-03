using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Dane
{
    public class Logger
    {
        private readonly string filePath;
        private readonly object fileLock = new object();

        public Logger(string filePath)
        {
            this.filePath = filePath;
        }

        public async Task LogAsync(IEnumerable<Ball> balls)
        {
            var ballData = new List<BallData>();
            foreach (var ball in balls)
            {
                ballData.Add(new BallData
                {
                    X = ball.X,
                    Y = ball.Y,
                    VelocityX = ball.VelocityX,
                    VelocityY = ball.VelocityY
                });
            }

            string json = JsonConvert.SerializeObject(ballData, Formatting.Indented);

            await Task.Run(() =>
            {
                lock (fileLock)
                {
                    File.AppendAllText(filePath, json + Environment.NewLine);
                }
            });
        }

        private class BallData
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double VelocityX { get; set; }
            public double VelocityY { get; set; }
        }
    }
}
