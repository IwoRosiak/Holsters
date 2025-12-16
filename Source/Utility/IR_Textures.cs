using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    [StaticConstructorOnStartup]
    internal static class IR_Textures
    {
        public static Texture2D background = ContentFinder<Texture2D>.Get("Background", true);
        public static Texture2D backgroundPawn = ContentFinder<Texture2D>.Get("BackgroundPawn", true);
    }
}