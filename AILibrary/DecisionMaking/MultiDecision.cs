namespace AILibrary.DecisionMaking;

public abstract class MultiDecision : Decision<int>
{
    public List<DecisionTreeNode> DaughterNodes { get; set; } = new();
    
    protected override DecisionTreeNode GetBranch()
    {
        return DaughterNodes[TestValue()];    
    }
    
}