using Avalonia;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pong.ViewModels;

public partial class MainWindowViewModel : GameBase
{
    public int Width { get; } = 800;
    public int Height { get; } = 450;

    // Liste des objets à afficher
    public ObservableCollection<GameObject> GameObjects { get; } = new();

    public MainWindowViewModel()
    {
        for (int i = 0; i < 2; i++)
        {
            var ball = new Ball(new Point(Width , Height ));
            GameObjects.Add(ball);

            var prey = new Prey(new Point(Width / 2 - 32, Height / 2 - 32));
            GameObjects.Add(prey);
            
            var plant = new Plant();
            GameObjects.Add(plant);
        }
    }

    protected override void Tick()
{
    // Mettre à jour les Ball et les Prey
    foreach (var ball in GameObjects.OfType<Ball>())
    {
        // Vérifier si une Prey est dans la zone de la Visionchase
        foreach (var prey in GameObjects.OfType<Prey>())
        {
            ball.ChasePreyIfInRange(prey);
        }
        ball.Tick();
    }

    foreach (var prey in GameObjects.OfType<Prey>())
    {
        prey.Tick();
    }

    foreach (var plant in GameObjects.OfType<Plant>())
    {
        plant.Tick();
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
            Rubish rubish = new Rubish(plant.Location); // Crée un Rubish à l'emplacement de la plante
            GameObjects[i] = rubish;
        }
    }
}

}
