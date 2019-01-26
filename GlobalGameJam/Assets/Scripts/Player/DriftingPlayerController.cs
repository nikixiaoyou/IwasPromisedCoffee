using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class DriftingPlayerController : PlayerController
    {
        protected override void Update()
        {
            Input.UpdateInput();

            Rigidbody.AddForce(new Vector2(Input.Horizontal_L, Input.Vertical_L) * Speed);
        }
    }
}