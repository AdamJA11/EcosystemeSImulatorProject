using System;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Timers;

namespace Pong.ViewModels;

public partial class Rubish : GameObject
{
    public Rubish(Point location) : base(location)
    {
        Position = location;
    }

    public Point Position { get; }

    // Cette classe pourrait avoir des propriétés spécifiques à l'image de viande, comme la texture
}
