using System.Net.Mime;
using System.Numerics;
using AILibrary.AIMovement;
using AILibrary.AIMovement.Behavoirs;
using AILibrary.AIMovement.Behavoirs.Movement;
using AILibrary.AIMovement.Behavoirs.Movement.Delegated;
using AILibrary.AIMovement.Behavoirs.Movement.Position.Basic;
using AILibrary.AIMovement.Interface;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;
using AILibrary.DecisionMaking;
using AILibrary.Static;
using Demo.DecisionTree.Actions;
using HelloWorld.DecisionTree.Decisions;
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
    
    private GetToDecision _decisionTree;
    private IBehaviorMovement _behaviorMovement;
    private BlendedSteering _steering;
    bool isSpawned = false;

    public Kinematic Kinematic { get; set; }

    public Pawn(Texture texture)
    {
        _texture = texture;
        Kinematic = new Kinematic();
        
        _decisionTree = new GetToDecision();
        _decisionTree.FalseNode = new MoveAction();
        _decisionTree.TrueNode = new AttackAction();
        _decisionTree.TargetPosition = new Vector2(100, 100);
        _decisionTree.AcceptRadius = 100f;
    }

    public virtual void Tick()
    {
        _decisionTree.CurrentPosition = Kinematic.Position;
        _decisionTree.MakeDecision();
        
        var steering = _steering.GetSteering();
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
        _behaviorMovement = behaviorMovement;
        _behaviorMovement.SetCharacter(Kinematic);
    }
    public void SetBlendingBehavior(BlendedSteering steering)
    {
        _steering = steering;
        foreach (var behavior in _steering.Behaviors)
        {
            behavior.Behavior.SetCharacter(Kinematic);
        }
    }
    
    public void SetTargetPosition(Vector2 targetPosition)
    {
        _behaviorMovement.SetTargetPosition(targetPosition);
    }
    
    public void SetTargetOrientation(float targetOrientation)
    {
        _behaviorMovement.SetTargetOrientation(targetOrientation);
    }
    
    public void SetTargetVelocity(Vector2 targetVelocity)
    {
        _behaviorMovement.SetTargetVelocity(targetVelocity);
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