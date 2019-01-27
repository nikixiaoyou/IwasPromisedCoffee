using System;
using UnityEngine;

namespace ggj
{
    public class Outro : MonoBehaviour
    {
        public CreditsCrabs[] Crabs; // Need to be in the same order as ShellType
        public GameObject PlayerShell;


        private Save _save;

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