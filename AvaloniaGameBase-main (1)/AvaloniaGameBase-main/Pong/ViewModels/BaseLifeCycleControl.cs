using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using System;

public class BaseLifeCycleControl : UserControl
{
    private int _lifeTimer;
    private int _hungerTimer;
    private DispatcherTimer _timer;

    // Propriété IsAlive pour vérifier si l'objet est encore vivant
    public bool IsAlive => _lifeTimer > 0 || _hungerTimer > 0;

    public event Action? OnLifeCycleEnd; // Événement de fin de cycle de vie

    public BaseLifeCycleControl(int lifeDuration, int hungerDuration)
    {
        _lifeTimer = lifeDuration;
        _hungerTimer = hungerDuration;

        // Configure le DispatcherTimer
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += Timer_Tick;
    }

    public void StartTimer()
    {
        _timer.Start();
        Console.WriteLine("Timer démarré.");
    }

    public void StopTimer()
    {
        _timer.Stop();
        Console.WriteLine("Timer arrêté.");
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (_lifeTimer > 0)
        {
            _lifeTimer--;
            Console.WriteLine($"Vie restante : {_lifeTimer}s");
        }
        else if (_hungerTimer > 0)
        {
            _hungerTimer--;
            Console.WriteLine($"Faim restante : {_hungerTimer}s");
        }
        else
        {
            // Arrêter le timer et appeler l'événement de fin de cycle
            _timer.Stop();
            OnLifeCycleEnd?.Invoke();
            RemoveFromParent();
        }
    }

    private void RemoveFromParent()
    {
        if (Parent is Panel panel)
        {
            panel.Children.Remove(this); // 'this' fait référence à l'instance actuelle de Ball
            Console.WriteLine("L'élément a été supprimé.");
        }
        else
        {
            Console.WriteLine("Le contrôle n'a pas de parent Panel.");
        }
    }

    // Nouvelle méthode pour augmenter la durée de vie
    public void IncreaseLife(int additionalLife)
    {
        if (_lifeTimer > 0)
        {
            _lifeTimer += additionalLife; // Ajouter de la vie
            Console.WriteLine($"La durée de vie a été augmentée de {additionalLife} secondes.");
        }
    }
}
