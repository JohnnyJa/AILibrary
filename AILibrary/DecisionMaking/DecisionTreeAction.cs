namespace AILibrary.DecisionMaking;

public class DecisionTreeAction : DecisionTreeNode
{
    protected event EventHandler PerformAction;
    
    public override DecisionTreeNode MakeDecision()
    {
        PerformAction?.Invoke(this, EventArgs.Empty);
        return null;
    }
}