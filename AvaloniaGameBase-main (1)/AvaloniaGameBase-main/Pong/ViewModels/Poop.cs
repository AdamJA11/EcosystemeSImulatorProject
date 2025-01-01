using System;
using Avalonia;

namespace Pong.ViewModels;

public class Poop<T> where T : GameObject
{
    private readonly T _owner;
    private readonly int _interval; // Intervalle en secondes
    private double _timer;

    public Poop(T owner, int interval)
    {
        _owner = owner;
        _interval = interval;
        _timer = 0;
    }

    public Rubish GenerateRubish(double deltaTime)
    {
        _timer += deltaTime;

        if (_timer >= _interval)
        {
            _timer -= _interval;
            return new Rubish(_owner.Location);
        }

        return null;
    }
}
