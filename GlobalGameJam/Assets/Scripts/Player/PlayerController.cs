using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class PlayerController : MonoBehaviour
    {
        public ActorInput Input;
        public Rigidbody2D Rigidbody;
		public float MoveSpeed;

        protected void Awake()
        {
            this.Register(this);
            Input.SetActorInput();
        }

        protected void OnDestroy()
        {
            this.UnRegister(this);
        }

        protected void Update()
        {
            Input.UpdateInput();

            Rigidbody.velocity = new Vector2(Input.Horizontal_L, Input.Vertical_L) * MoveSpeed;
        }
    }
}