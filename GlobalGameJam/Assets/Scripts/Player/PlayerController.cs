using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class PlayerController : MonoBehaviour
    {
        public ActorInput Input;

        protected void Awake()
        {
            this.Register(this);
            Input.SetActorInput();
        }

        protected void OnDestroy()
        {
            this.UnRegister(this);
        }

        protected void Update()
        {
            Input.UpdateInput();
        }
    }
}