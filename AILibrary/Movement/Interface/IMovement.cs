using System.Data;
using System.Numerics;

namespace AILibrary.Movement;

public interface IMovement
{
    public float RotateTo(float currentRotation, float targetRotation);

    public Vector2 MoveTo(Vector2 currentPosition, Vector2 targetPosition);
    // public SteeringOutput Update(float time);
}