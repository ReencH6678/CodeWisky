using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class Trigonometry 
{
    public static Vector2 CalculatePointOnCircle(Vector2 objectPosition, Vector2 directionObject, float radius)
    {
        Vector2 targetPosition = directionObject - objectPosition;
        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x);

        return new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius) + objectPosition;
    }

    public static Vector2 CalculatePointOnCircle(Vector2 objectPosition, Vector2 directionObject, float radius, float ellipseRatio)
    {
        Vector2 targetPosition = directionObject - objectPosition;
        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x);

        return new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius * ellipseRatio) + objectPosition;
    }

    public static float CalculateAngle(Vector3 transfom, Vector3 directionObject)
    {
        Vector3 targetPosition = directionObject - transfom;
        return  Mathf.Atan2(targetPosition.y, targetPosition.x);
    }

    public static Vector2 CalculateDirectionOfAngle(float angle)
    {
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
