using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ggj
{
    public class MainMenu : MonoBehaviour
    {
        public ArrowObjects[] arrowObjects;

        private ActorInput _input;
        private int _currentSelection;

        private void Start()
        {
            _input = GetComponent<ActorInput>();
            _input.SetActorInput();
#if UNITY_STANDALONE_WIN
            _input.ControllerId = 0;
#else
            _input.ControllerId = 1;
#endif

            Select(0);
        }

        private void Update()
        {
            _input.UpdateInput();

            float verticalInput = _input.Vertical_L;
            if (verticalInput > 0.8f)
            {
                if (_currentSelection > 0)
                {
                    Deselect(_currentSelection);
                    Select(_currentSelection - 1);
                }
            }
            else if (verticalInput < -0.8f)
            {
                if (_currentSelection < arrowObjects.Length - 1)
                {
                    Deselect(_currentSelection);
                    Select(_currentSelection + 1);
                }
            }

            if (_input.A == ButtonState.pressing || _input.Start == ButtonState.pressing)
            {
                switch(_currentSelection)
                {
                    case 0:
                        // Intro
                        int index = SceneManager.GetActiveScene().buildIndex + 1;
                        SceneManager.LoadScene(index);
                        break;
                    case 1:
                        // Credits
                        SceneManager.LoadScene("Outro");
                        break;
                }
            }
        }

        private void Select(int option)
        {
            if (option < 0 || option >= arrowObjects.Length)
            {
                return;
            }

            arrowObjects[option].LeftArrow.SetActive(true);
            arrowObjects[option].RightArrow.SetActive(true);
            _currentSelection = option;
        }

        private void Deselect(int option)
        {
            if (option < 0 || option >= arrowObjects.Length)
            {
                return;
            }

            arrowObjects[option].LeftArrow.SetActive(false);
            arrowObjects[option].RightArrow.SetActive(false);
        }
    }

    [Serializable]
    public struct ArrowObjects
    {
        public GameObject LeftArrow;
        public GameObject RightArrow;
    }
}