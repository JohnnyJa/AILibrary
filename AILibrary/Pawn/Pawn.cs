using System.Net.Mime;
using System.Numerics;
using AILibrary.AIMovement;
using AILibrary.AIMovement.Behavoirs;
using AILibrary.AIMovement.Behavoirs.Movement.Delegated;
using AILibrary.AIMovement.Behavoirs.Movement.Position.Basic;
using AILibrary.AIMovement.Interface;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;
using AILibrary.Static;
using ZeroElectric.Vinculum;

namespace AILibrary.Pawn;

public class Pawn
{
    private readonly Texture _texture;

    public float MaxSpeed { get; set; } = 50f;

    public float MaxAcceleration { get; set; } = 10f;
    // public float RotationAngle { get; set; }
    // public Vector2 CurrentPosition { get; set; }
    // public Vector2 TargetPosition { get; set; }
    // public float TargetRotation { get; set; }

    public List<Pawn> ToAvoid { get; set; } = new();
    

    private CollisionAvoidence _avoidBehavior = new(10f, 50f);
    bool isSpawned = false;

    public Kinematic Kinematic { get; set; }

    public Pawn(Texture texture)
    {
        _texture = texture;
        Kinematic = new Kinematic();
        _avoidBehavior.SetCharacter(Kinematic);
    }

    public virtual void Tick()
    {
        foreach (var pawn in ToAvoid)
        {
            _avoidBehavior.Targets.Add(pawn.Kinematic);
        }
        var steeringAvoid = _avoidBehavior.GetSteering();
        _avoidBehavior.Targets.Clear();
        
        Console.WriteLine($"steeringAvoid: {steeringAvoid?.Linear}");

        var steering = new SteeringOutput() + steeringAvoid;
        // _kinematic = _movement.Character;
        Kinematic.Update(steering, MaxSpeed);
        Draw();
    }

    public void SpawnAt(Vector2 position)
    {
        Kinematic.Position = position;
        isSpawned = true;
    }
    
    public void SetMovementBehavior(IBehaviorMovement behaviorMovement)
    {
        // _behaviorMovement = behaviorMovement;
    }
    
    public void SetTargetPosition(Vector2 targetPosition)
    {
        // _behaviorMovement.SetTargetPosition(targetPosition);
    }
    
    public void SetTargetOrientation(float targetOrientation)
    {
        // _behaviorMovement.SetTargetOrientation(targetOrientation);
    }
    
    public void SetTargetVelocity(Vector2 targetVelocity)
    {
        // _behaviorMovement.SetTargetVelocity(targetVelocity);
    }
    
    private void Draw()
    {
        // Console.WriteLine($" Current orientation: {Kinematic.Orientation}\n");
        
        Raylib.DrawTexturePro(_texture,
            new Rectangle(0, 0, _texture.width, _texture.height){  },
            new Rectangle(Kinematic.Position.X, Kinematic.Position.Y, _texture.width, _texture.height){  },
            new Vector2(_texture.width / 2f, _texture.height / 2f){  },
            Kinematic.Orientation * MathHelper.Rad2Deg, // Преобразование радиан в градусы
            Raylib.WHITE);
    }
}