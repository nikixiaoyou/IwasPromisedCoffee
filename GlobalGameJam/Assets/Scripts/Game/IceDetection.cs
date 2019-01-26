using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IceDetection : MonoBehaviour
{
    public class IceMovementDelegate : IModificator
    {
        public void UpdateMove()
        {
            Rigidbody.AddForce(new Vector2(Input.Horizontal_L, Input.Vertical_L) * Speed);
        }
    }

    public UnityEvent OnIceEnter;
    public UnityEvent OnIceExit;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Ice>() != null)
        {
            OnIceEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Ice>() != null)
        {
            OnIceExit.Invoke();
        }
    }

    private void SetIceDelegate()
    {

    }
}
