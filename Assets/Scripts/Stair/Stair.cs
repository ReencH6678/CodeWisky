using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Stair : MonoBehaviour
{
    [Range(0, 180)]
    public float Angle;

    public Vector2 GetDirection()
    {
        return Quaternion.AngleAxis(Angle, Vector3.forward) * Vector2.up;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Vector2 direction = GetDirection();
        Vector2 origin = transform.position;
        Vector2 start = origin - direction;
        Vector2 end = origin + direction;
        Gizmos.DrawSphere(start, 0.03f);
        Gizmos.DrawSphere(end, 0.03f);
        Gizmos.DrawLine(start, end);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<StairController>(out StairController player))
            player.AddStair(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<StairController>(out StairController player))
            player.SubStair();
    }
}
