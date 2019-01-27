using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ggj
{
    public class Outro : MonoBehaviour
    {
        public CreditsCrabs[] Crabs; // Need to be in the same order as ShellType
        public GameObject PlayerShell; // Player selfie shell
        public SpriteRenderer Shell; // Movable player's shell
        public Sprite BushShell;
        public ActorInput PlayerInput;

        private Save _save;

        private bool _canContinue;

        private void Start()
        {
            _save = GameObject.Find("SaveController").GetComponent<Save>();

            bool playerShellActive = true;
            for (int i = 0; i < Crabs.Length; ++i)
            {
                bool friend = _save.Friendship[i];
                CreditsCrabs crab = Crabs[i];
                crab.Crab.SetActive(friend);
                crab.Selfie.SetActive(friend);
                crab.SelfieShell.SetActive(friend);

                if (friend)
                {
                    playerShellActive = false;
                }
            }
            PlayerShell.SetActive(playerShellActive);

            if (playerShellActive)
            {
                Shell.sprite = BushShell;
            }

            _canContinue = false;
            Invoke("AllowContinue", 3f);
        }

        private void Update()
        {
            if (_canContinue)
            {
                if (PlayerInput.Start == ButtonState.down)
                {
                    SceneManager.LoadScene(0);
                }
            }
        }

        private void AllowContinue()
        {
            _canContinue = true;
        }
    }

    [Serializable]
    public struct CreditsCrabs
    {
        public GameObject Crab;
        public GameObject Selfie;
        public GameObject SelfieShell;
    }
}