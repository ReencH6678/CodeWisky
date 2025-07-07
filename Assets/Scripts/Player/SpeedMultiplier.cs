using UnityEngine;


public class SpeedMultiplier : MonoBehaviour
{
    public float SpeedRatio { get; private set; }

    public void Awake()
    {
        SpeedRatio = 1;
    }

    public void AddSpeed(float multiplier)
    {
        if (CanMultiply(multiplier))
            SpeedRatio += multiplier;
    }

    public void SubSpeed(float multiplier)
    {
        if (CanMultiply(multiplier))
            SpeedRatio -= multiplier;
    }

    public void ResetSpeed()
    {
        SpeedRatio = 1;
    }

    private bool CanMultiply(float multiplier)
    {
        return multiplier != 0;
    }
}
