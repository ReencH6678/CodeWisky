using UnityEngine;

public class TriggerLayerChange : MonoBehaviour
{
    [SerializeField] private Transform _ladderMidle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<LayerChanger>(out LayerChanger layerChanger))
        {
            if (GetMoverDirection(collision.gameObject.transform.position).y > 0)
                layerChanger.ChanngeDown();
            else
                layerChanger.ChangeUp();
        }
    }

    private Vector2 GetMoverDirection(Vector3 mover)
    {
        Vector2 moveDirection = mover - _ladderMidle.position;
        return moveDirection;
    }
}
