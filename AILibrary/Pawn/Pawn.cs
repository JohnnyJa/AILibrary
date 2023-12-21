using System.Net.Mime;
using System.Numerics;
using AILibrary.AIMovement;
using AILibrary.AIMovement.Model;
using AILibrary.Static;
using ZeroElectric.Vinculum;

namespace AILibrary.Pawn;

public class Pawn : Kinematic
{
    Texture _texture;
    // public float RotationAngle { get; set; }
    // public Vector2 CurrentPosition { get; set; }
    // public Vector2 TargetPosition { get; set; }
    // public float TargetRotation { get; set; }


    public Pawn(Texture texture, Kinematic current)
    {
        _texture = texture;
        Position = current.Position;
        Orientation = current.Orientation;
    }
    
    // public void Spawn(Kinematic kinematic)
    // {
    //
    //     Position = kinematic.Position;
    //     Orientation = kinematic.Orientation;
    // }

    public virtual void Tick()
    {
        // PerformRotation(TargetRotation);
        // PerformMovement(TargetPosition);
        Draw();
    }

    // private void PerformRotation(float targetRotation)
    // {
    //     RotationAngle = _movement.RotateTo( RotationAngle, targetRotation);
    // }
    //
    // private void PerformMovement(Vector2 targetPosition)
    // {
    //     CurrentPosition = _movement.MoveTo(CurrentPosition, targetPosition);
    // }
    
    private void Draw()
    {
        Console.WriteLine(Position);
        
        Raylib.DrawTexturePro(_texture,
            new Rectangle(0, 0, _texture.width, _texture.height){  },
            new Rectangle(Position.X, Position.Y, _texture.width, _texture.height){  },
            new Vector2(_texture.width / 2f, _texture.height / 2f){  },
            Orientation * MathHelper.Rad2Deg, // Преобразование радиан в градусы
            Raylib.WHITE);
    }
}