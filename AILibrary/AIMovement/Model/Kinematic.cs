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
    
    public void Update(KinematicSteeringOutput steering)
    {
        // Position = Vector2.Lerp(Position, Velocity, Raylib.GetFrameTime() * MovementSpeed);
        // return currentPosition;
        // Console.WriteLine($"{Position}, {Orientation}, {Velocity}, {Rotation}");
        Position += Velocity * Raylib.GetFrameTime();
        Orientation += Rotation * Raylib.GetFrameTime();
        Velocity += steering.Velocity * Raylib.GetFrameTime();
        if (steering.Rotation != null)
        {
            Rotation += (float)steering.Rotation * Raylib.GetFrameTime();
        }
    }
}