using UnityEngine;

namespace ggj
{
    public class Bullet : PoolObject
    {
		public float Speed;

		private const float kWidthBorder = 25f;
		private const float kHeightBorder = 15f;

        public void SetVelocity(Vector2 v)
        {
            _rigidbody.velocity = v * Speed;
        }

		public override void UpdateObject()
		{
			base.UpdateObject();

			if (Mathf.Abs(_transform.position.x) > kWidthBorder || Mathf.Abs(_transform.position.y) > kHeightBorder)
			{
				Destroy();
			}
		}
	}
}