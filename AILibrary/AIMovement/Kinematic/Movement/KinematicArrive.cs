using System.Numerics;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;
using AILibrary.Static;

namespace AILibrary.AIMovement;

public class KinematicArrive : BaseKinematic
{
    float _radius;
    float _timeToTarget = 0.25f;
    
    public KinematicArrive(float radius) : base(new Kinematic())
    {
        _radius = radius;
    }

    public override SteeringOutput? GetSteering()
    {
        if (Character == null)
        {
            throw new NullReferenceException("Character is null");
        }
        
        var result = new SteeringOutput();

        result.Linear = Target.Position - Character.Position;
            Console.WriteLine(result.Linear.Length());
            if (result.Linear.Length() < _radius)
            {
                return null;
            }

            result.Linear /= _timeToTarget;

            if (result.Linear.Length() > MaxSpeed)
            {
                result.Linear = Vector2.Normalize(result.Linear) * MaxSpeed;
            }

            Character.Orientation = GetNewOrientation(Character.Orientation, result.Linear);
        

        result.Angular = 0;
        return result;
    }
}