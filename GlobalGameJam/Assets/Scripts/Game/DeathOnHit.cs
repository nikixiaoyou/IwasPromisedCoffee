using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathOnHit : MonoBehaviour
{
    public UnityEvent OnDeath;

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Die(collider.gameObject);
    }

    public virtual void Die(GameObject player)
    {
        OnDeath.Invoke();
    }
}
