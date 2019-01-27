using ggj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IceDetection : MonoBehaviour
{
    private static float _kAccelerationMutliplier = 60.0f;

    public class IceMovementDelegate : IModificator
    {
        public void UpdateMove(PlayerController player)
        {
			if (player.ShellType != ShellType.tire)
			{
				player.Rigidbody.AddForce(new Vector2(player.Input.Horizontal_L, player.Input.Vertical_L) * player.Speed * _kAccelerationMutliplier * Time.deltaTime);
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
			_iceAmount = _iceAmount > 0 ? _iceAmount - 1 : 0;
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
