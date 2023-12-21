using System.Numerics;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;
using AILibrary.Static;

namespace AILibrary.AIMovement;

public class KinematicMovement
{
    public Kinematic Character { get; set; }
    public Kinematic Target { get; set; }
    public float MaxSpeed { get; set; }
    
    public KinematicMovement(Kinematic character, Kinematic target, float maxSpeed)
    {
        Character = character;
        Target = target;
        MaxSpeed = maxSpeed;
    }
    public KinematicSteeringOutput GetSeekSteering()
    {
        KinematicSteeringOutput steering = new KinematicSteeringOutput();
        steering.Velocity = Target.Position - Character.Position;
        steering.Velocity = Vector2.Normalize(steering.Velocity);
        steering.Velocity *= MaxSpeed;
        Character.Orientation = MathHelper.GetNewOrientation(Character.Orientation, steering.Velocity);
        steering.Rotation = 0;
        return steering;
    }
    
    public KinematicSteeringOutput GetFleeSteering()
    {
        KinematicSteeringOutput steering = new KinematicSteeringOutput();
        steering.Velocity = Character.Position - Target.Position;
        if (steering.Velocity.LengthSquared() < 1)
        {
            steering.Velocity = Vector2.Zero;
        }
        else
        {
            steering.Velocity = Vector2.Normalize(steering.Velocity);
            steering.Velocity *= MaxSpeed;
        }
        Character.Orientation = MathHelper.GetNewOrientation(Character.Orientation, steering.Velocity);
        steering.Rotation = 0;
        return steering;
    }
}