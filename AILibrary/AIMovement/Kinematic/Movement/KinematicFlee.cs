using System.Numerics;
using AILibrary.AIMovement.Interface;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;
using AILibrary.Static;

namespace AILibrary.AIMovement;

public class KinematicFlee : BaseKinematic
{
    public KinematicFlee() : base( new Kinematic())
    {
    }
    
    public override SteeringOutput GetSteering()
    {
        if (Character == null)
        {
            throw new NullReferenceException("Character is null");
        }
        
        SteeringOutput steering = new SteeringOutput();
        steering.Linear = Character.Position - Target.Position;
        if (steering.Linear.LengthSquared() < 1)
        {
            steering.Linear = Vector2.Zero;
        }
        else
        {
            steering.Linear = Vector2.Normalize(steering.Linear);
            steering.Linear *= MaxSpeed;
        }
        Character.Orientation = GetNewOrientation(Character.Orientation, steering.Linear);
        steering.Angular = 0;
        return steering;
    }
}