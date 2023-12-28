namespace AILibrary.DecisionMaking.StateMachine;

public abstract class BooleanCondition : Condition
{
    public override bool Test()
    {
        return TestValue();
    }
    protected abstract bool TestValue();
}