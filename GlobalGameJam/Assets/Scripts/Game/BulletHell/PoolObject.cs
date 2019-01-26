using UnityEngine;

namespace ggj
{
    public class PoolObject : MonoBehaviour
    {
        public string ObjectName;
        public bool IsActive;

        protected Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }

        public void Destroy()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }
    }
}