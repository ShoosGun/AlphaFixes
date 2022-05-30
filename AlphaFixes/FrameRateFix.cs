using UnityEngine;
using BepInEx.Configuration;

namespace AlphaFixes
{
    public class FrameRateFix : GameFix
    {
        private ConfigEntry<int> targetFrameRate;
        private ConfigEntry<int> physicsFrameRate;
        private ConfigEntry<bool> showFPSCounter;
        public void Awake()
        {
            FixName = "Frame Rate";

            showFPSCounter = AlphaFixesMod.Instance.Config.Bind(FixName + ".Toggles",
                                         "ShowFPSCounter",
                                         false,
                                         "Shows a FPS Counter of the TargetFrameRate and the PhysicsFrameRate on the left upper corner");


            targetFrameRate = AlphaFixesMod.Instance.Config.Bind(FixName,
                                         "TargetFrameRate",
                                         30,
                                         "The framerate that the game should target, it is adviced to be kept close to the value in PhysicsFrameRate");

            physicsFrameRate = AlphaFixesMod.Instance.Config.Bind(FixName,
                                                "PhysicsFrameRate",
                                                60,
                                                "The framerate that the game should use for physics, this value can have a heavy performance impact, so keeping it close to 60 is adviced");
        }
        public override void Innit()
        {
            if (!IsFixEnabled.Value)
                return;

            Application.targetFrameRate = targetFrameRate.Value;
            Time.fixedDeltaTime = 1f / physicsFrameRate.Value;
        }
        void OnGUI()
        {
            if (showFPSCounter.Value && IsFixEnabled.Value)
            {
                GUI.Box(new Rect(10, 10, 150, 10), "FPS : " + ((int)(1f / Time.deltaTime)).ToString());
                GUI.Box(new Rect(10, 20, 150, 10), "FPS (Physics): " + ((int)(1f / Time.fixedDeltaTime)).ToString());
            }
        }
    }
}
