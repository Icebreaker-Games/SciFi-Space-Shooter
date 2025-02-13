using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [HutongGames.PlayMaker.Tooltip("Searches all children and grandchildren of a specific GameObject for a GameObject with a name that matches a public string.")]
    public class FindDeepChildByName : FsmStateAction
    {
        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The parent GameObject to start the search from.")]
        public FsmOwnerDefault parentGameObject;

        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The name of the child to search for.")]
        public FsmString childName;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Store the found GameObject.")]
        public FsmGameObject storeResult;

        public override void Reset()
        {
            parentGameObject = null;
            childName = null;
            storeResult = null;
        }

        public override void OnEnter()
        {
            DoFindChildByName();
            Finish();
        }

        private void DoFindChildByName()
        {
            GameObject parent = Fsm.GetOwnerDefaultTarget(parentGameObject);
            if (parent == null || string.IsNullOrEmpty(childName.Value))
            {
                return;
            }

            Transform result = FindDeepChild(parent.transform, childName.Value);
            storeResult.Value = result != null ? result.gameObject : null;
        }

        private Transform FindDeepChild(Transform parent, string nameToFind)
        {
            if (parent.name == nameToFind)
                return parent;

            foreach (Transform child in parent)
            {
                Transform result = FindDeepChild(child, nameToFind);
                if (result != null)
                    return result;
            }

            return null; // Not found
        }
    }
}
