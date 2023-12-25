// See https://aka.ms/new-console-template for more information

using System.Numerics;
using AILibrary.AIMovement;
using AILibrary.AIMovement.Behavoirs;
using AILibrary.AIMovement.Behavoirs.Movement;
using AILibrary.AIMovement.Behavoirs.Movement.Delegated;
using AILibrary.AIMovement.Behavoirs.Movement.PathFollowing;
using AILibrary.AIMovement.Behavoirs.Movement.PathFollowing.Path;
using AILibrary.AIMovement.Behavoirs.Movement.Position.Basic;
using AILibrary.AIMovement.Model;
using AILibrary.Pathfinding;
using AILibrary.Pathfinding.Node;
using AILibrary.Pawn;
using AILibrary.Static;
using ZeroElectric.Vinculum;
using Path = AILibrary.AIMovement.Behavoirs.Movement.PathFollowing.Path.Path;


namespace HelloWorld;

class Program
{
    public static void Main()
    {
        const int screenWidth = 1024;
        const int screenHeight = 1024;
        Raylib.InitWindow(screenWidth, screenHeight, "Rotate Texture Towards Point");

        // Загрузка текстуры
        Texture texture = Raylib.LoadTexture("C:\\Worr\\course\\AILibrary\\Demo\\assets\\arrow2.png");

        // Исходная позиция текстуры
        Vector2 currentPosition = new(screenWidth / 2f - texture.width / 2f, screenHeight / 2f - texture.height / 2f);

        // Заданная точка, в направлении которой будет поворачиваться текстура
        Vector2 targetPoint = currentPosition;

        Raylib.SetTargetFPS(60);

        var current = new Kinematic()
        {
            Position = currentPosition,
            Orientation = 0f
        };
        var target = new Kinematic();


        var pawn = new Pawn(texture);


        var graph = new Graph();

        List<NodeXY> nodes = new List<NodeXY>()
        {
            new NodeXY(new Vector2(100, 100)),
            new NodeXY(new Vector2(200, 300)),
            new NodeXY(new Vector2(400, 200)),
            new NodeXY(new Vector2(600, 400)),
            new NodeXY(new Vector2(800, 100)),
        };

        for (var i = 0; i < nodes.Count; i++)
        {
            for (int j = 0; j < nodes.Count(); j++)
            {
                if (i == j)
                {
                    continue;
                }

                if (i==0 && j==3)
                {
                    continue;
                }
                graph.AddConnection(nodes[i], nodes[j], Vector2.Distance(nodes[i].Position, nodes[j].Position));
            }
        }

        var pathfinding = new AStarPathfinding();
        
        var path = new Path( pathfinding.FindPath(graph, nodes[0], nodes[3]).Select(x => x.Position).ToList());

        pawn.SpawnAt(new Vector2(50, 100));
        var steering = new BlendedSteering();
        steering.Behaviors = new BlendedSteering.BehaviorAndWeight[]
        {
            new BlendedSteering.BehaviorAndWeight()
            {
                Behavior = new FollowPath(path, 5f),
                Weight = 1f
            },
            new BlendedSteering.BehaviorAndWeight()
            {
                Behavior = new LookWhereYouGoBehavior(5f, 5f, 0.01f, 0.02f),
                Weight = 1f
            }
        };

        
        
        // pawn.SetBlendingBehavior(steering);
        pawn.SetBlendingBehavior(steering);
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            Vector2 mousePosition = Raylib.GetMousePosition();

            foreach (var pathPoint in nodes)
            {
                Raylib.DrawCircle((int)pathPoint.Position.X, (int)pathPoint.Position.Y, 5f, Raylib.RED);
            }

            // pawn.SetTargetPosition(targetPoint);


            Raylib.ClearBackground(Raylib.RAYWHITE);

            pawn.Tick();

            Raylib.EndDrawing();
        }


        Raylib.UnloadTexture(texture);
        Raylib.CloseWindow();
    }
}