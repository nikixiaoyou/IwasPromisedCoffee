using System;
using UnityEngine;


namespace ggj
{
    public class Save : MonoBehaviour
    {
        public bool[] Friendship;

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
                saveCtrl.Friendship = new bool[3];
                saveCtrl.Reset();
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

        public void Reset()
        {
            for (int i = 0; i < Friendship.Length; ++i)
            {
                Friendship[i] = true;
            }
        }

        public void SetFriendship(int friend, bool isFriend)
        {
            if (friend < 0 || friend >= Friendship.Length)
            {
                return;
            }
            Friendship[friend] = isFriend;
        }
    }
}
