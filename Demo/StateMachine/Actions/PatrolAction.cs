using AILibrary.AIMovement.Behavoirs.Movement;
using AILibrary.AIMovement.Behavoirs.Movement.Delegated;
using AILibrary.AIMovement.Behavoirs.Movement.PathFollowing;
using AILibrary.DecisionMaking.StateMachine;
using AILibrary.Pathfinding;
using AILibrary.Pathfinding.Node;
using AILibrary.Pawn;
using Path = AILibrary.AIMovement.Behavoirs.Movement.PathFollowing.Path.Path;

namespace HelloWorld.StateMachine.Actions;

public class PatrolAction : StateAction
{
    private Pawn _pawn;
    private Graph _graph;
    private NodeXY[] _nodes;

    public PatrolAction(Pawn pawn, Graph graph, NodeXY[] nodes)
    {
        _pawn = pawn;
        _graph = graph;
        _nodes = nodes;
    }
    
    public override void DoAction()
    {
        var pathfinding = new AStarPathfinding();
        
        var path = new Path( pathfinding.FindPath(_graph, _nodes[0], _nodes[3]).Select(x => x.Position).ToList());
        
        var steering = new BlendedSteering();
        steering.Behaviors = new BlendedSteering.BehaviorAndWeight[]
        {
            new BlendedSteering.BehaviorAndWeight()
            {
                Behavior = new FollowPath(path, 5f),
                Weight = 1f
            },
            new BlendedSteering.BehaviorAndWeight()
            {
                Behavior = new LookWhereYouGoBehavior(5f, 5f, 0.01f, 0.02f),
                Weight = 1f
            }
        };
        _pawn.SetBlendingBehavior(steering);
    }
}