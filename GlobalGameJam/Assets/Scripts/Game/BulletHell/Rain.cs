using System.Collections;
using UnityEngine;

namespace ggj
{
    public class Rain : MonoBehaviour
    {
		public float XMin;
		public float XMax;
		public float YMin;
		public float YMax;

		private Pool _pool;
		private Vector2 _delayedRainPosition;

		private const float kRainTimer = 1.1f;
		private const float kRainDropDelay = 2f;
		private const float kBombStart = 3f;

        private void Start()
        {
			_pool = GameObject.Find("Pool").GetComponent<Pool>();

            InvokeRepeating("StartRain", kRainTimer,kRainTimer);
        }

        private void StartRain()
        {
			SpawnRain();
        }

		private void SpawnRain()
		{
			_delayedRainPosition = new Vector2(Random.Range(XMin, XMax), Random.Range(YMin, YMax));
			_pool.Spawn("RainShadow", _delayedRainPosition);
			Invoke("SpawnRainDrop", kRainDropDelay);
		}

		private void SpawnRainDrop()
		{
			_pool.Spawn("RainDrop", _delayedRainPosition + new Vector2(0, 10));
		}
	}
}