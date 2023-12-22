using System.Numerics;
using AILibrary.AIMovement.Behavoirs.Abstract;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Behavoirs.Movement.Position.Basic;

public class SeparationBehavior : BaseBehavior
{
    private readonly float _maxAccelaraion;
    private readonly float _threshold;
    private readonly float _decayCoefficient;

    public SeparationBehavior(float maxAccelaraion, float threshold, float decayCoefficient)
    {
        _maxAccelaraion = maxAccelaraion;
        _threshold = threshold;
        _decayCoefficient = decayCoefficient;
    }

    public List<Kinematic> Targets { get; set; } = new();
    
    public override SteeringOutput? GetSteering()
    {
        var res = new SteeringOutput();

        foreach (var target in Targets)
        {
            var direction =   Character.Position - target.Position;
            var distance = direction.Length();
            
            Console.WriteLine($"distance: {distance}, _threshold: {_threshold}");
            
            if (distance < _threshold)
            {
                var strength = Math.Min(_decayCoefficient / (distance * distance), _maxAccelaraion);
                Console.WriteLine($"strength: {strength}");

                direction =  Vector2.Normalize(direction);
                res.Linear += strength * direction;
            }   
        }
        Console.WriteLine($"res.Linear: {res.Linear}");
        return res;
    }
}