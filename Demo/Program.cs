// See https://aka.ms/new-console-template for more information

using System.Numerics;
using AILibrary.AIMovement;
using AILibrary.AIMovement.Behavoirs;
using AILibrary.AIMovement.Behavoirs.Movement;
using AILibrary.AIMovement.Behavoirs.Movement.Delegated;
using AILibrary.AIMovement.Behavoirs.Movement.PathFollowing;
using AILibrary.AIMovement.Behavoirs.Movement.PathFollowing.Path;
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
        
        List<Vector2> pathPoints = new List<Vector2>
        {
            new Vector2(100, 100),
            new Vector2(200, 300),
            new Vector2(400, 200),
            new Vector2(600, 400)
        };

        var path = new Path(pathPoints);
        
        pawn.SpawnAt(currentPosition);
        pawn.SetMovementBehavior(new FollowPath(path, 5f));

        while (!Raylib.WindowShouldClose())
        {
            Vector2 mousePosition = Raylib.GetMousePosition();

            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_RIGHT))
            {
                targetPoint = mousePosition;
            }
            
            pawn.SetTargetPosition(targetPoint);

            Raylib.BeginDrawing();
            foreach (var pathPoint in pathPoints)
            {
                Raylib.DrawCircle((int)pathPoint.X, (int)pathPoint.Y, 5, Raylib.RED);
            
            }
            
            Raylib.ClearBackground(Raylib.RAYWHITE);

            pawn.Tick();

            Raylib.EndDrawing();
        }


        Raylib.UnloadTexture(texture);
        Raylib.CloseWindow();
    }
}