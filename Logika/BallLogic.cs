using Dane;
using System;

namespace Logika;

public class BallLogic
{
    private const int BallDiameter = 76;
    private const int CanvasWidth = 828;
    private const int CanvasHeight = 457;
    private const int OffsetX = 0;
    private const int OffsetY = 0;
    private Random random = new Random();
    public Ball Move(Ball ball)
    {
        ball.X += ball.VelocityX;
        ball.Y += ball.VelocityY;

        if (ball.X <= 0 +OffsetX || ball.X >= CanvasWidth - BallDiameter + OffsetX)
        {
            ball.VelocityX = -ball.VelocityX;
        }
        if (ball.Y <= 0 + OffsetY || ball.Y >= CanvasHeight - BallDiameter + OffsetY)
        {
            ball.VelocityY = -ball.VelocityY;
        }

        return ball;
    }
    public Ball CreateBall()
    {
       Ball ball= new Ball
        {
            X = random.Next(0, 828 - 76),
            Y = random.Next(0, 457 - 76),
            VelocityX = 5 * (random.Next(2) == 0 ? 1 : -1),
            VelocityY = 5 * (random.Next(2) == 0 ? 1 : -1)
        };
        return ball;
    }
}

