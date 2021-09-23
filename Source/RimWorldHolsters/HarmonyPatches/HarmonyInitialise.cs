using HarmonyLib;
using System.Reflection;
using Verse;

namespace RimWorldHolsters.HarmonyPatches
{
    [StaticConstructorOnStartup]
    internal static class IR_HarmonyInitialise
    {
        static IR_HarmonyInitialise()
        {
            var harmony = new Harmony("com.company.IwoRosiak.Holsters");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}