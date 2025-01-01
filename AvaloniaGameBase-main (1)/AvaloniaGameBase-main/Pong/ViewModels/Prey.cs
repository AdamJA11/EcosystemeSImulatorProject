using System;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Pong.ViewModels;

public partial class Prey : GameObject
{
    private Poop<Prey> _poop;
    private readonly BaseLifeCycleControl _lifeCycleControl;
    private readonly BaseLifeCycleControl _directionChangeCycleControl; // Nouveau timer pour changer la direction

    [ObservableProperty]
    private Point velocity = new Point(1.0, 0); // Vitesse initiale

    public bool IsAlive => _lifeCycleControl.IsAlive; // Accès à la propriété IsAlive

    public Prey(Point location) : base(location)
    {
        _poop = new Poop<Prey>(this, interval: 8);
        // Créer une instance de BaseLifeCycleControl pour la gestion de la vie
        _lifeCycleControl = new BaseLifeCycleControl(lifeDuration: 30, hungerDuration: 5);
        _lifeCycleControl.OnLifeCycleEnd += HandleLifeCycleEnd;
        _lifeCycleControl.StartTimer();

        // Créer une nouvelle instance de BaseLifeCycleControl pour changer la direction toutes les 2 secondes
        _directionChangeCycleControl = new BaseLifeCycleControl(lifeDuration: 1, hungerDuration: 0); // Durée de 2 secondes pour chaque changement de direction
        _directionChangeCycleControl.OnLifeCycleEnd += ChangeDirectionRandomly;
        _directionChangeCycleControl.StartTimer();
    }

    public void Tick()
    {
        if (_lifeCycleControl.IsAlive)
        {
            double newX = Location.X + Velocity.X;
            double newY = Location.Y + Velocity.Y;

            // Vérifier les limites horizontales
            if (newX < 0)
                newX = 800; // Réapparaît à droite
            else if (newX > 800)
                newX = 0; // Réapparaît à gauche

            // Vérifier les limites verticales
            if (newY < 0)
                newY = 450; // Réapparaît en bas
            else if (newY > 450)
                newY = 0; // Réapparaît en haut

            // Mettre à jour la position
            Location = new Point(newX, newY);
        }
    }

    private void HandleLifeCycleEnd()
    {
        // Lorsque le cycle de vie se termine, transformer en viande
        Meat meat = TransformToMeat();
        Console.WriteLine("La Prey a été transformée en viande.");
    }

    private void ChangeDirectionRandomly()
    {
        // Changer aléatoirement la direction toutes les 2 secondes
        Random random = new Random();
        double randomX = random.NextDouble() * 4 - 2;  // -1 à 1 pour direction aléatoire
        double randomY = random.NextDouble() * 4 - 2;  // -1 à 1 pour direction aléatoire

        Velocity = new Point(randomX, randomY); // Appliquer la nouvelle vitesse
        Console.WriteLine($"Nouvelle direction : {Velocity}");

        // Redémarrer le timer de 2 secondes après chaque changement de direction
        _directionChangeCycleControl.StartTimer();
    }

    public Meat TransformToMeat()
    {
        return new Meat(Location); // Crée une instance de Meat à l'emplacement de la Prey
    }
    public Rubish TryGenerateRubish(double deltaTime)
    {
        return _poop.GenerateRubish(deltaTime);
    }
}
