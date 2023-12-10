namespace AILibrary.Pathfinding.Node;

// ReSharper disable once InconsistentNaming
public class NodeXY : BaseNode
{
    public int X { get; set; }
    public int Y { get; set; }
    
    public bool IsObstacle { get; set; }
}