using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public interface IModificator
    {
        void UpdateMove(PlayerController player);
    }
}