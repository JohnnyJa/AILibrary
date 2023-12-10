using AILibrary.Pathfinding.Node;

namespace AILibrary.Pathfinding.Interface;

public interface ILevel<TNode> where TNode : BaseNode
{
    bool CanTraverse(TNode start, TNode end);
    bool IsNeighbours(TNode node1, TNode node2);
    IEnumerable<TNode> GetNeighbours(TNode node);
}