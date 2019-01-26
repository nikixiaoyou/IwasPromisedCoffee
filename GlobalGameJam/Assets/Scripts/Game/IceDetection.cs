using ggj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IceDetection : MonoBehaviour
{
    public class IceMovementDelegate : IModificator
    {
        public void UpdateMove(PlayerController player)
        {
            player.Rigidbody.AddForce(new Vector2(player.Input.Horizontal_L, player.Input.Vertical_L) * player.Speed);
        }
    }

    public UnityEvent OnIceEnter;
    public UnityEvent OnIceExit;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Ice>() != null)
        {
            OnIceEnter.Invoke();
            SetIceDelegate();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Ice>() != null)
        {
            OnIceExit.Invoke();
            ClearIceDelegate();
        }
    }

    private void SetIceDelegate()
    {
        this.Get<PlayerController>().Modificator = new IceMovementDelegate();
    }

    private void ClearIceDelegate()
    {
        this.Get<PlayerController>().Modificator = null;
    }
}
