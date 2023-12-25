using AILibrary.Pathfinding.Node;

namespace AILibrary.Pathfinding;

public class DijkstraPathfinding
{
    public static List<NodeXY> FindPath(Graph graph, NodeXY start, NodeXY goal)
    {
        // Імплементація алгоритму Dijkstra
        var open = new List<NodeRecord>();
        var closed = new List<NodeRecord>();

        NodeRecord startRecord = new NodeRecord
        {
            Node = start,
            Connection = null,
            ParentNode = null,
            CostSoFar = 0
        };

        open.Add(startRecord);

        while (open.Count > 0)
        {
            NodeRecord current = open.MinBy(x => x.CostSoFar);

            if (current.Node == goal)
            {
                return ReconstructPath(start, current);
            }

            List<Connection> connections = graph.GetConnections(current.Node);

            foreach (Connection connection in connections)
            {
                NodeXY endNode = connection.ToNode;
                float endNodeCost = current.CostSoFar + connection.Cost;

                if (closed.Exists(x => x.Node == endNode))
                {
                    continue;
                }

                NodeRecord endNodeRecord;
                if (open.Exists(x => x.Node == endNode))
                {
                    endNodeRecord = open.Find(x => x.Node == endNode);
                    if (endNodeRecord.CostSoFar <= endNodeCost)
                    {
                        continue;
                    }
                }
                else
                {
                    endNodeRecord = new NodeRecord
                    {
                        Node = endNode
                    };

                    open.Add(endNodeRecord);
                }

                endNodeRecord.CostSoFar = endNodeCost;
                endNodeRecord.Connection = connection;
                endNodeRecord.ParentNode = current;
            }

            open.Remove(current);
            closed.Add(current);
        }

        return null; // Немає шляху до цільового вузла.
    }

    private static List<NodeXY> ReconstructPath(NodeXY start, NodeRecord endNodeRecord)
    {
        List<NodeXY> path = new List<NodeXY>();

        while (endNodeRecord.Node != start)
        {
            path.Add(endNodeRecord.Node);
            endNodeRecord = endNodeRecord.ParentNode;
        }
        path.Add(start);
        path.Reverse();
        return path;
    }
}

public class NodeRecord
{
    public NodeXY Node { get; set; }
    public Connection? Connection { get; set; }
    public NodeRecord? ParentNode { get; set; }
    public float CostSoFar { get; set; }
}