using UnityEngine;
using HutongGames.PlayMaker;
using System.Diagnostics;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [HutongGames.PlayMaker.Tooltip("Activates or deactivates all child GameObjects of a specified parent GameObject that contain a certain string in their names.")]
    public class ToggleChildrenByName : FsmStateAction
    {
        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The parent GameObject.")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("Part of the name to search for in child GameObjects.")]
        public FsmString nameContains;

        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("True to activate, false to deactivate.")]
        public FsmBool activate;

        public override void Reset()
        {
            gameObject = null;
            nameContains = "";
            activate = true; // Default to activate
        }

        public override void OnEnter()
        {
            DoToggleChildren();

            Finish(); // Finish the action immediately after execution
        }

        void DoToggleChildren()
        {
            // Get the parent GameObject
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go != null)
            {
                // Loop through all children
                foreach (Transform child in go.transform)
                {
                    // Check if the child's name contains the specified string
                    if (child.name.Contains(nameContains.Value))
                    {
                        child.gameObject.SetActive(activate.Value); // Activate or deactivate the child based on the activate flag
                    }
                }
            }
            else
            {
                UnityEngine.Debug.LogError("Parent GameObject not specified");
            }
        }
    }
}
