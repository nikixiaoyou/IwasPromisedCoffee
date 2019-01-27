using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class PlayerController : MonoBehaviour
    {
        private const string ANIM_WALK = "Walk";
        private const string ANIM_SPEED = "Speed";
        private const string ANIM_HIDE = "Hide";

        public ActorInput Input;
        public Rigidbody2D Rigidbody;
        public Animator Anim;
        public ShellType ShellType;
        public SpriteRenderer Shell;

        public float Speed = 5f;
        public float AnimSpeed = 2f;
        public float Epsilon = 0.05f;

        public IModificator Modificator { get; set; }

        public bool IsHidden { get; private set; }

        private AudioSource _audioSource;


        protected void Awake()
        {
            this.Register(this);
            Input.SetActorInput();

			_audioSource = this.GetComponent<AudioSource>();
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
			Vector2 inputAxis = new Vector2(Input.Horizontal_L, Input.Vertical_L);

			if (Modificator != null)
            {
                Modificator.UpdateMove(this);
            }
            else
            {
				
				if (inputAxis.magnitude > 1)
				{
					inputAxis = inputAxis.normalized;
				}

				Rigidbody.velocity = Speed * inputAxis;
            }


			if(inputAxis != Vector2.zero && _audioSource != null)
			{
				_audioSource.pitch = inputAxis.magnitude * 1.5f;
			}
			else
			{
				_audioSource.pitch = 0;
			}

        }

        protected virtual void UpdateAnimations()
        {
            // Walk / Idle animations
            var mag = Rigidbody.velocity.magnitude;
            Anim.SetBool(ANIM_WALK, mag > Epsilon);
            Anim.SetFloat(ANIM_SPEED, Mathf.Lerp(0f, AnimSpeed, mag / Speed));

            // Hide under shell if stealth
            if(ShellType == ShellType.bush)
            {
                IsHidden = mag < Epsilon;
                Anim.SetBool(ANIM_HIDE, IsHidden);
            }
            else
            {
                Anim.SetBool(ANIM_HIDE, false);
            }

            // Flip x
            if (Mathf.Abs(Rigidbody.velocity.x) > Epsilon)
            {
                var s = Anim.transform.localScale;
                s.x = Mathf.Abs(s.x) * Mathf.Sign(Rigidbody.velocity.x);
                Anim.transform.localScale = s;
            }
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