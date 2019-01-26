using System.Collections;
using UnityEngine;

namespace ggj
{
	public class RainShadow : PoolObject
	{
		public float InitialSize;

		private float _speed;

		private const float kDestroyTimer = 3f;

		public override void Init()
		{
			base.Init();
		}

		public override void UpdateObject()
		{
			base.UpdateObject();

			if (IsActive)
			{
				Vector2 scale = _transform.localScale;
				float delta = Time.deltaTime * _speed;
				scale.x += delta;
				scale.y += delta;
				_transform.localScale = scale;
			}
		}

		public override void Activate(Vector2 position)
		{
			base.Activate(position);
			_transform.localScale = new Vector3(InitialSize, InitialSize, 1);
			_speed = (5 - InitialSize) / kDestroyTimer;
			Invoke("SpawnBullet", kDestroyTimer);
			Invoke("Destroy", kDestroyTimer);
		}

		public override void Destroy()
		{
			base.Destroy();
		}

		public void SpawnBullet()
		{
			float angle = 0;
			Vector2 velocity = new Vector2(0, 0);
			for (int i = 0; i < 8; ++i)
			{
				Bullet bullet = pool.Spawn("Bullet", _transform.position) as Bullet;
				if (bullet != null)
				{
					velocity.x = Mathf.Cos(angle * Mathf.Deg2Rad);
					velocity.y = Mathf.Sin(angle * Mathf.Deg2Rad);
					bullet.SetVelocity(velocity);
					angle += 45;
				}
				//yield return null;
			}
		}
	}
}