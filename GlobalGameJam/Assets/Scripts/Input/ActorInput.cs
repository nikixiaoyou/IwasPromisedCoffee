using System.Collections;
using UnityEngine;
#if UNITY_STANDALONE_WIN
using XInputDotNetPure;
#endif


namespace ggj
{
    public class ActorInput : AInput
    {
        public const float JOYSTICK_EPSILON = 0.1f;
       
        private const float HEART_BEAT_PERIOD_MIN = 0.3f;
        private const float HEART_BEAT_PERIOD_MAX = 1.3f;
        private const float HEART_BEAT_STRENGTH_MIN = 0.6f;
        private const float HEART_BEAT_STRENGTH_MAX = 1f;
        private const int BEAT_FRAMES = 3;


        private KeyCode _x = KeyCode.None;
        private KeyCode _y = KeyCode.None;
        private KeyCode _a = KeyCode.None;
        private KeyCode _b = KeyCode.None;
        private KeyCode _l1 = KeyCode.None;
        private KeyCode _r1 = KeyCode.None;
        private KeyCode _l3 = KeyCode.None;
        private KeyCode _r3 = KeyCode.None;
        private KeyCode _start = KeyCode.None;
        private KeyCode _select = KeyCode.None;

        private KeyCode _up = KeyCode.None;
        private KeyCode _down = KeyCode.None;
        private KeyCode _right = KeyCode.None;
        private KeyCode _left = KeyCode.None;

        private bool _vibrate;
        private float _vibrateTime;
        private float _vibrateDuration;

        private bool _heartBeat;
        private float _heartBeatTime;
        private float _heartBeatStrength;
        private float _heartBeatDuration;
        private IEnumerator _heartBeatEnum;

        public ControllerType CtrlChoice;
        public int ControllerId = 1;

        public float Horizontal_L { get; protected set; }
        public float Vertical_L { get; protected set; }
        public float Horizontal_R { get; protected set; }
        public float Vertical_R { get; protected set; }
        public float Horizontal_DPad { get; protected set; }
        public float Vertical_DPad { get; protected set; }
        public float Left_Trigger { get; protected set; }
        public float Right_Trigger { get; protected set; }

        public ButtonState X { get; protected set; }
        public ButtonState Y { get; protected set; }
        public ButtonState A { get; protected set; }
        public ButtonState B { get; protected set; }
        public ButtonState L1 { get; protected set; }
        public ButtonState R1 { get; protected set; }

        public ButtonState L3 { get; protected set; }
        public ButtonState R3 { get; protected set; }

        public ButtonState Start { get; protected set; }
        public ButtonState Select { get; protected set; }


        public void SetActorInput()
        {
            // Axis
            _horizontalL = FormatAxis(_horizontalL, ControllerId);

            _verticalL = FormatAxis(_verticalL, ControllerId);
            _horizontalR = FormatAxis(_horizontalR, ControllerId);
            _verticalR = FormatAxis(_verticalR, ControllerId);
            _horizontalDpad = FormatAxis(_horizontalDpad, ControllerId);
            _verticalDpad = FormatAxis(_verticalDpad, ControllerId);
            _leftTrigger = FormatAxis(_leftTrigger, ControllerId);
            _rightTrigger = FormatAxis(_rightTrigger, ControllerId);

            // Buttons
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX
            _x = ParseKeyCode(ControllerId, 2);
            _y = ParseKeyCode(ControllerId, 3);
            _a = ParseKeyCode(ControllerId, 0);
            _b = ParseKeyCode(ControllerId, 1);
            _l1 = ParseKeyCode(ControllerId, 4);
            _r1 = ParseKeyCode(ControllerId, 5);

#if UNITY_STANDALONE_WIN
            _l3 = ParseKeyCode(ControllerId, 8);
            _r3 = ParseKeyCode(ControllerId, 9);
#elif UNITY_STANDALONE_LINUX
        _l3 = ParseKeyCode(ControllerId, 9);
        _r3 = ParseKeyCode(ControllerId, 10);
#endif

            _start = ParseKeyCode(ControllerId, 7);
            _select = ParseKeyCode(ControllerId, 6);
#elif UNITY_STANDALONE_OSX
        _x = ParseKeyCode(ControllerId, 18);
        _y = ParseKeyCode(ControllerId, 19);
        _a = ParseKeyCode(ControllerId, 16);
        _b = ParseKeyCode(ControllerId, 17);
        _l1 = ParseKeyCode(ControllerId, 13);
        _r1 = ParseKeyCode(ControllerId, 14);
        _l3 = ParseKeyCode(ControllerId, 11);
        _r3 = ParseKeyCode(ControllerId, 12);

        _start = ParseKeyCode(ControllerId, 9);
        _select = ParseKeyCode(ControllerId, 10);

        _right = ParseKeyCode(ControllerId, 5);
        _left = ParseKeyCode(ControllerId, 6);
        _down = ParseKeyCode(ControllerId, 7);
        _up = ParseKeyCode(ControllerId, 8);
#endif
        }

        public override void UpdateInput()
        {
            switch (CtrlChoice)
            {
                case ControllerType.x360:
#if UNITY_STANDALONE_WIN
                    UpdateX360win();
#else
                     UpdateX360osx();                    
#endif
                    break;
                case ControllerType.steam:
                    UpdateSteam();
                    break;
                case ControllerType.keyboard:
                    UpdateKeyboard();
                    break;
            }
            UpdateVibrations();
        }

        public void SetVibrations(float left, float right)
        {
#if UNITY_STANDALONE_WIN
            GamePad.SetVibration((PlayerIndex)ControllerId, left, right);
#endif
        }

        public void Vibrate(float time)
        {
            _vibrate = true;
            _vibrateTime = Time.time;
            _vibrateDuration = time;
        }

        public void StartHeartBeat(float speedRatio)
        {
            _heartBeat = true;
            _heartBeatTime = Time.time;
            _heartBeatDuration = Mathf.Lerp(HEART_BEAT_PERIOD_MAX, HEART_BEAT_PERIOD_MIN, speedRatio);
            _heartBeatStrength = Mathf.Lerp(HEART_BEAT_STRENGTH_MIN, HEART_BEAT_STRENGTH_MAX, speedRatio);
        }

        public void StopHeartBeat()
        {
            _heartBeat = false;
        }

        protected void Awake()
        {
            _heartBeatEnum = HeartBeatEnum();
        }

        protected void OnDestroy()
        {
            SetVibrations(0f, 0f);
            _heartBeatEnum = null;
        }

        protected void ResetInput()
        {
            Horizontal_L = 0f;
            Vertical_L = 0f;
            Horizontal_R = 0f;
            Vertical_R = 0f;
            Horizontal_DPad = 0f;
            Vertical_DPad = 0f;
            Left_Trigger = 0f;
            Right_Trigger = 0f;
            X = ButtonState.none;
            Y = ButtonState.none;
            A = ButtonState.none;
            B = ButtonState.none;
            L1 = ButtonState.none;
            R1 = ButtonState.none;
            L3 = ButtonState.none;
            R3 = ButtonState.none;
            Start = ButtonState.none;
            Select = ButtonState.none;
        }


        private void UpdateVibrations()
        {
            if(_vibrate)
            {
                // Lerp vibrations to 1, then back to 0
                if(Time.time < _vibrateTime + _vibrateDuration)
                {
                    var halfDur = 0.5f * _vibrateDuration;
                    if (Time.time < _vibrateTime + halfDur)
                    {
                        var t = (Time.time - _vibrateTime) / halfDur;
                        var v = Mathf.Lerp(0f, 1f, t);
                        SetVibrations(v, v);
                    }
                    else
                    {
                        var t = (Time.time - _vibrateTime - halfDur) / halfDur;
                        var v = Mathf.Lerp(1f, 0f, t);
                        SetVibrations(v, v);
                    }
                }
                else
                {
                    _vibrate = false;
                }
            }
            else if (!_vibrate && _heartBeat)
            {
                if (Time.time < _heartBeatTime + _heartBeatDuration)
                {
                    if (_heartBeatEnum != null && _heartBeatEnum.MoveNext()) { }
                }
                else
                {                    
                    _heartBeatEnum = HeartBeatEnum();
                    _heartBeatTime = Time.time;
                }
            }
            else
            {
                SetVibrations(0f, 0f);
            }
        }

        private IEnumerator HeartBeatEnum()
        {
            var _startTime = Time.time;
            for (int i = 0; i < BEAT_FRAMES; i++)
            {
                SetVibrations(_heartBeatStrength, _heartBeatStrength);
                yield return null;
            }
            SetVibrations(0f, 0f);
        }


#if UNITY_STANDALONE_WIN
        private void UpdateX360win()
        {
            var state = GamePad.GetState((PlayerIndex)ControllerId);
            if (state.IsConnected)
            {
                // Axis
                Horizontal_L = state.ThumbSticks.Left.X;
                Vertical_L = state.ThumbSticks.Left.Y;
                Horizontal_R = state.ThumbSticks.Right.X;
                Vertical_R = state.ThumbSticks.Right.Y;

                Horizontal_DPad = state.DPad.Left == XInputDotNetPure.ButtonState.Pressed ? -1f :
                                  state.DPad.Right == XInputDotNetPure.ButtonState.Pressed ? 1f :
                                  0f;
                Vertical_DPad = state.DPad.Up == XInputDotNetPure.ButtonState.Pressed ? 1f :
                                state.DPad.Down == XInputDotNetPure.ButtonState.Pressed ? -1f :
                                0f;

                Left_Trigger = state.Triggers.Left;
                Right_Trigger = state.Triggers.Right;

                // Buttons
                X = GetButtonState(X, state.Buttons.X);
                Y = GetButtonState(Y, state.Buttons.Y);
                A = GetButtonState(A, state.Buttons.A);
                B = GetButtonState(B, state.Buttons.B);
                L1 = GetButtonState(L1, state.Buttons.LeftShoulder);
                R1 = GetButtonState(R1, state.Buttons.RightShoulder);
                L3 = GetButtonState(L3, state.Buttons.LeftStick);
                R3 = GetButtonState(R3, state.Buttons.RightStick);
                Start = GetButtonState(Start, state.Buttons.Start);
                Select = GetButtonState(Select, state.Buttons.Back);
            }
            else
            {
                ResetInput();
            }
        }
#endif

        // X360 controller on windows platforms
        private void UpdateX360osx()
        {
            // Axis
            Horizontal_L = Input.GetAxis(_horizontalL);
            Vertical_L = Input.GetAxis(_verticalL);
            Horizontal_R = Input.GetAxis(_horizontalR);
            Vertical_R = Input.GetAxis(_verticalR);

            Horizontal_DPad = FakeAxis(_down, _up);
            Vertical_DPad = FakeAxis(_left, _right);


            var lt = Input.GetAxis(_leftTrigger);
            Left_Trigger = (Mathf.Approximately(lt, 0f)) ? 0f : (lt + 1f) / 2f;
            var rt = Input.GetAxis(_rightTrigger);
            Right_Trigger = (Mathf.Approximately(rt, 0f)) ? 0f : (rt + 1f) / 2f;

            // Buttons
            X = GetButtonState(_x);
            Y = GetButtonState(_y);
            A = GetButtonState(_a);
            B = GetButtonState(_b);
            L1 = GetButtonState(_l1);
            R1 = GetButtonState(_r1);
            L3 = GetButtonState(_l3);
            R3 = GetButtonState(_r3);
            Start = GetButtonState(_start);
            Select = GetButtonState(_select);
        }

        // Steam desktop mapping
        private void UpdateSteam()
        {
            // Axis
            Horizontal_L = FakeAxis(KeyCode.LeftArrow, KeyCode.RightArrow);
            Vertical_L = FakeAxis(KeyCode.DownArrow, KeyCode.UpArrow);

            Horizontal_R = Input.GetAxis(_horizontalR);
            Vertical_R = Input.GetAxis(_verticalR);

            Horizontal_DPad = Input.mouseScrollDelta.x;//Input.GetAxis(_horizontalDpad);
            Vertical_DPad = Input.mouseScrollDelta.y;
            //LR2 = Input.GetAxis(_lr2);

            X = GetButtonState(KeyCode.PageUp);
            Y = GetButtonState(KeyCode.PageDown);
            A = GetButtonState(KeyCode.Return);
            B = GetButtonState(KeyCode.Space);

            L1 = GetButtonState(KeyCode.LeftControl);
            R1 = GetButtonState(KeyCode.LeftAlt);
            Start = GetButtonState(KeyCode.Escape);
            Select = GetButtonState(KeyCode.Tab);
        }

        // Keyboard mapping
        private void UpdateKeyboard()
        {
            // Keyboad mapping when no controller...
            Horizontal_L = FakeAxis(KeyCode.LeftArrow, KeyCode.RightArrow);
            Vertical_L = FakeAxis(KeyCode.DownArrow, KeyCode.UpArrow);

            Left_Trigger = FakeAxis(KeyCode.LeftShift, KeyCode.RightShift);
            A = GetButtonState(KeyCode.Space);
            X = GetButtonState(KeyCode.LeftControl);

            Start = GetButtonState(KeyCode.Return);
            Select = GetButtonState(KeyCode.Space);
        }
    }
}