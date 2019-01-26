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


        private ShellState _shellState;
        private IEnumerator _bumping;

        protected void Awake()
        {
            Shell.OnEnterShell = OnEnterShell;
            _shellState = ShellState.notAvailable;
        }

        protected void OnCollisionEnter2D(Collision2D col)
        {
            var player = this.Get<PlayerController>();
            if (col.gameObject == player.gameObject && 
                _bumping == null)
            {
                _bumping = Bump(col);
                StartCoroutine(_bumping);
            }
        }

        private void OnEnterShell(Collider2D col)
        {
            if(_shellState == ShellState.availabe)
            {
                _shellState = ShellState.taken;
                Shell.SwapWithPlayer();
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