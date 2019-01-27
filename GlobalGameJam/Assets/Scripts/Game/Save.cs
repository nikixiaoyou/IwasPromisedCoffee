using System;
using UnityEngine;


namespace ggj
{
    public class SaveState
    {
        public ShellType CurrentType;
    }

    public class SaveController
    {
        private const string STATE = "ggj_sav";

        public SaveState State { get; private set; }


        public SaveController()
        {
            var saveStr = PlayerPrefs.GetString(STATE);
            if(string.IsNullOrEmpty(saveStr))
            {
                State = new SaveState();
            }
            else 
            {
                State = JsonUtility.FromJson<SaveState>(saveStr);
            }

        }


        public void UpdateShell(ShellType previous, ShellType current)
        {
            State.CurrentType = current;
            Save();
        }


        private void Save()
        {
            var saveStr = JsonUtility.ToJson(State);
            PlayerPrefs.SetString(STATE, saveStr);
        }
    }
}
