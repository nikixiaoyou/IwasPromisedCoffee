using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class Pool : MonoBehaviour
    {
        private Dictionary<string, List<PoolObject>> _poolObj;

        protected void Start()
        {
            this.Register(this);

			_poolObj = new Dictionary<string, List<PoolObject>>();
            foreach (Transform t in transform)
            {
				PoolObject obj = t.GetComponent<PoolObject>();
				if (!_poolObj.ContainsKey(obj.ObjectName))
				{
					_poolObj.Add(obj.ObjectName, new List<PoolObject>());
				}
				obj.Init();
				obj.Destroy();
				obj.pool = this;
				_poolObj[obj.ObjectName].Add(obj);
			}
        }

        public PoolObject Spawn(string objName, Vector2 position)
        {
			if (_poolObj.ContainsKey(objName))
			{
				foreach(var obj in _poolObj[objName])
				{
					if (!obj.IsActive)
					{
						obj.Activate(position);
						return obj;
					}
				}
			}
			return null;
        }

		private void Update()
		{
			foreach(var keypair in _poolObj)
			{
				foreach(PoolObject obj in keypair.Value)
				{
					obj.UpdateObject();
				}
			}
		}

		public void Reset()
		{
			if (_poolObj == null)
			{
				return;
			}

			foreach (var keypair in _poolObj)
			{
				foreach (var obj in keypair.Value)
				{
					obj.Destroy();
				}
			}
		}
	}
}