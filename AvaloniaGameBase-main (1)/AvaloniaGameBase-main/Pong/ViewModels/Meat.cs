using System;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Timers;

namespace Pong.ViewModels
{
    public partial class Meat : GameObject
    {
        private Timer _timer;
        private bool _timerExpired = false; // Variable d'état pour indiquer si le timer a expiré
        public Point Position { get; }

        public Meat(Point location) : base(location)
        {
            Position = location;
            StartTimer();
        }

        private void StartTimer()
        {
            // Crée un timer qui appelle la méthode TimerCallback après 5 secondes (5000 ms)
            _timer = new Timer(5000);
            _timer.Elapsed += TimerCallback;
            _timer.AutoReset = false;  // Arrêter après une seule exécution
            _timer.Start();
        }

        // Change le type de retour à void, car l'événement Elapsed ne permet pas de retour de valeur
        private void TimerCallback(object sender, ElapsedEventArgs e)
        {
            // Affiche un message dans la console après l'expiration du timer
            Console.WriteLine($"Le timer de Meat à {Position} a expiré.");

            // Met à jour l'état pour indiquer que le timer a expiré
            _timerExpired = true;
            IsTimerExpired();
        }

        // Méthode pour vérifier si le timer a expiré
        public bool IsTimerExpired()
        {
            return _timerExpired;
        }
    }
}
