using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Logic)]
    [HutongGames.PlayMaker.Tooltip("Compares an integer value to a custom range of values and sends an event for the matching range.")]
    public class CompareIntToRange : FsmStateAction
    {
        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The integer value to compare.")]
        public FsmInt intValue;

        [CompoundArray("Ranges", "Range Start", "Event")]
        public FsmInt[] rangeStarts;
        public FsmEvent[] events;

        public override void Reset()
        {
            intValue = 0;
            rangeStarts = new FsmInt[0];
            events = new FsmEvent[0];
        }

        public override void OnEnter()
        {
            for (int i = 0; i < rangeStarts.Length; i++)
            {
                int rangeEnd = (i < rangeStarts.Length - 1) ? rangeStarts[i + 1].Value - 1 : intValue.Value;
                if (intValue.Value >= rangeStarts[i].Value && intValue.Value <= rangeEnd)
                {
                    Fsm.Event(events[i]);
                    break;
                }
            }

            Finish();
        }
    }
}
