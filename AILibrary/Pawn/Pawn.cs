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

    public float MaxAcceleration { get; set; } = 10f;
    // public float RotationAngle { get; set; }
    // public Vector2 CurrentPosition { get; set; }
    // public Vector2 TargetPosition { get; set; }
    // public float TargetRotation { get; set; }

    private IBehaviorMovement _behaviorMovement;
    bool isSpawned = false;
    
    private Kinematic _kinematic;
    public Pawn(Texture texture)
    {
        _texture = texture;
        _kinematic = new Kinematic();
        _behaviorMovement = new EmptyBehavior();
    }

    public virtual void Tick()
    {
        var steering = _behaviorMovement.GetSteering();
        // _kinematic = _movement.Character;
        _kinematic.Update(steering, MaxSpeed);
        Draw();
    }

    public void SpawnAt(Vector2 position)
    {
        _kinematic.Position = position;
        isSpawned = true;
    }
    
    public void SetMovementBehavior(IBehaviorMovement behaviorMovement)
    {
        _behaviorMovement = behaviorMovement;
        _behaviorMovement.SetCharacter(_kinematic);
    }
    
    public void SetTargetLocation(Vector2 targetPosition)
    {
        _behaviorMovement.SetTargetPosition(targetPosition);
    }
    
    public void SetTargetOrientation(Vector2 targetOrientation)
    {
        _behaviorMovement.SetTargetOrientation(targetOrientation - _kinematic.Position);
    }
    
    public void SetTargetVelocity(Vector2 targetVelocity)
    {
        _behaviorMovement.SetTargetVelocity(targetVelocity);
    }
    
    private void Draw()
    {
        Console.WriteLine($" Current orientation: {_kinematic.Orientation}\n");
        
        Raylib.DrawTexturePro(_texture,
            new Rectangle(0, 0, _texture.width, _texture.height){  },
            new Rectangle(_kinematic.Position.X, _kinematic.Position.Y, _texture.width, _texture.height){  },
            new Vector2(_texture.width / 2f, _texture.height / 2f){  },
            _kinematic.Orientation * MathHelper.Rad2Deg, // Преобразование радиан в градусы
            Raylib.WHITE);
    }
}