using AILibrary.Pathfinding;
using AILibrary.Pathfinding.Node;

namespace PathfindingTesting;

public class Tests
{
    
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestAStar()
    {
        
        var level = new Level(new[,]
        {
            {1, 1, 1, 1, 1, 1, 1},
            {1, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 1, 0, 0, 1},
            {1, 0, 1, 1, 0, 0, 1},
            {1, 0, 0, 1, 0, 0, 1},
            {1, 0, 0, 1, 0, 0, 1},
            {1, 1, 1, 1, 1, 1, 1}
        });

        AStarPathfinding<NodeXY> pf = new AStarPathfinding<NodeXY>(level, GetH);
        var res = pf.FindPath(level[1, 1], level[5, 5]);

        if (res is null)
        {
            Console.WriteLine("Path doesn't exist");
        }
        
        else foreach (var node in res)
        {
            Console.WriteLine($"X:{node.X} Y:{node.Y}");
        }

        return;

        int GetH(NodeXY start, NodeXY target)
        {
            int dstX = Math.Abs(start.X - target.X);
            int dstY = Math.Abs(start.Y - target.Y);

            return dstX + dstY;
        }
    }
    
}