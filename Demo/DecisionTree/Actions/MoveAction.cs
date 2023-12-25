using AILibrary.DecisionMaking;

namespace HelloWorld.DecisionTree.Actions;

public class MoveAction : DecisionTreeAction
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