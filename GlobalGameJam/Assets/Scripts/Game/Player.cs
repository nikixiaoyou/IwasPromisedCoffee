using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 _startingPosition;
    // Start is called before the first frame update
    private void Start()
    {
        _startingPosition = this.transform.position;
    }

    public void Reset()
    {
        this.transform.position = _startingPosition;
        var rigidBody = this.GetComponent<Rigidbody2D>();
        if(rigidBody!= null)
        {
            rigidBody.velocity = Vector2.zero;
        }
    }

}
