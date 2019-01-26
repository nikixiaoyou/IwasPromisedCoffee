using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ggj
{
    public class ShellController : MonoBehaviour
    {
        public Action<Collider2D> OnEnterShell { get; set; }
        public SpriteRenderer Shell;

        public void SwapWithPlayer()
        {
            var player = this.Get<PlayerController>();

            var playerShell = player.Shell.sprite;
            var shell = Shell.sprite;

            player.Shell.sprite = shell;
            Shell.sprite = playerShell;
        }


        protected void OnTriggerEnter2D(Collider2D collision)
        {
            OnEnterShell(collision);
        }
    }
}