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
        Texture texture = Raylib.LoadTexture("C:\\Users\\Ivan\\RiderProjects\\AILibrary\\Demo\\assets\\arrow2.png");

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

        var pawns = new Pawn[]
        {
            new Pawn(texture),
            new Pawn(texture)
        };


        List<Vector2> pathPoints = new List<Vector2>
        {
            new Vector2(100, 100),
            new Vector2(200, 300),
            new Vector2(400, 200),
            new Vector2(600, 400)
        };

        var path = new Path(pathPoints);


        pawns[0].SpawnAt(new Vector2(100, 100));
        pawns[1].SpawnAt(new Vector2(850, 150));

        pawns[0].SetMovementBehavior(new ArriveBehavior(5f, 5f, 20f, 5f));
        pawns[1].SetMovementBehavior(new ArriveBehavior(5f, 5f, 20f, 5f));
        
        pawns[0].ToAvoid.Add(pawns[1]);
        pawns[1].ToAvoid.Add(pawns[0]);


        pawns[0].Kinematic.Velocity = new Vector2(10, 0);
        pawns[1].Kinematic.Velocity = new Vector2(-10, 0);
        while (!Raylib.WindowShouldClose())
        {
            Vector2 mousePosition = Raylib.GetMousePosition();

            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_RIGHT))
            {
                targetPoint = mousePosition;
            }

            
            
            pawns[0].SetTargetPosition(new Vector2(300, 100));
            pawns[1].SetTargetPosition(new Vector2(100, 300));
            
            // foreach (var pawn in pawns)
            // {
            //     pawn.SetTargetPosition(targetPoint);
            // }

            Raylib.BeginDrawing();
            foreach (var pathPoint in pathPoints)
            {
                Raylib.DrawCircle((int)pathPoint.X, (int)pathPoint.Y, 5, Raylib.RED);
            }

            Raylib.ClearBackground(Raylib.RAYWHITE);

            foreach (var pawn in pawns)
            {
                pawn.Tick();
            }

            Raylib.EndDrawing();
        }


        Raylib.UnloadTexture(texture);
        Raylib.CloseWindow();
    }
}