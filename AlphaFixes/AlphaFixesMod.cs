using BepInEx;
using System;
using HarmonyLib;

namespace AlphaFixes
{
    [BepInPlugin("Locochoco.OWA.AlphaFixes", "Alpha Fixes", "1.1.0")]
    [BepInProcess("OuterWilds_Alpha_1_2.exe")]
    public class AlphaFixesMod : BaseUnityPlugin
    {
        public static AlphaFixesMod Instance;
        public Harmony HarmonyInstance;
        public void Awake()
        {
            if(Instance != null) 
                Destroy(this);
            Instance = this;

            HarmonyInstance = new Harmony("com.locochoco.OWA.AlphaFixes");

            var gameFixes = new Type[]
            {
                typeof(FrameRateFix),
                typeof(OWAudioSourceFix),
            };
            foreach(var fix in gameFixes) 
            {
                GameFix gameFixComponent = (GameFix)gameObject.AddComponent(fix);
                gameFixComponent.IsFixEnabled = Config.Bind("Fixes",
                                         gameFixComponent.FixName,
                                         true,
                                         $"Enables the {gameFixComponent.FixName} fix");
                gameFixComponent.Innit();
            }
        }
    } 
}

