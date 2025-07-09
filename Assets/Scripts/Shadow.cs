using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private float _shadowYRatio;

    private GameObject _shadowObject;
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _shadowObject = GetComponent<GameObject>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        CreateShadow();
    }

    private void Update()
    {
        UpdateShadow();
    }

    private void CreateShadow()
    {
        _shadowObject = new GameObject(name + "_Shadow");
        _shadowObject.transform.SetParent(transform);
        _shadowObject.transform.localPosition = Vector3.zero;

        var shadowRenderer = _shadowObject.AddComponent<SpriteRenderer>();
        shadowRenderer.sprite = _spriteRenderer.sprite;
        shadowRenderer.color = new Color(0, 0, 0, 0.3f);
        shadowRenderer.sortingOrder = _spriteRenderer.sortingOrder -1;
        shadowRenderer.sortingLayerName = _spriteRenderer.sortingLayerName;

        _shadowObject.transform.localScale = new Vector3(1f, 0.6f, 1f);
        _shadowObject.transform.rotation = Quaternion.Euler(0, 0, -25);
    }

    private void UpdateShadow()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(0, _spriteRenderer.bounds.max.y), Vector3.forward);
        Collider2D currentHit = null;

        Vector3 currentScale = _shadowObject.transform.localScale;

        if(hit.collider != null && hit.collider != currentHit)
        {
            if (hit.collider.gameObject.TryGetComponent<Wall>(out _))
            {
                _shadowObject.transform.localScale = new Vector3(currentScale.x, currentScale.y * _shadowYRatio, currentScale.z);
                currentHit = hit.collider;
            }
        }

    }
}
