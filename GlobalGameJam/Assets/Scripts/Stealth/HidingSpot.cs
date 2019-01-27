using ggj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        collider.attachedRigidbody.GetComponent<Stealth>().IsVisible = false;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        collider.attachedRigidbody.GetComponent<Stealth>().IsVisible = true;
    }
}
