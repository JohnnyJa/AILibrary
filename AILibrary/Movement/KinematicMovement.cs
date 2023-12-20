using System.Numerics;
using AILibrary.Static;
using ZeroElectric.Vinculum;

namespace AILibrary.Movement;

public class KinematicMovement : IMovement
{
    public float MovementSpeed { get ; set ; }
    public float RotationSpeed { get; set; }

    public KinematicMovement(float maxSpeed, float maxRotationSpeed)
    {
        MovementSpeed = maxSpeed;
        RotationSpeed = maxRotationSpeed;
    }

    public Vector2 MoveTo(Vector2 currentPosition, Vector2 targetPosition)
    {
        currentPosition = Vector2.Lerp(currentPosition, targetPosition, Raylib.GetFrameTime() * MovementSpeed);
        return currentPosition;
    }
    
    public float RotateTo(float currentRotation, float targetRotation)
    {
        currentRotation = MathHelper.WrapAngle(currentRotation);
        targetRotation = MathHelper.WrapAngle(targetRotation);
        
        float rotationDirection = GetDirection(currentRotation, targetRotation);
        currentRotation += rotationDirection * RotationSpeed * Raylib.GetFrameTime();

        return currentRotation;
    }

    private float GetDirection(float currentRotation, float targetRotation)
    {

        if (currentRotation > targetRotation)
        {
            if (currentRotation - targetRotation < MathF.PI)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        else if (currentRotation < targetRotation)
        {
            if (targetRotation - currentRotation < MathF.PI)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            return 0;
        }

    }
    
}