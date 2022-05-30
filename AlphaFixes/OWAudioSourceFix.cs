using HarmonyLib;
using System.Reflection;
namespace AlphaFixes
{
    internal class OWAudioSourceFix : GameFix
    {        
        void Awake()
        {
            FixName = "OWAudioSource";
        }        
        public override void Innit()
        {
            if (!IsFixEnabled.Value)
                return;

            MethodInfo OWAudioSourceAwake = AccessTools.Method(typeof(OWAudioSource), "Awake");
            MethodInfo OWAudioSourceOnDestroy = AccessTools.Method(typeof(OWAudioSource), "OnDestroy");

            HarmonyMethod awakePrefix = new HarmonyMethod(typeof(OWAudioSourceFix), nameof(OWAudioSourceFix.OWAudioSourceAwakePrefix));
            HarmonyMethod onDestroyPrefix = new HarmonyMethod(typeof(OWAudioSourceFix), nameof(OWAudioSourceFix.OWAudioSourceOnDestroyPrefix));

            AlphaFixesMod.Instance.HarmonyInstance.Patch(OWAudioSourceAwake, prefix: awakePrefix);
            AlphaFixesMod.Instance.HarmonyInstance.Patch(OWAudioSourceOnDestroy, prefix: onDestroyPrefix);
        }

        public static bool OWAudioSourceAwakePrefix(OWAudioSource __instance) 
        {
            if (Locator.GetAudioMixer() == null)
            {
                __instance.enabled = false;
                return false;
            }
            return true;
        }
        public static bool OWAudioSourceOnDestroyPrefix(OWAudioSource __instance)
        {
            return __instance._audioTrack != null;
        }
    }
}
