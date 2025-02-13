using UnityEngine;
using HutongGames.PlayMaker;
using UnityEngine.InputSystem;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Input)]
   [HutongGames.PlayMaker.Tooltip("Detects input devices based on count and type using Unity's new Input System and fires events based on the device type.")]
    public class DetectInputDeviceByCountAndType : FsmStateAction
    {
       [HutongGames.PlayMaker.Tooltip("Event to send if an Xbox controller is detected.")]
        public FsmEvent xboxControllerEvent;

       [HutongGames.PlayMaker.Tooltip("Event to send if a PlayStation controller is detected.")]
        public FsmEvent playStationControllerEvent;

       [HutongGames.PlayMaker.Tooltip("Event to send if any other game controller is detected.")]
        public FsmEvent otherControllerEvent;

       [HutongGames.PlayMaker.Tooltip("Event to send if only a keyboard and mouse are detected.")]
        public FsmEvent keyboardAndMouseEvent;

        public override void Reset()
        {
            xboxControllerEvent = null;
            playStationControllerEvent = null;
            otherControllerEvent = null;
            keyboardAndMouseEvent = null;
        }

        public override void OnUpdate()
        {
            int deviceCount = InputSystem.devices.Count;
            if (deviceCount > 2)
            {
                // More than two devices, likely includes a controller
                CheckControllerType();
            }
            else
            {
                // Two or fewer devices, likely just keyboard and mouse
                Fsm.Event(keyboardAndMouseEvent);
            }
        }

        private void CheckControllerType()
        {
            foreach (var device in InputSystem.devices)
            {
                if (device is Gamepad gamepad) // Check only gamepad devices
                {
                    string deviceName = gamepad.name.ToLower();

                    if (deviceName.Contains("xbox") || deviceName.Contains("xinput"))
                    {
                        Fsm.Event(xboxControllerEvent);
                        return; // Exit once a specific type is identified
                    }
                    else if (deviceName.Contains("playstation") || deviceName.Contains("dualshock") || deviceName.Contains("dualsense"))
                    {
                        Fsm.Event(playStationControllerEvent);
                        return; // Exit once a specific type is identified
                    }
                }
            }

            // If no specific Xbox or PlayStation is detected, assume other controller
            Fsm.Event(otherControllerEvent);
        }
    }
}
