using System;
using UnityEngine;


namespace ggj
{
    public class Save : MonoBehaviour
    {
        public class SaveState
        {
            public bool NewSave;
            public ShellType CurrentType;
        }


        private const string STATE = "ggj_sav";

        public SaveState State { get; private set; }


        public static Save GetOrCreate()
        {
			Save saveCtrl = null;
            var saveInstance = GameObject.Find("SaveController");
            if(saveInstance != null)
            {
                saveCtrl = saveInstance.GetComponent<Save>();
            }
            if (saveCtrl == null)
            {
                var go = new GameObject("SaveController");
                saveCtrl = go.AddComponent<Save>();
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
