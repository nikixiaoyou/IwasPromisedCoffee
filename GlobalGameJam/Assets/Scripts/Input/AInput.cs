using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ggj
{
    public enum ButtonState
    {
        none,
        down,
        up,
        pressing,
    }

    public enum ControllerType
    {
        keyboard,
        x360,
        steam,
    }

    public abstract class AInput : MonoBehaviour
    {
        protected string _horizontalL = "Horizontal_L";
        protected string _verticalL = "Vertical_L";
        protected string _horizontalR = "Horizontal_R";
        protected string _verticalR = "Vertical_R";
        protected string _horizontalDpad = "Horizontal_DPad";
        protected string _verticalDpad = "Vertical_DPad";
        protected string _leftTrigger = "Left_Trigger";
        protected string _rightTrigger = "Right_Trigger";


        public abstract void UpdateInput();


        protected KeyCode ParseKeyCode(int player, int button)
        {
            return string.Format("Joystick{0}Button{1}",
                                 player > 0 ? player.ToString() : "",
                                 button).GetEnum<KeyCode>();
        }

        protected string FormatAxis(string axisName, int player)
        {
            return string.Format("{0}_{1}", axisName, player);
        }

        protected ButtonState GetButtonState(KeyCode code)
        {
            var state = ButtonState.none;
            if (Input.GetKeyDown(code))
            {
                state = ButtonState.down;
            }
            else if (Input.GetKey(code))
            {
                state = ButtonState.pressing;
            }
            else if (Input.GetKeyUp(code))
            {
                state = ButtonState.up;
            }
            return state;
        }

#if UNITY_STANDALONE_WIN
        protected ButtonState GetButtonState(ButtonState prevState, XInputDotNetPure.ButtonState button)
        {
            var state = ButtonState.none;
            if (button == XInputDotNetPure.ButtonState.Pressed && prevState == ButtonState.none)
            {
                state = ButtonState.down;
            }
            else if (button == XInputDotNetPure.ButtonState.Pressed && (prevState == ButtonState.down || prevState == ButtonState.pressing))
            {
                state = ButtonState.pressing;
            }
            else if (button == XInputDotNetPure.ButtonState.Released && prevState == ButtonState.pressing)
            {
                state = ButtonState.up;
            }
            return state;
        }
#endif

        protected float GetButtonAxisValue(KeyCode code)
        {
            var value = 0f;
            if (Input.GetKeyDown(code) || Input.GetKey(code) || Input.GetKeyUp(code))
            {
                value = 1f;
            }
            return value;
        }

        protected ButtonState GetButtonsState(KeyCode[] keys)
        {
            var state = ButtonState.none;
            for (int i = 0; i < keys.Length; i++)
            {
                var current = GetButtonState(keys[i]);
                if (state == ButtonState.down) break;
                if (current != ButtonState.none)
                {
                    state = current;
                }
            }
            return state;
        }

        protected float FakeAxis(KeyCode left, KeyCode right)
        {
            return Input.GetKey(left) ? -1f :
                   Input.GetKey(right) ? 1f : 0f;
        }
    }
}