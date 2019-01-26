using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class PlayerController : MonoBehaviour
    {
        public ActorInput Input;
        public Rigidbody2D Rigidbody;

        public float Speed = 5f;


        public IModificator Modificator { get; set; }


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

        protected virtual void UpdateMove()
        {
            if(Modificator != null)
            {
                Modificator.UpdateMove(this);
            }
            else
            {
                Rigidbody.velocity = Speed * new Vector2(Input.Horizontal_L, Input.Vertical_L).normalized;
            }
        }

        protected virtual void OnCollisionEnter(Collision col)
        {

        }

        protected virtual void OnTriggerEnter(Collider col)
        {
            var crabShell = col.gameObject.GetComponent<CrabShellController>();
            Debug.Log("Hit " + col.gameObject.name);
            if (crabShell != null)
            {
                crabShell.Bump();
            }
        }

        protected virtual void Update()
        {
            Input.UpdateInput();
            UpdateMove();



        }
    }
}