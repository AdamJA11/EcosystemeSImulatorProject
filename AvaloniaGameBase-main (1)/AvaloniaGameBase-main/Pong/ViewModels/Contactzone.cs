using System;
using Avalonia;

namespace Pong.ViewModels
{
    public class ContactZone
    {
        private Ball _ball; // Référence à la Ball

        public ContactZone(Ball ball)
        {
            _ball = ball;
        }

        // Vérifie si une Prey est dans la zone de contact
        public bool IsInContact(Prey prey)
        {
            // Calculer la distance entre la Ball et la Prey pour vérifier le contact
            double distance = Math.Sqrt(Math.Pow(_ball.Location.X - prey.Location.X, 2) + Math.Pow(_ball.Location.Y - prey.Location.Y, 2));
            
            // Supposons que la zone de contact est de rayon 50 (vous pouvez ajuster ce rayon selon votre logique)
            return distance < 50; // Si la distance est inférieure à 50, cela signifie que la Prey est dans la zone de contact
        }

        // Vérifie et effectue des actions supplémentaires quand la Prey est en contact
        public void CheckPreyContact(Prey prey)
        {
            if (IsInContact(prey))
            {
                // Logique supplémentaire pour gérer les conséquences du contact
                Console.WriteLine("La Prey est dans la zone de contact de la Ball!");
            }
        }
    }
}
