using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DirectionFlip : MonoBehaviour
{
    public bool FacingLeft;

    private Vector3 _lastPos;
    private Transform _transform;
    private SpriteRenderer[] _spriteRenderers;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        _lastPos = _transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var currentX = _transform.position.x;
        if(!(Mathf.Approximately(_transform.position.x,_lastPos.x)) && (FacingLeft && _lastPos.x < currentX) || (!FacingLeft && _lastPos.x > currentX))
        {
            Flip();
        }
        _lastPos = _transform.position;
    }

    private void Flip()
    {
        foreach(var spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        FacingLeft = !FacingLeft;
    }
}
