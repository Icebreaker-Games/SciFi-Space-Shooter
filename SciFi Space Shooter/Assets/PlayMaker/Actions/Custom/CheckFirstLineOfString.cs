using UnityEngine;
using HutongGames.PlayMaker;
using UnityEngine.UI; // Ensure this namespace is specified for UI components

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UI")]
    [HutongGames.PlayMaker.Tooltip("Checks if the first line of a UI Text component contains a specified string.")]
    public class CheckFirstLineOfString : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(Text))]
        [HutongGames.PlayMaker.Tooltip("The GameObject with the UI Text component.")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The string value to check for.")]
        public FsmString stringValue;

        [HutongGames.PlayMaker.Tooltip("Event to send if the string value is found.")]
        public FsmEvent stringFoundEvent;

        [HutongGames.PlayMaker.Tooltip("Event to send if the string value is not found.")]
        public FsmEvent stringNotFoundEvent;

        public override void Reset()
        {
            gameObject = null;
            stringValue = "";
            stringFoundEvent = null;
            stringNotFoundEvent = null;
        }

        public override void OnEnter()
        {
            DoCheckString();
            Finish();
        }

        void DoCheckString()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go != null)
            {
                UnityEngine.UI.Text textComponent = go.GetComponent<UnityEngine.UI.Text>();
                if (textComponent != null)
                {
                    string[] lines = textComponent.text.Split('\n');
                    if (lines.Length > 0 && lines[0].Contains(stringValue.Value))
                    {
                        Fsm.Event(stringFoundEvent);
                    }
                    else
                    {
                        Fsm.Event(stringNotFoundEvent);
                    }
                }
            }
        }
    }
}
