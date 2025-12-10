using HarmonyLib;
using System.Reflection;
using Verse;

namespace RimWorldHolsters.Core
{
    [StaticConstructorOnStartup]
    internal static class IR_HarmonyInitialise
    {
        private const string HARMONY_IDENTIFIER = "com.company.IwoRosiak.Holsters";

        static IR_HarmonyInitialise()
        {
            var harmony = new Harmony(HARMONY_IDENTIFIER);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}