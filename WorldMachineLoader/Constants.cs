using System.IO;
using System.Reflection;

namespace WorldMachineLoader
{
    internal static class Constants
    {
        /// <summary>The path to the game folder.</summary>
        public static string GamePath { get; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>The game's assembly name.</summary>
        internal static string GameAssemblyName { get; } = "OneShotMG";
    }
}
