using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class Pool : MonoBehaviour
    {
        private List<PoolObject> _poolObj;

        protected void Start()
        {
            this.Register(this);

            foreach (Transform t in transform)
            {
                _poolObj.Add(t.GetComponent<PoolObject>());
            }
        }

        public PoolObject Spawn(string objName)
        {
            foreach (var obj in _poolObj)
            {
                if (!obj.IsActive)
                {
                    obj.Activate();
                    return obj;
                }
            }
            return null;
        }
    }
}