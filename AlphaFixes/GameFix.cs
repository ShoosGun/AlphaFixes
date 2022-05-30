using BepInEx.Configuration;
using UnityEngine;

namespace AlphaFixes
{
    public class GameFix : MonoBehaviour
    {
        public string FixName { get; protected set; }
        public ConfigEntry<bool> IsFixEnabled;

        public virtual void Innit() { }
    }
}
