using System.Net.Mime;
using System.Numerics;
using AILibrary.AIMovement;
using AILibrary.AIMovement.Interface;
using AILibrary.AIMovement.Model;
using AILibrary.Static;
using ZeroElectric.Vinculum;

namespace AILibrary.Pawn;

public class Pawn : Kinematic
{
    private readonly Texture _texture;

    public float MaxSpeed { get; set; } = 50f;
    // public float RotationAngle { get; set; }
    // public Vector2 CurrentPosition { get; set; }
    // public Vector2 TargetPosition { get; set; }
    // public float TargetRotation { get; set; }

    private IMovement _movement;
    bool isSpawned = false;
    
    private Kinematic _kinematic;
    public Pawn(Texture texture)
    {
        _texture = texture;
        _kinematic = new Kinematic();
        _movement = new KinematicEmpty();
    }

    public virtual void Tick()
    {
        var steering = _movement.GetSteering();
        // _kinematic = _movement.Character;
        _kinematic.Update(steering, MaxSpeed);
        Draw();
    }

    public void SpawnAt(Vector2 position)
    {
        _kinematic.Position = position;
        isSpawned = true;
    }
    
    public void SetMovementBehavior(IMovement movement)
    {
        _movement = movement;
        _movement.SetParams(_kinematic, MaxSpeed);
    }
    
    public void SetTargetLocation(Vector2 targetPosition)
    {
        _movement.SetTarget(targetPosition);
    }
    
    private void Draw()
    {
        Console.WriteLine($"{_kinematic.Position}, {_kinematic.Orientation}, {_kinematic.Velocity}, {_kinematic.Rotation}");
        
        Raylib.DrawTexturePro(_texture,
            new Rectangle(0, 0, _texture.width, _texture.height){  },
            new Rectangle(_kinematic.Position.X, _kinematic.Position.Y, _texture.width, _texture.height){  },
            new Vector2(_texture.width / 2f, _texture.height / 2f){  },
            _kinematic.Orientation * MathHelper.Rad2Deg, // Преобразование радиан в градусы
            Raylib.WHITE);
    }
}