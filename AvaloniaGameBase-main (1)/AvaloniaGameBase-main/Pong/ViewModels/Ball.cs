using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Pong.ViewModels;

public partial class Ball : GameObject {
    [ObservableProperty]
    private Point velocity = new Point(1.0, 0);

    public Ball(Point location) : base(location) {

    }

    public void Tick() {
        Location = Location + Velocity;
    }
}