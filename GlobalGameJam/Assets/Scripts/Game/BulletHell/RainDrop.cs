using UnityEngine;

namespace ggj
{
	public class RainDrop : PoolObject
	{
		private float _dropSpeed;

		private const float kDestroyTimer = 1f;

		public override void UpdateObject()
		{
			base.UpdateObject();

			if (_transform == null)
			{
				Debug.Log("H");
			}

			_transform.position -= Time.deltaTime * new Vector3(0, _dropSpeed);
		}

		public override void Activate(Vector2 position)
		{
			base.Activate(position);

			_dropSpeed = 10f / kDestroyTimer;
			Invoke("Destroy", kDestroyTimer);
		}
	}
}