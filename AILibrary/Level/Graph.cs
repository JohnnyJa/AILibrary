using AILibrary.Pathfinding.Node;

namespace AILibrary.Pathfinding;

public class Graph
{
    private Dictionary<NodeXY, List<Connection>> connections;

    public Graph()
    {
        connections = new Dictionary<NodeXY, List<Connection>>();
    }

    public List<Connection> GetConnections(NodeXY fromNode)
    {
        if (connections.TryGetValue(fromNode, out List<Connection> connectionList))
        {
            return connectionList;
        }

        return new List<Connection>();
    }

    public void AddConnection(NodeXY fromNode, NodeXY toNode, float cost)
    {
        if (!connections.ContainsKey(fromNode))
        {
            connections[fromNode] = new List<Connection>();
        }

        connections[fromNode].Add(new Connection(fromNode, toNode, cost));
    }
}

public class Connection
{
    public NodeXY FromNode { get; }
    public NodeXY ToNode { get; }
    public float Cost { get; }

    public Connection(NodeXY fromNode, NodeXY toNode, float cost)
    {
        FromNode = fromNode;
        ToNode = toNode;
        Cost = cost;
    }
}