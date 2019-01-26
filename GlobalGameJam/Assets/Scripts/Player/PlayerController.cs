using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class PlayerController : MonoBehaviour
    {
        private const string ANIM_WALK = "Walk";
        private const string ANIM_SPEED = "Speed";

        public ActorInput Input;
        public Rigidbody2D Rigidbody;
        public Animator Anim;

        public float Speed = 5f;
        public float AnimSpeed = 2f;
        public float Epsilon = 0.05f;

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

        protected virtual void UpdateAnimations()
        {
            var mag = Rigidbody.velocity.magnitude;
            Anim.SetBool(ANIM_WALK, mag > Epsilon);
            Anim.SetFloat(ANIM_SPEED, Mathf.Lerp(0f, AnimSpeed, mag / Speed));
        }

        protected virtual void OnCollisionEnter2D(Collision2D col)
        {

        }

        protected virtual void Update()
        {
            Input.UpdateInput();
            UpdateMove();
            UpdateAnimations();
        }
    }
}