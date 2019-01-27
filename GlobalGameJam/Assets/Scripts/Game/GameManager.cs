using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public ServicesLocator Services { get; private set; }

        public void Destroy()
        {
            if (Instance == this)
            {
                Services.UnregisterAll();
                Instance = null;
            }
        }

        protected void Awake()
        {
            Instance = this;
            Services = new ServicesLocator();

            // Default services
            this.Register(new SaveController());
        }
    }
}
