using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class CrabShellController : MonoBehaviour
    {
        private enum ShellState
        {
            notAvailable,
            availabe,
            taken,
        }

        private const string ANIM_HIT = "CrabHit";
        private const string ANIM_BACK = "CrabBack";

        public Collider2D Col;
        public ShellController Shell;
        public Animation Anim;

        public float HitIdle;

		public GameObject dialogueMad;
		public GameObject dialogueHappy;

		public GameObject VFXHit;


		private ShellState _shellState;
        private IEnumerator _bumping;

		private AudioSource _audioSource;

        private Save _save;
        private ShellType _initialShell;

		Quaternion savedRotation;
		Vector3 savedPosition;

		float timePositionReset = 0.5f;

		GameObject currentDialogue;

		bool haveIsOwnShell = true;

		ParticleSystem[] system;

        protected void Awake()
        {
            Shell.OnEnterShell = OnEnterShell;
            _shellState = ShellState.notAvailable;

			_audioSource = this.GetComponent<AudioSource>();

			savedPosition = this.transform.position;
			savedRotation = this.transform.rotation;

            _save = GameObject.Find("SaveController").GetComponent<Save>();

			system = this.GetComponentsInChildren<ParticleSystem>();

            _initialShell = Shell.Type;
		}

        protected void OnCollisionEnter2D(Collision2D col)
        {
            var player = this.Get<PlayerController>();
            if (col.gameObject == player.gameObject && 
                _bumping == null)
            {
                _bumping = Bump(col);

				if(currentDialogue != null)
				{
					Destroy(currentDialogue);
					currentDialogue = null;
				}

				if( _audioSource != null )
				{
					_audioSource.Play();
				}

				if(system != null && system.Length != 0)
				{
					foreach(ParticleSystem part in system)
					{
						part.Play();
					}
				}

                StartCoroutine(_bumping);
            }
        }

        private void OnEnterShell(Collider2D col)
        {
            if(_shellState == ShellState.availabe)
            {
				haveIsOwnShell = !haveIsOwnShell;

				_shellState = ShellState.taken;
                Shell.SwapWithPlayer();
                _save.SetFriendship((int)_initialShell - 1, haveIsOwnShell);
            }
        }

        private IEnumerator Bump(Collision2D col)
        {
            Col.isTrigger = true;
            var start = Time.time;

            yield return YieldPlay(ANIM_HIT);

            _shellState = ShellState.availabe;

            yield return new WaitForSeconds(HitIdle);

            yield return YieldPlay(ANIM_BACK);
            _shellState = ShellState.notAvailable;

			Vector3 position = transform.position;
			Quaternion rotation = transform.rotation;
			float time = 0f;

			while (transform.position.x != savedPosition.x)
			{
				time += 0.02f;

				transform.position = Vector3.Lerp(position, savedPosition, time / timePositionReset);
				transform.rotation = Quaternion.Lerp(rotation, savedRotation, time / timePositionReset);

				yield return new WaitForSeconds(0.02f);
			}

			if( dialogueMad != null)
			{
				if(!haveIsOwnShell)
				{
					currentDialogue = Instantiate(dialogueMad, this.transform);
				}
				else
				{
					currentDialogue = Instantiate(dialogueHappy, this.transform);
                }				
			}
            Col.isTrigger = false;
            _bumping = null;
        }

        private IEnumerator YieldPlay(string clipName)
        {
            var clip = Anim[clipName];
            Anim.Play(clipName);
            while (clip.enabled)
            {
                yield return null;
            }
        }
    }
}