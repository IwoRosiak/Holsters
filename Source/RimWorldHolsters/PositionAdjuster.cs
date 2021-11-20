using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    [StaticConstructorOnStartup]
    public static class IR_PositionAdjuster
    {
        public static Vector3 GetBodyOffsetLargeWeapons(Pawn pawn, Rot4 pawnRotation)
        {
            Vector3 offset = Vector3.zero;
            float modif = 0;

            if (pawn.story.bodyType.defName == "Fat")
            {
                modif = 1f;
            }

            if (pawn.story.bodyType.defName == "Hulk")
            {
                modif = 0.5f;
            }

            if (pawn.story.bodyType.defName == "Thin")
            {
                modif = -0.5f;
            }

            if (pawnRotation == Rot4.East)
            {
                offset = new Vector3(-0.2f, 0f, 0f);
            }
            if (pawnRotation == Rot4.West)
            {
                offset = new Vector3(0.2f, 0f, 0f);
            }

            return offset * modif;
        }

        public static Vector3 GetBodyOffsetSmallWeapons(Pawn pawn, Rot4 pawnRotation)
        {
            Vector3 offset = Vector3.zero;
            float modif = 0;

            if (pawn.story.bodyType.defName == "Fat")
            {
                modif = 1f;
            }

            if (pawn.story.bodyType.defName == "Hulk")
            {
                modif = 0.5f;
            }

            if (pawn.story.bodyType.defName == "Thin")
            {
                modif = -0.2f;
            }

            if (pawnRotation == Rot4.North)
            {
                offset = new Vector3(-0.2f, 0f, 0f);
            }
            if (pawnRotation == Rot4.South)
            {
                offset = new Vector3(0.2f, 0f, 0f);
            }

            return offset * modif;
        }
    }
}