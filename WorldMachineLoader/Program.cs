using System;
using System.Threading;
using System.IO;
using System.Reflection;
using HarmonyLib;

namespace WorldMachineLoader
{
    internal class Program
    {
        public static void Main()
        {
            Console.Title = "hat_kid's World Machine Loader";

            if (!Program.CheckForOneShotMG())
                Environment.Exit(1);

            Program.RunOneShotMG();
        }

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
                    Console.WriteLine($"Exception while trying to get game assembly: {ex}");
                }
            }

            return false;
        }

        private static void RunOneShotMG()
        {
            Assembly gameAssembly = Assembly.LoadFrom($"{Constants.GameAssemblyName}.exe");

            //Harmony.DEBUG = true;
            Harmony harmony = new Harmony("io.github.thehatkid.oswmeloader");

            harmony.PatchAll(gameAssembly);

            // Invoke OneShotMG entrypoint to run the game
            MethodBase gameEntrypoint = gameAssembly.ManifestModule.ResolveMethod(gameAssembly.EntryPoint.MetadataToken);
            new Thread(() =>
            {
                gameEntrypoint.Invoke(null, null);
            }).Start();
        }
    }
}
