using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [HutongGames.PlayMaker.Tooltip("Finds a child GameObject with a matching numerical value in the name.")]
    public class FindChildWithNumericalValue : FsmStateAction
    {
        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The parent GameObject to search.")]
        public FsmGameObject parentGameObject;

        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The GameObject with the numerical value in the name.")]
        public FsmGameObject gameObjectWithNumber;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Store the result: the child GameObject with a matching numerical value in the name.")]
        public FsmGameObject storeResult;

        public override void Reset()
        {
            parentGameObject = null;
            gameObjectWithNumber = null;
            storeResult = null;
        }

        public override void OnEnter()
        {
            FindMatchingChild();
            Finish();
        }

        private void FindMatchingChild()
        {
            if (parentGameObject.Value == null || gameObjectWithNumber.Value == null)
            {
                return;
            }

            string numberInName = GetNumberFromString(gameObjectWithNumber.Value.name);

            foreach (Transform child in parentGameObject.Value.transform)
            {
                if (child.name.Contains(numberInName))
                {
                    storeResult.Value = child.gameObject;
                    return;
                }
            }

            // If no matching child is found, you can optionally set storeResult to null
            storeResult.Value = null;
        }

        private string GetNumberFromString(string input)
        {
            string number = string.Empty;

            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    number += c;
                }
            }

            return number;
        }
    }
}
