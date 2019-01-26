using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class PlayerController : MonoBehaviour
    {
        public ActorInput Input;
        public Rigidbody2D Rigidbody;
        public float Speed = 2f;


        protected void Awake()
        {
            this.Register(this);
            Input.SetActorInput();

#if UNITY_STANDALONE_WIN
            Input.ControllerId = 0;
#else
            Input.ControllerId = 1;
#endif
        }

        protected void OnDestroy()
        {
            this.UnRegister(this);
        }

        protected virtual void Update()
        {
            Input.UpdateInput();

            var v = Speed * new Vector2(Input.Horizontal_L, Input.Vertical_L);

            Rigidbody.velocity = v;
        }
    }
}