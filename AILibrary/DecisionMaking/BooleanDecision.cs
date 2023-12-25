namespace AILibrary.DecisionMaking;

public abstract class BooleanDecision : Decision<bool>
{
    protected override DecisionTreeNode GetBranch()
    {
        return TestValue() ? TrueNode : FalseNode;
    }
}