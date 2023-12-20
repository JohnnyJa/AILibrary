namespace AILibrary.Static;

public class MathHelper
{
    public static float Rad2Deg { get; set; } = 180 / MathF.PI;
    public static float WrapAngle(float angle)
    {
        if (angle >= MathF.PI || angle <= -MathF.PI)
        {
            angle *= -1;
        } 
        return angle;
    }
}