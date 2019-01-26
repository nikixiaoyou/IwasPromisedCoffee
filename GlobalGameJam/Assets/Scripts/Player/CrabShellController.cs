using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class CrabShellController : MonoBehaviour
    {
        public Collider2D Col;
        public Transform Crab;
        public Transform Shell;

        public float Distance;
        public float OutTime;
        public float BackTime;

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
            var c = col.contacts[0];
            var dir = -1f * c.normal;


            while(Time.time < start + OutTime)
            {


                yield return null;
            }
            Col.isTrigger = false;
            _bumping = null;
            Debug.Log("done!");
        }
    }
}