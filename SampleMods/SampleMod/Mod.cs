using HarmonyLib;
using OneShotMG.src;

namespace SampleMod
{
    internal class Mod
    {
    }

    [HarmonyPatch(typeof(LanguageManager))]
    class WME_LanguageManagerPatch
    {
        /// <summary>Replaces any localized strings with our "Shoutouts to Simpleflips" one.</summary>
        [HarmonyPrefix]
        [HarmonyPatch("GetLocString")]
        static bool GetLocString_PrefixPatch(ref string __result)
        {
            __result = "Shoutouts to Simpleflips.";
            return false;
        }
    }
}
