using System;
using UnityEngine;


namespace ggj
{
    public class Save : MonoBehaviour
    {
        [Serializable]
        public class SaveState
        {
            public bool NewSave;
            public ShellType CurrentType;
            public bool[] Friendship;

            public SaveState()
            {
                CurrentType = ShellType.basic;
                Friendship = new bool[3];
                for (int i = 0, FriendshipLength = Friendship.Length; i < FriendshipLength; i++)
                {
                    Friendship[i] = true;
                }
            }

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
                saveCtrl.Reset();
            }
            return saveCtrl;
        }


        public void UpdateShell(ShellType previous, ShellType current)
        {
            State.CurrentType = current;
            Debug.Log("Set state to " + current);
        }

        protected void Awake()
        {
            State = new SaveState();
            DontDestroyOnLoad(gameObject);
        }

        public void Reset()
        {
            State = new SaveState();
        }

        public void SetFriendship(int friend, bool isFriend)
        {
            if (friend < 0 || friend >= State.Friendship.Length)
            {
                return;
            }
            State.Friendship[friend] = isFriend;
        }
    }
}
