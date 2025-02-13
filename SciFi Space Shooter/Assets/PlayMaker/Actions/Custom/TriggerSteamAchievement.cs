using UnityEngine;
using HutongGames.PlayMaker;
using Steamworks;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Steam")]
   [HutongGames.PlayMaker.Tooltip("Triggers a Steam achievement using the specified ID.")]
    public class TriggerSteamAchievement : FsmStateAction
    {
        [RequiredField]
       [HutongGames.PlayMaker.Tooltip("The ID of the Steam achievement to trigger.")]
        public FsmString achievementID;

       [HutongGames.PlayMaker.Tooltip("Event to send if the achievement is successfully triggered.")]
        public FsmEvent successEvent;

       [HutongGames.PlayMaker.Tooltip("Event to send if the achievement cannot be triggered.")]
        public FsmEvent failureEvent;

        // Registering for Steam callbacks
        protected Callback<UserStatsStored_t> onUserStatsStored;

        public override void Reset()
        {
            achievementID = null;
            successEvent = null;
            failureEvent = null;
            onUserStatsStored = Callback<UserStatsStored_t>.Create(OnUserStatsStored);
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (!SteamManager.Initialized)
            {
                UnityEngine.Debug.Log("Steam is not initialized.");
                Fsm.Event(failureEvent);
                Finish();
                return;
            }

            if (string.IsNullOrEmpty(achievementID.Value))
            {
                UnityEngine.Debug.Log("Achievement ID is empty or null.");
                Fsm.Event(failureEvent);
                Finish();
                return;
            }

            if (SteamUserStats.SetAchievement(achievementID.Value))
            {
                SteamUserStats.StoreStats();
                UnityEngine.Debug.Log("Achievement unlock requested: " + achievementID.Value);
            }
            else
            {
                UnityEngine.Debug.Log("Failed to request achievement unlock.");
                Fsm.Event(failureEvent);
                Finish();
            }
        }

        // Callback for when user stats are stored
        private void OnUserStatsStored(UserStatsStored_t callback)
        {
            if (callback.m_nGameID == SteamUtils.GetAppID().m_AppId)  // Corrected comparison here
            {
                if (callback.m_eResult == EResult.k_EResultOK)
                {
                    UnityEngine.Debug.Log("Achievement unlocked and stats stored successfully.");
                    Fsm.Event(successEvent);
                }
                else
                {
                    UnityEngine.Debug.LogError("Failed to store stats: " + callback.m_eResult);
                    Fsm.Event(failureEvent);
                }
            }
            Finish();
        }
    }
}
