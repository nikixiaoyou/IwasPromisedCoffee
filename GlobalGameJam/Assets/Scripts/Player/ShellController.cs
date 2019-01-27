using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ggj
{
    public enum ShellType
    {
        basic,
        tire,
        rock,
        bush,
    }

    public class ShellController : MonoBehaviour
    {
        public ShellType Type;
        public SpriteRenderer Shell;


        public Action<Collider2D> OnEnterShell { get; set; }


        public void SwapWithPlayer()
        {
            var player = this.Get<PlayerController>();

            // Texture
            var playerShell = player.Shell.sprite;
            var shell = Shell.sprite;
            player.Shell.sprite = shell;
            Shell.sprite = playerShell;

            // Type
            var playerType = player.ShellType;
            var shellType = Type;
            player.ShellType = shellType;
            Type = playerType;
			player.SwappedShellCallback(shellType);

            this.Get<Save>().UpdateShell(playerType, shellType);
        }


        protected void OnTriggerEnter2D(Collider2D collision)
        {
            OnEnterShell(collision);
        }
    }
}