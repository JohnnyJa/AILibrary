using System.Numerics;

namespace AILibrary.AIMovement.Behavoirs.Movement.PathFollowing.Path;

public class Path
{
    private List<Vector2> points;
    private float totalPathLength;

    public Path(List<Vector2> points)
    {
        this.points = points;
        CalculateTotalPathLength();
    }

    private void CalculateTotalPathLength()
    {
        totalPathLength = 0;
        for (int i = 0; i < points.Count; i++)
        {
            int nextIndex = (i + 1) % points.Count; // Індекс наступної точки, циклічно
            totalPathLength += Vector2.Distance(points[i], points[nextIndex]);
        }
    }

    public float GetParam(Vector2 position, float lastParam)
    {
        float closestParam = lastParam;
        float closestDistance = float.MaxValue;

        for (float t = lastParam; t < lastParam + 1.0f; t += 0.01f) // Перевіряємо кожну 0.01 частину шляху
        {
            float param = t % 1.0f; // Обертаємо param між 0 і 1 (циклічно)
            Vector2 pathPosition = GetPosition(param);

            float distance = Vector2.Distance(position, pathPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestParam = param;
            }
        }

        return closestParam;
    }

    public Vector2 GetPosition(float param)
    {
        param = param % 1.0f; // Обертаємо param між 0 і 1 (циклічно)

        float targetDistance = param * totalPathLength;
        float currentDistance = 0;

        for (int i = 0; i < points.Count; i++)
        {
            int nextIndex = (i + 1) % points.Count; // Індекс наступної точки, циклічно
            float segmentLength = Vector2.Distance(points[i], points[nextIndex]);

            if (currentDistance + segmentLength >= targetDistance)
            {
                float remainingDistance = targetDistance - currentDistance;
                float t = remainingDistance / segmentLength;

                return Vector2.Lerp(points[i], points[nextIndex], t);
            }

            currentDistance += segmentLength;
        }

        // Ніколи не має дійти сюди при правильному використанні
        return Vector2.Zero;
    }
}