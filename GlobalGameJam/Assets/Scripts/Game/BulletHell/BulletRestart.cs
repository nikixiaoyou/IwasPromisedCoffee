using UnityEngine;

namespace ggj
{
	public class BulletRestart : MonoBehaviour
	{
		private Pool pool;
		private Player player;
		public GameObject DeadVFX;
		public int MaxHp;

		private PlayerController _playerController;
		private int _hp;


		private void Start()
		{
			player = this.GetComponent<Player>();
			pool = GameObject.Find("Pool").GetComponent<Pool>();

			_playerController = GetComponent<PlayerController>();


		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (col.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
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
			player.Reset();

			if (pool != null)
			{
				pool.Reset();
			}

			_hp = MaxHp;
		}


		public void SetHp(int hp)
		{
			_hp = hp;
		}
	}
}