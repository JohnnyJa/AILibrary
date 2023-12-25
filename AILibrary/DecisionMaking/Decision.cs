namespace AILibrary.DecisionMaking;

public abstract class Decision<T> : DecisionTreeNode 
{
    public DecisionTreeNode TrueNode { get; set; }
    public DecisionTreeNode FalseNode { get; set; }

    public override DecisionTreeNode MakeDecision()
    {
        var branch = GetBranch();
        return branch.MakeDecision();
    }

    protected abstract DecisionTreeNode GetBranch();
    protected abstract T TestValue();

    
}