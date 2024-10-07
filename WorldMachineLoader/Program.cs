using System;
using System.IO;
using System.Reflection;
using System.Threading;
using HarmonyLib;

namespace WorldMachineLoader
{
    /// <summary>The main entry point for WorldMachineLoader, responsible for hooking into and launching the game.</summary>
    internal class Program
    {
        /// <summary>The main entry point which hooks into and launches the game.</summary>
        public static void Main()
        {
            Console.Title = "hat_kid's World Machine Loader";
            Console.WriteLine("The World Machine Loader");

            // Set Current Working Directory to game's folder (where's this assembly located at)
            Directory.SetCurrentDirectory(Constants.GamePath);

            if (!Program.CheckForOneShotMG())
                Environment.Exit(1);

            Program.RunOneShotMG();
        }

        /// <summary>Check that the game assembly is available.</summary>
        private static bool CheckForOneShotMG()
        {
            try
            {
                _ = Type.GetType($"OneShotMG.Game1, {Constants.GameAssemblyName}", true);

                return true;
            }
            catch (BadImageFormatException ex)
            {
                Console.WriteLine($"Could not load \"{ex.FileName}.exe\"! It seems be invalid or we are running in 32-bit.");
            }
            catch (Exception ex)
            {
                if (!File.Exists(Path.Combine(Constants.GamePath, $"{Constants.GameAssemblyName}.exe")))
                {
                    Console.WriteLine("Could not find the game executable file. Please check if it's running inside game folder.");
                }
                else
                {
                    Console.WriteLine($"Exception while trying to get game assembly:\n{ex}");
                }
            }

            return false;
        }

        /// <summary>Loads game assembly to patch and launches the game.</summary>
        private static void RunOneShotMG()
        {
            Assembly gameAssembly = Assembly.LoadFrom($"{Constants.GameAssemblyName}.exe");

            // Create Harmony patcher
            //Harmony.DEBUG = true;
            Harmony harmony = new Harmony("io.github.thehatkid.oswmeloader");

            // Patches any Harmony annotations from this assembly
            harmony.PatchAll();

            // Invoke OneShotMG entry point to run the game
            Console.WriteLine("Starting OneShotMG...");
            MethodBase gameEntrypoint = gameAssembly.ManifestModule.ResolveMethod(gameAssembly.EntryPoint.MetadataToken);
            new Thread(() =>
            {
                gameEntrypoint.Invoke(null, null);
            }).Start();
        }
    }
}
