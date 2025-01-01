using System;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Pong.ViewModels;

public partial class Ball : GameObject
{
    private Poop<Ball> _poop;
    private bool isFrozen;
    private double frozenTimeRemaining;
    private ContactZone _contactZone;
    private static readonly Random RandomGenerator = new Random();
    private Visionchase _visionchase; // Référence à la zone de détection

    [ObservableProperty]
    private Point velocity = new Point(1.0, 0); // Vitesse initiale

    private readonly BaseLifeCycleControl _lifeCycleControl;
    private readonly BaseLifeCycleControl _directionChangeCycleControl;

    public bool IsExpired => !_lifeCycleControl.IsAlive;

    public Ball(Point location) : base(location)
    {
        _poop = new Poop<Ball>(this, interval: 5); // Créer une instance de Poop pour générer du Rubish
        // Créer une instance de Visionchase autour de la Ball
        _visionchase = new Visionchase(this);
         _contactZone = new ContactZone(this);

        // Créer une instance de BaseLifeCycleControl pour la gestion de la vie
        _lifeCycleControl = new BaseLifeCycleControl(lifeDuration: 20, hungerDuration: 0); // Durée de vie de 20 secondes
        _lifeCycleControl.OnLifeCycleEnd += HandleLifeCycleEnd;
        _lifeCycleControl.StartTimer();

        // Créer une nouvelle instance de BaseLifeCycleControl pour changer la direction toutes les 2 secondes
        _directionChangeCycleControl = new BaseLifeCycleControl(lifeDuration: 2, hungerDuration: 0); // Durée de 2 secondes pour chaque changement de direction
        _directionChangeCycleControl.OnLifeCycleEnd += ChangeVelocity;
        _directionChangeCycleControl.StartTimer();
    }

    public void Tick()
    {
        if (IsExpired)
            return; // Si expiré, ne pas se déplacer.

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
        if (isFrozen)
        {
            frozenTimeRemaining -= 1 / 60.0; // Supposons un Tick à 60 FPS
            if (frozenTimeRemaining <= 0)
            {
                isFrozen = false;
            }
            return; // Ne pas exécuter le reste du Tick si figé
        }

        // Autres logiques de Tick pour la balle
        

        // Mettre à jour la position
        Location = new Point(newX, newY);
    }

    private void HandleLifeCycleEnd()
    {
        Console.WriteLine("Ball has expired.");
    }

    private void ChangeVelocity()
    {
        // Générer une nouvelle vitesse aléatoire
        double randomX = RandomGenerator.NextDouble() * 4 - 2; // Vitesse aléatoire entre -2 et 2
        double randomY = RandomGenerator.NextDouble() * 4 - 2; // Vitesse aléatoire entre -2 et 2

        // Mettre à jour la propriété Velocity
        Velocity = new Point(randomX, randomY);
        

        // Redémarrer le timer de changement de direction
        _directionChangeCycleControl.StartTimer();
    }

    public Meat TransformToMeat()
    {
        return new Meat(Location); // Crée une instance de Meat à l'emplacement de la Ball
    }

    // Méthode pour faire chasser la Ball par la Prey si elle est dans la zone de Visionchase
    public void ChasePreyIfInRange(Prey prey)
    {
        _visionchase.ChasePreyIfInRange(prey); // Vérifie et change la direction de la Ball
    }
    public bool IsPreyInContactZone(Prey prey)
    {
        return _contactZone.IsPreyInZone(prey);
    }
    public void FreezeForSeconds(double seconds)
    {
        isFrozen = true;
        frozenTimeRemaining = seconds;
    }
    public void ExtendLifetime(int seconds, int hungerSeconds)
    {
        _lifeCycleControl.IncreaseHungerAndLife(seconds , hungerSeconds );
    }
    public Rubish TryGenerateRubish(double deltaTime)
    {
        return _poop.GenerateRubish(deltaTime);
    }
}
