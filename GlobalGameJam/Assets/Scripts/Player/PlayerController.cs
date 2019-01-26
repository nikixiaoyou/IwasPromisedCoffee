using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class PlayerController : MonoBehaviour
    {
        protected void Awake()
        {
            this.Register(this);
        }

        protected void OnDestroy()
        {
            this.UnRegister(this);
        }
    }
}