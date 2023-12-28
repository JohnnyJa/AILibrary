using AILibrary.DecisionMaking;

namespace Demo.DecisionTree.Actions;

public class MoveAction : DecisionAction
{
    public MoveAction()
    {
        PerformAction += Move;
    }
    
    private void Move(object? sender, EventArgs e)
    {
        Console.WriteLine("Moving!");
    }
}