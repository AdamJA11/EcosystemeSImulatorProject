using Avalonia;

namespace Pong.ViewModels
{
    public class ContactZone
    {
        private readonly Ball _ball;
        private readonly double _radius;

        public ContactZone(Ball ball, double radius = 10.0) // Rayon par défaut de 50 unités
        {
            _ball = ball;
            _radius = radius;
        }

        public bool IsPreyInZone(Prey prey)
        {
            double dx = prey.Location.X - _ball.Location.X;
            double dy = prey.Location.Y - _ball.Location.Y;
            double distanceSquared = dx * dx + dy * dy;

            return distanceSquared <= _radius * _radius;
        }
    }
}
