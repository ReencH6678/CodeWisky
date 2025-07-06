using UnityEngine;

public class DirectionMultiplier : MonoBehaviour
{
    public Vector2 DirectionRario { get; private set; }

    public void Awake()
    {
        DirectionRario = new Vector2(0, 0);
    }

    public void Add(Vector2 multiplier)
    {
        DirectionRario += multiplier;
    }
    public void Sub(Vector2 multiplier)
    {
        DirectionRario -= multiplier;
    }

    public void Inverse()
    {
        DirectionRario *= -1;
    }

    public void Reset()
    {
        DirectionRario = new Vector2(0, 0);
    }
}
