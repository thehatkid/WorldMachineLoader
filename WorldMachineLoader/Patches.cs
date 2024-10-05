using HarmonyLib;

namespace WorldMachineLoader
{
    [HarmonyPatch(typeof(OneShotMG.Game1))]
    class WME_Game1Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch("Initialize")]
        static void InitializePatch(OneShotMG.Game1 __instance)
        {
            OneShotMG.Game1.logMan.Log(OneShotMG.src.LogManager.LogLevel.Info, "Hello from World Machine Loader!");
            OneShotMG.Game1.VersionString = $"{OneShotMG.Game1.VersionString} + WorldMachineLoader";
        }
    }

    /*
    [HarmonyPatch(typeof(OneShotMG.src.LanguageManager))]
    class WME_LanguageManagerPatch
    {
        /// <summary>
        /// Replaces any localized strings with our "Shoutouts to Simpleflips" one.
        /// </summary>
        [HarmonyPrefix]
        [HarmonyPatch("GetLocString")]
        static bool GetLocStringPatch(OneShotMG.src.LanguageManager __instance, ref string __result)
        {
            __result = "Shoutouts to Simpleflips.";
            return false;
        }
    }
    */
}
