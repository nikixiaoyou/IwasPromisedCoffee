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
			if (player.ShellType != ShellType.tire)
			{
				player.Rigidbody.AddForce(new Vector2(player.Input.Horizontal_L, player.Input.Vertical_L) * player.Speed);
			}
			else
			{
				player.DefaultUpdateMove();
			}
		}
	}

    public UnityEvent OnIceEnter;
    public UnityEvent OnIceExit;

    private int _iceAmount = 0;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Ice>() != null)
        {
            OnIceEnter.Invoke();
            _iceAmount++;
            if(_iceAmount == 1)
            {
                SetIceDelegate();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Ice>() != null)
        {
            OnIceExit.Invoke();
             _iceAmount--;
			_iceAmount = _iceAmount > 0 ? _iceAmount - 1 : _iceAmount;
			if (_iceAmount == 0)

			{
				ClearIceDelegate();
            }
        }
    }

    private void SetIceDelegate()
    {
        this.Get<PlayerController>().Modificator = new IceMovementDelegate();
    }

    public void ClearIceDelegate()
    {
        this.Get<PlayerController>().Modificator = null;
		_iceAmount = 0;
    }
}
