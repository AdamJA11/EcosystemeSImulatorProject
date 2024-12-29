using System;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Pong.ViewModels;

public partial class Plant : GameObject
{
    private readonly BaseLifeCycleControl _lifeCycleControl; // Gestion du cycle de vie

    public bool IsAlive => _lifeCycleControl.IsAlive; // Indique si la plante est encore en vie

    public Plant() : base(GenerateRandomLocation()) // Position initiale aléatoire
    {
        // Créer une instance de BaseLifeCycleControl pour la gestion de la durée de vie
        _lifeCycleControl = new BaseLifeCycleControl(lifeDuration: 15, hungerDuration: 6);
        _lifeCycleControl.StartTimer(); // Lancement du timer
    }

    public void Tick()
    {
        // La méthode Tick peut inclure d'autres comportements futurs si nécessaire
        if (!_lifeCycleControl.IsAlive)
        {
            Console.WriteLine("La plante est morte.");
        }
    }

    private static Point GenerateRandomLocation()
    {
        // Générer une position aléatoire dans les limites du jeu
        Random random = new Random();
        double randomX = random.NextDouble() * 800; // Limite horizontale
        double randomY = random.NextDouble() * 450; // Limite verticale
        return new Point(randomX, randomY);
    }
}
