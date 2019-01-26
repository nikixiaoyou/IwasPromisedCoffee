using UnityEngine;

namespace ggj
{
	public class BulletRestart : MonoBehaviour
	{
		public Vector2 InitialPosition;

		private Pool pool;

		private void Start()
		{
			pool = GameObject.Find("Pool").GetComponent<Pool>();

			RestartLevel();
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (col.tag == "Bullet")
			{
				RestartLevel();
			}
		}

		private void RestartLevel()
		{
			transform.position = InitialPosition;

			if (pool != null)
			{
				pool.Reset();
			}
		}
	}
}