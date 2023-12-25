using System.Numerics;
using AILibrary.Pathfinding.Node;

namespace AILibrary.Pathfinding;

public class AStarPathfinding
{
    private float CountHCost(NodeXY start, NodeXY goal)
    {
        return Vector2.Distance(start.Position, goal.Position);
    }

    private Dictionary<NodeXY, NodeValues> Nodes = new();

    private class NodeValues
    {
        public NodeXY? Parent { get; set; } = null;
        public float GCost { get; set; } = 0;
        public float HCost { get; set; } = 0;

        public float FCost => GCost + HCost;
    }

    public IEnumerable<NodeXY>? FindPath(Graph graph, NodeXY startNode, NodeXY goal)
    {
        List<NodeXY> openSet = new List<NodeXY>();
        HashSet<NodeXY> closedSet = new HashSet<NodeXY>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            var currentNode = openSet[0];
            Nodes.TryAdd(currentNode, new NodeValues());


            for (var i = 1; i < openSet.Count; i++)
            {
                if (Nodes[openSet[i]].FCost < Nodes[currentNode].FCost ||
                    (Nodes[openSet[i]].FCost == Nodes[currentNode].FCost &&
                     Nodes[openSet[i]].HCost < Nodes[currentNode].HCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == goal)
            {
                return RetracePath(currentNode);
            }

            foreach (Connection connection in graph.GetConnections(currentNode))
            {
                var neighbor = connection.ToNode;
                
                if (closedSet.Contains(neighbor))
                {
                    continue;
                }

                var newCostToNeighbor = Nodes[currentNode].GCost + CountHCost(currentNode, neighbor);

                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                    Nodes.Add(neighbor, new NodeValues()
                    {
                        GCost = newCostToNeighbor,
                        HCost = CountHCost(neighbor, goal),
                        Parent = currentNode
                    });
                }
                else if (newCostToNeighbor < Nodes[neighbor].GCost)
                {
                    Nodes[neighbor].GCost = newCostToNeighbor;
                    Nodes[neighbor].HCost = CountHCost(neighbor, goal);
                    Nodes[neighbor].Parent = currentNode;
                }
            }
        }

        return null; // Шлях не знайдено
    }

    private IEnumerable<NodeXY> RetracePath(NodeXY endNode)
    {
        List<NodeXY> path = new List<NodeXY>();
        NodeXY? currentNode = endNode;

        while (currentNode is not null)
        {
            path.Add(currentNode);
            currentNode = Nodes[currentNode].Parent;
        }

        path.Reverse();
        return path;
    }
}