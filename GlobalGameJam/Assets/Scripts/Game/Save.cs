using System;
using UnityEngine;


namespace ggj
{
    public class SaveController : MonoBehaviour
    {
        public class SaveState
        {
            public bool NewSave;
            public ShellType CurrentType;
        }


        private const string STATE = "ggj_sav";

        public SaveState State { get; private set; }


        public static SaveController GetOrCreate()
        {
            SaveController saveCtrl = null;
            var saveInstance = GameObject.Find("SaveController");
            if(saveInstance != null)
            {
                saveCtrl = saveInstance.GetComponent<SaveController>();
            }
            if (saveCtrl == null)
            {
                var go = new GameObject("SaveController");
                saveCtrl = go.AddComponent<SaveController>();
            }
            return saveCtrl;
        }


        public void UpdateShell(ShellType previous, ShellType current)
        {
            State.CurrentType = current;
        }

        protected void Awake()
        {
            State = new SaveState();
            DontDestroyOnLoad(gameObject);
        }

    }
}
