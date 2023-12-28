namespace AILibrary.DecisionMaking;

public class DecisionAction : DecisionTreeNode
{
    protected event EventHandler PerformAction;
    
    public override DecisionTreeNode MakeDecision()
    {
        PerformAction?.Invoke(this, EventArgs.Empty);
        return this;
    }
}