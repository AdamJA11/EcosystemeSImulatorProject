using System.Collections.Generic;
using Avalonia.Input;

namespace Pong;

// Contient un ensemble (HashSet) qui contient l'ensemble des touches enfoncées
internal static class Keyboard
{
    // Le HashSet est mis à jour dans MainWindow.axaml.cs
    public static readonly HashSet<Key> Keys = new();

    public static bool IsKeyDown(Key key)
    {
        return Keys.Contains(key);
    }
}