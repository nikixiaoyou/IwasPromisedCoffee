using UnityEngine;

namespace ggj
{
	public class BulletRestart : MonoBehaviour
	{
		public Vector3 InitialPosition;
		

		private Pool pool;
		private Player player;

		private void Start()
		{
			InitialPosition = this.transform.position;
			player = this.GetComponent<Player>();
			pool = GameObject.Find("Pool").GetComponent<Pool>();
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (col.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
			{
				RestartLevel();
			}
		}

		private void RestartLevel()
		{
			player.Reset();

			if (pool != null)
			{
				pool.Reset();
			}
		}
	}
}