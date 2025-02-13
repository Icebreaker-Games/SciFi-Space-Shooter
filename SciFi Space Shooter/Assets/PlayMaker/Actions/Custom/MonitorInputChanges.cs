using UnityEngine;
using HutongGames.PlayMaker;
using UnityEngine.InputSystem;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Input)]
   [HutongGames.PlayMaker.Tooltip("Fires an event whenever there is a change in the connected input devices.")]
    public class MonitorInputChanges : FsmStateAction
    {
       [HutongGames.PlayMaker.Tooltip("Event to send when an input device is connected or disconnected.")]
        public FsmEvent inputChangeEvent;

        private int lastDeviceCount;

        public override void Reset()
        {
            inputChangeEvent = null;
        }

        public override void OnEnter()
        {
            lastDeviceCount = InputSystem.devices.Count; // Initialize with the current count of devices
            InputSystem.onDeviceChange += OnDeviceChange; // Subscribe to device change events
        }

        public override void OnExit()
        {
            InputSystem.onDeviceChange -= OnDeviceChange; // Unsubscribe when exiting the state
        }

        private void OnDeviceChange(InputDevice device, InputDeviceChange changeType)
        {
            // Check if the change type is relevant to add or remove events
            if (changeType == InputDeviceChange.Added || changeType == InputDeviceChange.Removed)
            {
                if (InputSystem.devices.Count != lastDeviceCount)
                {
                    lastDeviceCount = InputSystem.devices.Count; // Update the last device count
                    Fsm.Event(inputChangeEvent); // Fire the event
                }
            }
        }
    }
}
