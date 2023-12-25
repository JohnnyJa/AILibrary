using System.Numerics;

namespace AILibrary.Pathfinding.Node;

// ReSharper disable once InconsistentNaming
public class NodeXY 
{
    public NodeXY(Vector2 position)
    {
        Position = position;
    }

    public Vector2 Position { get; set; }
}