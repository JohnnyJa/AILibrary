using AILibrary.Pathfinding.Interface;
using AILibrary.Pathfinding.Node;

namespace AILibrary.Pathfinding;

public class AStarPathfinding<T> : IPathfinding<T> where T : BaseNode
{
    private readonly Func<T, T, int> _countHCost;
    private readonly ILevel<T> _level;

    private Dictionary<T, NodeValues> Nodes = new();

    private class NodeValues
    {
        public T? Parent { get; set; } = null;
        public int GCost { get; set; } = 0;
        public int HCost { get; set; } = 0;

        public int FCost => GCost + HCost;
    }


    public AStarPathfinding(ILevel<T> level, Func<T, T, int> countHCost)
    {
        _level = level;
        _countHCost = countHCost;
    }

    public IEnumerable<T>? FindPath(T startNode, T targetNode)
    {
        List<T> openSet = new List<T>();
        HashSet<T> closedSet = new HashSet<T>();

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

            if (currentNode == targetNode)
            {
                return RetracePath(currentNode);
            }

            foreach (T neighbor in _level.GetNeighbours(currentNode))
            {

                if (!_level.CanTraverse(currentNode, neighbor) || closedSet.Contains(neighbor))
                {
                    continue;
                }

                int newCostToNeighbor = Nodes[currentNode].GCost + _countHCost(currentNode, neighbor);

                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                    Nodes.Add(neighbor, new NodeValues()
                    {
                        GCost = newCostToNeighbor,
                        HCost = _countHCost(neighbor, targetNode),
                        Parent = currentNode
                    });
                }
                else if (newCostToNeighbor < Nodes[neighbor].GCost)
                {
                    Nodes[neighbor].GCost = newCostToNeighbor;
                    Nodes[neighbor].HCost = _countHCost(neighbor, targetNode);
                    Nodes[neighbor].Parent = currentNode;
                }
            }
        }

        return null; // Шлях не знайдено
    }

    private IEnumerable<T> RetracePath(T endNode)
    {
        List<T> path = new List<T>();
        T? currentNode = endNode;

        while (currentNode is not null)
        {
            path.Add(currentNode);
            currentNode = Nodes[currentNode].Parent;
        }

        path.Reverse();
        return path;
    }
}