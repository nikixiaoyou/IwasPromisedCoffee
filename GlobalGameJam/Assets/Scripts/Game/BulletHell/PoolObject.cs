using UnityEngine;

namespace ggj
{
    public class PoolObject : MonoBehaviour
    {
		public Pool pool;
        public string ObjectName;
        public bool IsActive;

		protected Transform _transform;
        protected Rigidbody2D _rigidbody;

        public virtual void Init()
        {
			_transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public virtual void Activate(Vector2 position)
        {
            IsActive = true;
			_transform.position = position;
            gameObject.SetActive(true);
        }

        public virtual void Destroy()
        {
            IsActive = false;
            gameObject.SetActive(false);
			CancelInvoke();
        }

		public virtual void UpdateObject()
		{

		}
	}
}