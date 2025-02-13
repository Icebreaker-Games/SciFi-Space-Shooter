using UnityEngine;
using HutongGames.PlayMaker;

namespace CustomActions
{
    [ActionCategory("Custom Actions")]
    [HutongGames.PlayMaker.Tooltip("Toggles emission on/off for the GameObject's shared material.")]
    public class ToggleSharedMaterialEmission : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(Renderer))]
        [HutongGames.PlayMaker.Tooltip("The GameObject with the shared material to toggle emission on/off.")]
        public FsmOwnerDefault gameObject;

        [HutongGames.PlayMaker.Tooltip("True to turn emission on, false to turn it off.")]
        public FsmBool turnEmissionOn;

        public override void Reset()
        {
            gameObject = null;
            turnEmissionOn = false;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
               // UnityEngine.Debug.LogError("ToggleSharedMaterialEmission: GameObject is null.");
                Finish();
                return;
            }

            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer == null)
            {
               // UnityEngine.Debug.LogError("ToggleSharedMaterialEmission: Renderer component not found on the GameObject.");
                Finish();
                return;
            }

            Material mat = renderer.sharedMaterial;
            if (mat == null)
            {
                //UnityEngine.Debug.LogError("ToggleSharedMaterialEmission: Shared material not found on the renderer.");
                Finish();
                return;
            }

            if (turnEmissionOn.Value)
            {
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.white);
                //UnityEngine.Debug.Log("Shared material emission turned ON.");
            }
            else
            {
                mat.DisableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.black);
                //UnityEngine.Debug.Log("Shared material emission turned OFF.");
            }

            Finish();
        }
    }
}
