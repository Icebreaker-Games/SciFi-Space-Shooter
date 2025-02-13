using HutongGames.PlayMaker;

namespace CustomActions
{
    [ActionCategory("Custom Actions")]
    [HutongGames.PlayMaker.Tooltip("Resets specified boolean variables to false.")]
    public class ResetBoolsToFalse : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("The boolean variables to reset.")]
        public FsmBool[] boolVariables;

        public override void Reset()
        {
            // Initialize your variables here
            boolVariables = new FsmBool[0];
        }

        public override void OnEnter()
        {
            // Loop through each boolean variable and set it to false
            foreach (var boolVar in boolVariables)
            {
                if (boolVar != null)
                {
                    boolVar.Value = false;
                }
            }

            // Always call Finish() at the end of OnEnter() to indicate that the action is done
            Finish();
        }
    }
}
