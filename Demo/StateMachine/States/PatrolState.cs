using AILibrary.DecisionMaking.StateMachine;
using AILibrary.Pawn;
using HelloWorld.StateMachine.Actions;

namespace HelloWorld.StateMachine.States;

public class PatrolState : State
{
    
    private Pawn _pawn;
    
    public PatrolState(Pawn pawn)
    {
        _pawn = pawn;
    }
    
    public override List<StateAction> GetActions()
    {
        return new List<StateAction>() { new PatrolAction(_pawn, Level.Graph, Level.Nodes) };

    }

    public override List<StateAction> GetEntryActions()
    {
        return new List<StateAction>();
    }

    public override List<StateAction> GetExitActions()
    {
        return new List<StateAction>();
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>()
        {
            new Transition(new OnLeftButtonPressedCondition())
            {
                TargetState = new RunAwayState(_pawn),
            }
        };
    }
}