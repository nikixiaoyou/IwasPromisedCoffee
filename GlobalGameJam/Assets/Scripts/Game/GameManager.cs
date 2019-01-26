using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; private set; }

        public Services Services { get; private set; }

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            Services = new Services();
        }
    }
}
