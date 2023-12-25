using System.Numerics;
using AILibrary.AIMovement.Output;
using ZeroElectric.Vinculum;

namespace AILibrary.AIMovement.Model;

public class Kinematic
{
    public Vector2 Position { get; set; }
    public float Orientation { get; set; }
    public Vector2 Velocity { get; set; }
    public float Rotation { get; set; }
    
    public void Update(SteeringOutput? steering, float maxSpeed)
    {
        // Position = Vector2.Lerp(Position, Velocity, Raylib.GetFrameTime() * MovementSpeed);
        // return currentPosition;
        // Console.WriteLine($"{Position}, {Orientation}, {Velocity}, {Rotation}");
        
        if (steering == null)
        {
            Velocity = new Vector2();
            Rotation = 0;
            return;
        }

        if (float.IsNaN(steering.Angular))  
        {
            Rotation = 0;
        }

        else
        {
            Rotation += steering.Angular * Raylib.GetFrameTime();
        }
        Position += Velocity * Raylib.GetFrameTime();
        Orientation += Rotation * Raylib.GetFrameTime();
        
        Velocity += steering.Linear * Raylib.GetFrameTime();

        if (Velocity.Length() > maxSpeed)
        {
            Velocity = Vector2.Normalize(Velocity) * maxSpeed;
        }
    }
}