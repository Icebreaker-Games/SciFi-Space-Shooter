using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [HutongGames.PlayMaker.Tooltip("Activates a specific child of a GameObject based on index and deactivates all other children.")]
    public class ActivateSpecificChild : FsmStateAction
    {
        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The parent GameObject whose children will be managed.")]
        public FsmOwnerDefault parentGameObject;

        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The index of the child to activate.")]
        public FsmInt childIndex;

        [HutongGames.PlayMaker.Tooltip("The event to send if the child index is out of range.")]
        public FsmEvent indexOutOfRangeEvent;

        public override void Reset()
        {
            parentGameObject = null;
            childIndex = 0;
            indexOutOfRangeEvent = null;
        }

        public override void OnEnter()
        {
            DoActivateChild();
            Finish(); // Finish the action unless you need it to run every frame
        }

        private void DoActivateChild()
        {
            GameObject parent = Fsm.GetOwnerDefaultTarget(parentGameObject);
            if (parent == null)
            {
                return;
            }

            if (childIndex.Value < 0 || childIndex.Value >= parent.transform.childCount)
            {
                // If index is out of range, send the appropriate event
                //UnityEngine.Debug.LogError("Child index is out of range.");
                if (indexOutOfRangeEvent != null)
                {
                    Fsm.Event(indexOutOfRangeEvent);
                }
                return;
            }

            // Activate the specified child and deactivate others
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                Transform child = parent.transform.GetChild(i);
                child.gameObject.SetActive(i == childIndex.Value);
            }
        }
    }
}
