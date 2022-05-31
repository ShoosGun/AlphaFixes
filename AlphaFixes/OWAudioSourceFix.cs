using HarmonyLib;
using System.Reflection;
using UnityEngine;
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
            
            MethodInfo LocatorGetAudioMixer = AccessTools.Method(typeof(Locator), "GetAudioMixer");
            HarmonyMethod getAudioMixerPostfix = new HarmonyMethod(typeof(OWAudioSourceFix), nameof(OWAudioSourceFix.GetAudioMixerPostfix));
            AlphaFixesMod.Instance.HarmonyInstance.Patch(LocatorGetAudioMixer, postfix: getAudioMixerPostfix);
        }
        private static AudioMixer audioMixerInstance;
        public static void GetAudioMixerPostfix(ref AudioMixer __result) 
        {
            if(__result == null && Application.loadedLevel == 0) 
            {
                if(audioMixerInstance == null)
                    audioMixerInstance = GameObject.Find("Root").GetComponent<AudioMixer>();

                __result = audioMixerInstance;
            }
        }
    }
}
