using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [HutongGames.PlayMaker.Tooltip("Stores a reference to this GameObject in a public GameObject variable.")]
    public class StoreSelfReference : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("The variable to store the reference to the GameObject.")]
        public FsmGameObject gameObjectReference;

        public override void Reset()
        {
            gameObjectReference = null;
        }

        public override void OnEnter()
        {
            gameObjectReference.Value = Owner.gameObject;
            Finish();
        }
    }
}
