using AILibrary.AIMovement.Behavoirs.Movement;
using AILibrary.AIMovement.Behavoirs.Movement.Delegated;
using AILibrary.AIMovement.Behavoirs.Movement.PathFollowing;
using AILibrary.AIMovement.Model;
using AILibrary.DecisionMaking.StateMachine;
using AILibrary.Pawn;
using ZeroElectric.Vinculum;

namespace HelloWorld.StateMachine.Actions;

public class RunAwayAction : StateAction
{
    private Pawn _pawn;

    public RunAwayAction(Pawn pawn)
    {
        _pawn = pawn;
    }
    
    public override void DoAction()
    {
        var steering = new BlendedSteering
        {
            Behaviors = new BlendedSteering.BehaviorAndWeight[]
            {
                new BlendedSteering.BehaviorAndWeight()
                {
                    Behavior = new EvadeBehavior(5f, 5f)
                    {
                        Target = new Kinematic(){ Position = Raylib.GetMousePosition()},
                    },
                    Weight = 1f
                },
                new BlendedSteering.BehaviorAndWeight()
                {
                    Behavior = new LookWhereYouGoBehavior(5f, 5f, 0.01f, 0.02f),
                    Weight = 1f
                }
            }
        };
        _pawn.SetBlendingBehavior(steering);
    }
}