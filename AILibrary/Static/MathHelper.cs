using System.Numerics;

namespace AILibrary.Static;

public class MathHelper
{
    public static float Rad2Deg { get; set; } = 180 / MathF.PI;
    public static float WrapAngle(float radians)
    {
        radians %= 2 * MathF.PI;

        if (radians > MathF.PI)
        {
            radians -= 2 * MathF.PI;
        }

        if (radians <= -MathF.PI)
        {
            radians += 2 * MathF.PI;
        }

        return radians;
    }
    
    public static float GetNewOrientation(float currentOrientation, Vector2 velocity)
    {
        if (velocity.LengthSquared() > 0)
        {
            return (float)Math.Atan2(velocity.Y, velocity.X);
        }
        else
        {
            return currentOrientation;
        }
    }
    
    public static Vector2 AsVector(float radians)
    {
        return new Vector2(MathF.Cos(radians), MathF.Sin(radians));
    }
}