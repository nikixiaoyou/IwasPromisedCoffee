using UnityEngine;

namespace ggj
{
    public class Bullet : PoolObject
    {
        public void SetVelocity(Vector2 v)
        {
            _rigidbody.velocity = v;
        }
    }
}