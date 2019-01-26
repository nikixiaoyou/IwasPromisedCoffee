using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

namespace ggj.editor
{
    public class InputWriter : ScriptableObject
    {
        private const string PATH = "ProjectSettings/InputManager.asset";
        private const string HEADER =
    @"%YAML 1.1
%TAG !u! tag:unity3d.com,2011:
--- !u!13 &1
InputManager:
  m_ObjectHideFlags: 0
  serializedVersion: 2
  m_Axes:
";

        private const string EVENT_SYSTEM_REQUIRED =
    @"  - serializedVersion: 3
    m_Name: Horizontal
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: left
    positiveButton: right
    altNegativeButton: a
    altPositiveButton: d
    gravity: 3
    dead: 0.001
    sensitivity: 3
    snap: 1
    invert: 0
    type: 0
    axis: 0
    joyNum: 0
  - serializedVersion: 3
    m_Name: Vertical
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: down
    positiveButton: up
    altNegativeButton: s
    altPositiveButton: w
    gravity: 3
    dead: 0.001
    sensitivity: 3
    snap: 1
    invert: 0
    type: 0
    axis: 1
    joyNum: 0
  - serializedVersion: 3
    m_Name: Submit
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: 
    positiveButton: return
    altNegativeButton: 
    altPositiveButton: joystick button 0
    gravity: 1000
    dead: 0.001
    sensitivity: 1000
    snap: 0
    invert: 0
    type: 0
    axis: 0
    joyNum: 0
  - serializedVersion: 3
    m_Name: Cancel
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: 
    positiveButton: escape
    altNegativeButton: 
    altPositiveButton: joystick button 1
    gravity: 1000
    dead: 0.001
    sensitivity: 1000
    snap: 0
    invert: 0
    type: 0
    axis: 0
    joyNum: 0
  - serializedVersion: 3
    m_Name: Mouse X
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: 
    positiveButton: 
    altNegativeButton: 
    altPositiveButton: 
    gravity: 0
    dead: 0
    sensitivity: 0.1
    snap: 0
    invert: 0
    type: 1
    axis: 0
    joyNum: 0
  - serializedVersion: 3
    m_Name: Mouse Y
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: 
    positiveButton: 
    altNegativeButton: 
    altPositiveButton: 
    gravity: 0
    dead: 0
    sensitivity: 0.1
    snap: 0
    invert: 0
    type: 1
    axis: 1
    joyNum: 0
  - serializedVersion: 3
    m_Name: Mouse ScrollWheel
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: 
    positiveButton: 
    altNegativeButton: 
    altPositiveButton: 
    gravity: 0
    dead: 0
    sensitivity: 0.1
    snap: 0
    invert: 0
    type: 1
    axis: 2
    joyNum: 0
";


        private const string RAW_INPUT =
    @"  - serializedVersion: 3
    m_Name: Horizontal_L_{0}
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: left
    positiveButton: right
    altNegativeButton: a
    altPositiveButton: d
    gravity: 0
    dead: 0.15
    sensitivity: 1
    snap: 1
    invert: 0
    type: 2
    axis: 0
    joyNum: {0}
  - serializedVersion: 3
    m_Name: Vertical_L_{0}
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: down
    positiveButton: up
    altNegativeButton: s
    altPositiveButton: w
    gravity: 0
    dead: 0.15
    sensitivity: 1
    snap: 1
    invert: 1
    type: 2
    axis: 1
    joyNum: {0}
  - serializedVersion: 3
    m_Name: Horizontal_R_{0}
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: down
    positiveButton: up
    altNegativeButton: s
    altPositiveButton: w
    gravity: 0
    dead: 0.15
    sensitivity: 1
    snap: 1
    invert: 0
    type: 2
    axis: {1}
    joyNum: {0}
  - serializedVersion: 3
    m_Name: Vertical_R_{0}
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: down
    positiveButton: up
    altNegativeButton: s
    altPositiveButton: w
    gravity: 0
    dead: 0.15
    sensitivity: 1
    snap: 1
    invert: 1
    type: 2
    axis: {2}
    joyNum: {0}
  - serializedVersion: 3
    m_Name: Horizontal_DPad_{0}
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: down
    positiveButton: up
    altNegativeButton: s
    altPositiveButton: w
    gravity: 0
    dead: 0.1
    sensitivity: 1
    snap: 1
    invert: 0
    type: 2
    axis: {3}
    joyNum: {0}
  - serializedVersion: 3
    m_Name: Vertical_DPad_{0}
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: down
    positiveButton: up
    altNegativeButton: s
    altPositiveButton: w
    gravity: 0
    dead: 0.1
    sensitivity: 1
    snap: 1
    invert: 0
    type: 2
    axis: {4}
    joyNum: {0}
  - serializedVersion: 3
    m_Name: Left_Trigger_{0}
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: down
    positiveButton: up
    altNegativeButton: s
    altPositiveButton: w
    gravity: 0
    dead: 0.1
    sensitivity: 1
    snap: 1
    invert: 0
    type: 2
    axis: {5}
    joyNum: {0}
  - serializedVersion: 3
    m_Name: Right_Trigger_{0}
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: down
    positiveButton: up
    altNegativeButton: s
    altPositiveButton: w
    gravity: 0
    dead: 0.1
    sensitivity: 1
    snap: 1
    invert: 0
    type: 2
    axis: {6}
    joyNum: {0}

";

        [MenuItem("GameTools/WriteInputManagerAsset")]
        public static void WriteInput()
        {
            if (File.Exists(PATH))
            {
                File.WriteAllText(PATH, MakeFile(4));
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.LogFormat("Wrote {0} for {1}!", PATH, Application.platform);
            }
            else
            {
                Debug.LogErrorFormat("No file at {0}.", PATH);
            }
        }

        private static string MakeFile(int controllerCount)
        {
#pragma warning disable XS0001 // Find usages of mono todo items
            var sw = new StringBuilder();
#pragma warning restore XS0001 // Find usages of mono todo items
            sw.Append(HEADER);
            sw.Append(EVENT_SYSTEM_REQUIRED);

            string horizontal_r = null;
            string vertical_r = null;
            string horizontal_dpad = null;
            string vertical_dpad = null;
            string left_trigger = null;
            string right_trigger = null;

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    horizontal_r = "3";
                    vertical_r = "4";
                    horizontal_dpad = "5";
                    vertical_dpad = "6";
                    left_trigger = "2";
                    right_trigger = "2";
                    break;
                case RuntimePlatform.OSXEditor:
                    horizontal_r = "2";
                    vertical_r = "3";
                    horizontal_dpad = "-1";
                    vertical_dpad = "-1";
                    left_trigger = "4";
                    right_trigger = "5";
                    break;
                case RuntimePlatform.LinuxEditor:
                    horizontal_r = "4";
                    vertical_r = "5";
                    horizontal_dpad = "7";
                    vertical_dpad = "8";
                    left_trigger = "3";
                    right_trigger = "6";
                    break;
            }

            // 0 is all joysticks
            for (int i = 0; i < controllerCount + 1; i++)
            {
                sw.Append(string.Format(RAW_INPUT, i, horizontal_r, vertical_r,
                                        horizontal_dpad, vertical_dpad,
                                        left_trigger, right_trigger));
            }
            return sw.ToString();
        }
    }
}