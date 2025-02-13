using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [HutongGames.PlayMaker.Tooltip("Finds an active GameObject's child, compares it to an array of GameObjects, and returns the index.")]
    public class FindActiveChildAndReturnIndex : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The parent GameObject to search through.")]
        public FsmOwnerDefault parentGameObject;

        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.GameObject)]
        public FsmArray gameObjectsArray;

        [UIHint(UIHint.Variable)]
        public FsmInt index;

        [HutongGames.PlayMaker.Tooltip("Whether to perform this action every frame.")]
        public FsmBool everyFrame;

        public override void Reset()
        {
            parentGameObject = null;
            gameObjectsArray = null;
            index = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoFindActiveChildAndReturnIndex();

            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                DoFindActiveChildAndReturnIndex();
            }
        }

        private void DoFindActiveChildAndReturnIndex()
        {
            GameObject parent = Fsm.GetOwnerDefaultTarget(parentGameObject);
            if (parent == null)
            {
                return;
            }

            GameObject activeChild = null;
            foreach (Transform child in parent.transform)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    // Assuming you want to check the children of the found active child
                    foreach (Transform grandChild in child)
                    {
                        if (grandChild.gameObject.activeInHierarchy)
                        {
                            activeChild = grandChild.gameObject;
                            break;
                        }
                    }
                }

                if (activeChild != null)
                {
                    break;
                }
            }

            if (activeChild != null)
            {
                for (int i = 0; i < gameObjectsArray.Length; i++)
                {
                    if (gameObjectsArray.Get(i) as GameObject == activeChild)
                    {
                        index.Value = i;
                        return;
                    }
                }

                // If the active child was not found in the array
                index.Value = -1;
            }
        }
    }
}
