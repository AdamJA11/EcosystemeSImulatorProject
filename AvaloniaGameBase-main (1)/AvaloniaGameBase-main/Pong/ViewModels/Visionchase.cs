using System;
using Avalonia;

namespace Pong.ViewModels;

public class Visionchase
{
    private const double VisionRadius = 100; // Rayon de la zone de détection
    private Ball _ball;

    public Visionchase(Ball ball)
    {
        _ball = ball;
    }

    // Vérifie si une Prey est dans la zone de détection de la Visionchase
    public bool IsPreyInRange(Prey prey)
    {
        double dx = _ball.Location.X - prey.Location.X;
        double dy = _ball.Location.Y - prey.Location.Y;
        double distance = Math.Sqrt(dx * dx + dy * dy);
        return distance <= VisionRadius;
    }

    // Change la direction de la Ball vers la Prey si elle est détectée dans la zone
    public void ChasePreyIfInRange(Prey prey)
    {
        if (IsPreyInRange(prey))
        {
            double dx = prey.Location.X - _ball.Location.X;
            double dy = prey.Location.Y - _ball.Location.Y;

            // Normaliser la direction pour éviter une vitesse trop rapide
            double magnitude = Math.Sqrt(dx * dx + dy * dy);
            double normalizedX = dx / magnitude;
            double normalizedY = dy / magnitude;

            // Modifier la direction de la Ball pour se diriger vers la Prey
            _ball.Velocity = new Point(normalizedX * 2, normalizedY * 2); // Multiplier pour augmenter la vitesse
        }
    }
}
