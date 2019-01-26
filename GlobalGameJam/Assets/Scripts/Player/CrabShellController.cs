using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class CrabShellController : MonoBehaviour
    {
        private const string ANIM_HIT = "CrabHit";
        private const string ANIM_BACK = "CrabBack";

        public Collider2D Col;
        public Animation Anim;

        public float HitIdle;


        private IEnumerator _bumping;

        protected void OnCollisionEnter2D(Collision2D col)
        {
            var player = this.Get<PlayerController>();
            if (col.gameObject == player.gameObject && _bumping == null)
            {
                _bumping = Bump(col);
                StartCoroutine(_bumping);
            }
        }

        public IEnumerator Bump(Collision2D col)
        {
            Debug.Log("bump.");
            Col.isTrigger = true;
            var start = Time.time;

            yield return YieldPlay(ANIM_HIT);
            yield return new WaitForSeconds(HitIdle);

            yield return YieldPlay(ANIM_BACK);

            Col.isTrigger = false;
            _bumping = null;
            Debug.Log("done!");
        }

        private IEnumerator YieldPlay(string clipName)
        {
            Debug.Log("play " + clipName);
            var clip = Anim[clipName];
            Anim.Play(clipName);
            while (clip.enabled)
            {
                yield return null;
            }
        }
    }
}