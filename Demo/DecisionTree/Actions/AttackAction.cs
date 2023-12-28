using AILibrary.DecisionMaking;

namespace Demo.DecisionTree.Actions;

public class AttackAction : DecisionAction
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