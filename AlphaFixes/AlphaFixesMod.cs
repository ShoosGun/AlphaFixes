using UnityEngine;
using BepInEx;
using BepInEx.Configuration;

namespace AlphaFixes
{
    [BepInPlugin("Locochoco.OWA.AlphaFixes", "Alpha Fixes", "1.0.0")]
    [BepInProcess("OuterWilds_Alpha_1_2.exe")]
    public class AlphaFixesMod : BaseUnityPlugin
    {
        private ConfigEntry<int> targetFrameRate;
        private ConfigEntry<int> physicsFrameRate;
        private ConfigEntry<bool> showFPSCounter;
        void Awake() 
        {
            showFPSCounter = Config.Bind("Frame Rate.Toggles",
                                         "ShowFPSCounter",
                                         false,
                                         "Shows a FPS Counter of the TargetFrameRate and the PhysicsFrameRate on the left upper corner");


            targetFrameRate = Config.Bind("Frame Rate",
                                         "TargetFrameRate", 
                                         30,
                                         "The framerate that the game should target, it is adviced to be kept close to the value in PhysicsFrameRate");

            physicsFrameRate = Config.Bind("Frame Rate",
                                                "PhysicsFrameRate",
                                                60,
                                                "The framerate that the game should use for physics, this value can have a heavy performance impact, so keeping it close to 60 is adviced");
                        
            Application.targetFrameRate = targetFrameRate.Value;
            Time.fixedDeltaTime = 1f / physicsFrameRate.Value;
        }
        
        void OnGUI()
        {
            if (showFPSCounter.Value)
            {
                GUI.Box(new Rect(10, 10, 150, 10), "FPS : " + ((int)(1f / Time.deltaTime)).ToString());
                GUI.Box(new Rect(10, 20, 150, 10), "FPS (Physics): " + ((int)(1f / Time.fixedDeltaTime)).ToString());
            }
        }
    } 
}

