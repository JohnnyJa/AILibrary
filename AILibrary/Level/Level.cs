using AILibrary.Pathfinding.Interface;
using AILibrary.Pathfinding.Node;

namespace AILibrary.Pathfinding;

public class Level : ILevel<NodeXY>
{
    private readonly NodeXY[,] _nodeMatrix;


    public Level(int[,] levelMatrix)
    {
        _nodeMatrix = new NodeXY[levelMatrix.GetLength(0), levelMatrix.GetLength(1)];
        for (int i = 0; i < levelMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < levelMatrix.GetLength(1); j++)
            {
                _nodeMatrix[i, j] = new NodeXY()
                {
                    X = i,
                    Y = j,
                    IsObstacle = levelMatrix[i, j] == 1
                };
            }
        }
    }

    public NodeXY this[int x, int y] => _nodeMatrix[x, y];

    public bool CanTraverse(NodeXY start, NodeXY end)
    {
        if (IsWrongNode(start) || IsWrongNode(end))
        {
            throw new Exception("Node is out of range");
        }

        return !_nodeMatrix[end.X, end.Y].IsObstacle && IsNeighbours(start, end);
    }

    public bool IsNeighbours(NodeXY node1, NodeXY node2)
    {
        if (IsWrongNode(node1) || IsWrongNode(node2))
        {
            throw new Exception("Node is out of range");
        }

        if (node1.X - 1 == node2.X && node1.Y == node2.Y)
        {
            return true;
        }

        if (node1.X == node2.X - 1 && node1.Y == node2.Y)
        {
            return true;
        }

        if (node1.X == node2.X && node1.Y - 1 == node2.Y)
        {
            return true;
        }

        if (node1.X == node2.X && node1.Y == node2.Y - 1)
        {
            return true;
        }

        return false;
    }

    private bool IsWrongNode(NodeXY node)
    {
        return node.X < 0 || node.Y < 0 || node.X - 1 > _nodeMatrix.GetLength(0) ||
               node.Y - 1 > _nodeMatrix.GetLength(1);
    }

    public IEnumerable<NodeXY> GetNeighbours(NodeXY node)
    {
        if (IsWrongNode(node))
        {
            throw new Exception("Node is out of range");
        }

        var res = new List<NodeXY>();

        if (node.X > 0)
        {
            res.Add(_nodeMatrix[node.X - 1, node.Y]);
        }

        if (node.Y > 0)
        {
            res.Add(_nodeMatrix[node.X, node.Y - 1]);
        }

        if (node.X < _nodeMatrix.GetLength(0) - 1)
        {
            res.Add(_nodeMatrix[node.X + 1, node.Y]);
        }

        if (node.Y < _nodeMatrix.GetLength(1) - 1)
        {
            res.Add(_nodeMatrix[node.X, node.Y + 1]);
        }

        return res;
    }
}