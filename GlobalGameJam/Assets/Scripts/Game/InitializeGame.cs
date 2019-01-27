using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public class InitializeGame : MonoBehaviour
    {
        private void Awake()
        {
            this.Get<SaveController>().Reset();
        }
    }
}