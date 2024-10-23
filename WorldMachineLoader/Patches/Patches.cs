using HarmonyLib;
using OneShotMG;
using OneShotMG.src;

namespace WorldMachineLoader.Patches
{
    [HarmonyPatch(typeof(Game1))]
    class Game1Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch("Initialize")]
        static void Initialize_PrefixPatch()
        {
            Game1.logMan.Log(LogManager.LogLevel.Info, "Hello from World Machine Loader!");
            Game1.VersionString = $"{Game1.VersionString} + WorldMachineLoader";
        }
    }
}
