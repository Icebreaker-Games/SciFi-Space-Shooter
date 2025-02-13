using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [HutongGames.PlayMaker.Tooltip("Finds the active child of a specified parent GameObject and activates either the next or previous child based on a boolean flag.")]
    public class ActivateNextOrPreviousChild : FsmStateAction
    {
        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The parent GameObject whose children will be iterated over.")]
        public FsmOwnerDefault parentGameObject;

        [HutongGames.PlayMaker.Tooltip("Whether to loop back to the start/end when the end/start is reached.")]
        public FsmBool loop;

        [HutongGames.PlayMaker.Tooltip("If true, activates the previous child. If false, activates the next child.")]
        public FsmBool goBackwards;

        public override void Reset()
        {
            parentGameObject = null;
            loop = false;
            goBackwards = false;
        }

        public override void OnEnter()
        {
            DoActivateChild();

            Finish();
        }

        void DoActivateChild()
        {
            GameObject parent = Fsm.GetOwnerDefaultTarget(parentGameObject);
            if (parent == null || parent.transform.childCount == 0)
            {
                return;
            }

            int activeChildIndex = -1;
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                if (parent.transform.GetChild(i).gameObject.activeSelf)
                {
                    activeChildIndex = i;
                    break; // Found the active child
                }
            }

            // Determine the next/previous index based on the direction and whether looping is enabled
            int nextIndex = CalculateNextIndex(activeChildIndex, parent.transform.childCount, goBackwards.Value, loop.Value);

            // Activate the next/previous child and deactivate others
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                parent.transform.GetChild(i).gameObject.SetActive(i == nextIndex);
            }
        }

        int CalculateNextIndex(int currentIndex, int totalChildren, bool goBackwards, bool loop)
        {
            if (goBackwards)
            {
                if (currentIndex <= 0)
                {
                    return loop ? totalChildren - 1 : 0;
                }
                else
                {
                    return currentIndex - 1;
                }
            }
            else
            {
                if (currentIndex >= totalChildren - 1)
                {
                    return loop ? 0 : totalChildren - 1;
                }
                else
                {
                    return currentIndex + 1;
                }
            }
        }
    }
}
