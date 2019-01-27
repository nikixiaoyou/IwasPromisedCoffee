using UnityEngine;

namespace ggj
{
	public class BulletRestart : MonoBehaviour
	{
		public Vector2 InitialPosition;
		public GameObject DeadVFX;
		public int MaxHp;

		private Pool pool;
		private PlayerController _playerController;
		private int _hp;

		private void Start()
		{
			pool = GameObject.Find("Pool").GetComponent<Pool>();
			_playerController = GetComponent<PlayerController>();

			RestartLevel();
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (col.tag == "Bullet")
			{
				_hp--;

				if (_playerController.ShellType != ShellType.rock || _hp <= 0)
				{
					RestartLevel();
					col.GetComponent<Bullet>().Destroy();
				}
			}
		}

		private void RestartLevel()
		{
			Instantiate(DeadVFX, transform.position, Quaternion.identity);
			transform.position = InitialPosition;

			if (pool != null)
			{
				pool.Reset();
			}

			_hp = MaxHp;
		}
	}
}