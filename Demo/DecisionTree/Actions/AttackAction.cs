using AILibrary.DecisionMaking;

namespace HelloWorld.DecisionTree.Actions;

public class AttackAction : DecisionTreeAction
{
    public AttackAction()
    {
        PerformAction += Attack;
    }

    private void Attack(object? sender, EventArgs e)
    {
        Console.WriteLine("Attacking!");
    }
}