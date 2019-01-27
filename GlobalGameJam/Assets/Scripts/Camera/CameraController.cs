using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class CameraController : MonoBehaviour
    {
        public Vector2 Margins;


        protected void Awake()
        {
            this.Register(this);
        }

        protected void OnDestroy()
        {
            this.UnRegister(this);
        }


        protected void LateUpdate()
        {
        }
    }
}