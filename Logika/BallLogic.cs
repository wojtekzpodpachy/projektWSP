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
    private const double Margin = 0.5; // Smaller margin for accuracy
    private Random random = new Random();

    public Ball Move(Ball ball)
    {
        ball.X += ball.VelocityX;
        ball.Y += ball.VelocityY;

        // Bounce off canvas edges
        if (ball.X <= OffsetX)
        {
            ball.X = OffsetX;
            ball.VelocityX = Math.Abs(ball.VelocityX);
        }
        else if (ball.X >= CanvasWidth - BallDiameter + OffsetX)
        {
            ball.X = CanvasWidth - BallDiameter + OffsetX;
            ball.VelocityX = -Math.Abs(ball.VelocityX);
        }

        if (ball.Y <= OffsetY)
        {
            ball.Y = OffsetY;
            ball.VelocityY = Math.Abs(ball.VelocityY);
        }
        else if (ball.Y >= CanvasHeight - BallDiameter + OffsetY)
        {
            ball.Y = CanvasHeight - BallDiameter + OffsetY;
            ball.VelocityY = -Math.Abs(ball.VelocityY);
        }

        return ball;
    }

    public Ball CreateBall()
    {
        Ball ball = new Ball
        {
            X = random.Next(OffsetX, CanvasWidth - BallDiameter + OffsetX),
            Y = random.Next(OffsetY, CanvasHeight - BallDiameter + OffsetY),
            VelocityX = 5 * (random.Next(2) == 0 ? 1 : -1),
            VelocityY = 5 * (random.Next(2) == 0 ? 1 : -1)
        };
        return ball;
    }

    public void CheckAndHandleCollision(Ball ball, IList<Ball> balls)
    {
        var rect1 = ball.CollisionRect;
        for (int i = 0; i < balls.Count; i++)
        {
            var b = balls[i];
            if (b == ball) continue;
            var rect2 = b.CollisionRect;
            if (rect1.IntersectsWith(rect2))
            {
                ResolveCollision(ball, b);
            }
        }
    }

    /*private void ResolveCollision(Ball ball1, Ball ball2)
    {
        // Oblicz różnice w położeniu (od lewego górnego rogu)
        double deltaX = ball2.X - ball1.X;
        double deltaY = ball2.Y - ball1.Y;
        double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

        // Zapobieganie przyklejeniu przez zapewnienie minimalnego odstępu
        double overlap = BallDiameter - distance + Margin;
        if (overlap > 0)
        {
            // Oblicz znormalizowaną różnicę
            double normX = deltaX / distance;
            double normY = deltaY / distance;

            // Przesuń kulki, aby zapobiec przyklejeniu
            ball1.X -= normX * overlap / 2;
            ball1.Y -= normY * overlap / 2;
            ball2.X += normX * overlap / 2;
            ball2.Y += normY * overlap / 2;

            // Oblicz względną prędkość w kierunku kolizji
            double relVelocityX = ball1.VelocityX - ball2.VelocityX;
            double relVelocityY = ball1.VelocityY - ball2.VelocityY;
            double dotProduct = relVelocityX * normX + relVelocityY * normY;

            // Wymień prędkości (zasada zachowania pędu)
            double impulse = 2 * dotProduct / (ball1.Mass + ball2.Mass);
            ball1.VelocityX -= impulse * ball2.Mass * normX;
            ball1.VelocityY -= impulse * ball2.Mass * normY;
            ball2.VelocityX += impulse * ball1.Mass * normX;
            ball2.VelocityY += impulse * ball1.Mass * normY;
        }
    }*/
    private void ResolveCollision(Ball ball1, Ball ball2)
    {
        // Oblicz różnice w położeniu (od lewego górnego rogu)
        double deltaX = ball2.X - ball1.X;
        double deltaY = ball2.Y - ball1.Y;
        double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

        // Zapobieganie przyklejeniu przez zapewnienie minimalnego odstępu
        double overlap = BallDiameter - distance + Margin;
        if (overlap > 0)
        {
            double normX = deltaX / distance;
            double normY = deltaY / distance;

            ball1.X -= normX * overlap / 2;
            ball1.Y -= normY * overlap / 2;
            ball2.X += normX * overlap / 2;
            ball2.Y += normY * overlap / 2;

            double velocityComponent1 = (ball1.VelocityX * normX + ball1.VelocityY * normY);
            double velocityComponent2 = (ball2.VelocityX * normX + ball2.VelocityY * normY);
            ball1.VelocityX -= 2 * velocityComponent1 * normX;
            ball1.VelocityY -= 2 * velocityComponent1 * normY;
            ball2.VelocityX -= 2 * velocityComponent2 * normX;
            ball2.VelocityY -= 2 * velocityComponent2 * normY;
           
        }
    }

}
