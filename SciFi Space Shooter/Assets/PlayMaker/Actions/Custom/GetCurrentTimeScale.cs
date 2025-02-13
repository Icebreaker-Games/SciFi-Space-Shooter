using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Time)]
    [Tooltip("Gets the current time scale of the game.")]
    public class GetCurrentTimeScale : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [Tooltip("Variable to store the current time scale.")]
        public FsmFloat storeTimeScale;

        public override void Reset()
        {
            storeTimeScale = null;
        }

        public override void OnEnter()
        {
            storeTimeScale.Value = Time.timeScale;
            Finish();
        }
    }
}
