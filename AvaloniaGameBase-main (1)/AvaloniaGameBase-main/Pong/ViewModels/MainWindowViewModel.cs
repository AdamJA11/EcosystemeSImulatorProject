using Avalonia;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using Random = System.Random;

namespace Pong.ViewModels;

public partial class MainWindowViewModel : GameBase
{
    public int Width { get; } = 800;
    public int Height { get; } = 450;

    // Liste des objets à afficher
    public ObservableCollection<GameObject> GameObjects { get; } = new();

    public MainWindowViewModel()
    {
        for (int i = 0; i < 3; i++)
        {
            var ball = new Ball(new Point(Width, Height));
            GameObjects.Add(ball);

            var prey = new Prey(new Point(Width / 2 - 32, Height / 2 - 32));
            GameObjects.Add(prey);

            var plant = new Plant();
            GameObjects.Add(plant);
        }
    }

    protected override void Tick()
    {
        var objectsToRemove = new List<GameObject>(); // Liste temporaire pour suppression
        var newGameObjects = new List<GameObject>();  // Liste pour ajouter des nouveaux objets
        Random randomGenerator = new Random();

        // Gérer la logique des Ball et leur interaction avec Prey
        foreach (var ball in GameObjects.OfType<Ball>())
        {
            foreach (var prey in GameObjects.OfType<Prey>())
            {
                ball.ChasePreyIfInRange(prey);

                if (ball.IsPreyInContactZone(prey))
                {
                    // Appliquer la logique de probabilité (1 chance sur 2)
                    if (randomGenerator.Next(2) == 0) // Retourne 0 ou 1 aléatoirement
                    {
                        objectsToRemove.Add(prey); // Cas 1 : La Prey disparaît
                        ball.ExtendLifetime(5, 3); // Ball gagne 5 secondes de vie
                    }
                    else
                    {
                        ball.FreezeForSeconds(3); // Cas 2 : Ball est figée pendant 3 secondes
                    }
                }
            }

            // Générer du Rubish depuis les Ball
            var rubish = ball.TryGenerateRubish(1.0 / 60.0); // Supposons un tick à 60 FPS
            if (rubish != null)
            {
                newGameObjects.Add(rubish);
            }

            ball.Tick();
        }

        // Gérer les ticks des Prey
        foreach (var prey in GameObjects.OfType<Prey>())
        {
            prey.Tick();

            // Générer du Rubish depuis les Prey
            var rubish = prey.TryGenerateRubish(1.0 / 60.0);
            if (rubish != null)
            {
                newGameObjects.Add(rubish);
            }
        }

        // Gérer les ticks des Plant
        foreach (var plant in GameObjects.OfType<Plant>())
        {
            plant.Tick();
        }

        // Supprimer les objets marqués
        foreach (var obj in objectsToRemove)
        {
            GameObjects.Remove(obj);
        }

        // Transformer les Ball expirées en Meat
        for (int i = 0; i < GameObjects.Count; i++)
        {
            if (GameObjects[i] is Ball ball && ball.IsExpired)
            {
                Meat meat = ball.TransformToMeat();
                GameObjects[i] = meat;
            }
        }

        // Transformer les Prey expirées en Meat
        for (int i = 0; i < GameObjects.Count; i++)
        {
            if (GameObjects[i] is Prey prey && !prey.IsAlive)
            {
                Meat meat = prey.TransformToMeat();
                GameObjects[i] = meat;
            }
        }

        // Transformer les Meat en Rubish si leur timer est expiré
        for (int i = 0; i < GameObjects.Count; i++)
        {
            if (GameObjects[i] is Meat meat && meat.IsTimerExpired())
            {
                Rubish rubish = new Rubish(meat.Location);
                GameObjects[i] = rubish;
            }
        }

        // Transformer les Plant expirées en Rubish
        for (int i = 0; i < GameObjects.Count; i++)
        {
            if (GameObjects[i] is Plant plant && !plant.IsAlive)
            {
                Rubish rubish = new Rubish(plant.Location);
                GameObjects[i] = rubish;
            }
        }

        // Ajouter les nouveaux objets générés
        foreach (var obj in newGameObjects)
        {
            GameObjects.Add(obj);
        }
    }
}
