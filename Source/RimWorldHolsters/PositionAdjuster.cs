using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    [StaticConstructorOnStartup]
    public static class IR_PositionAdjuster
    {
        public static BodyType GetBodyType(Pawn pawn)
        {
            if (pawn.story?.bodyType?.defName == "Fat")
            {
                return BodyType.fat;
            }

            if (pawn.story?.bodyType?.defName == "Hulk")
            {
                return BodyType.hulk;
            }

            if (pawn.story?.bodyType?.defName == "Thin")
            {
                return BodyType.thin;
            }
            return BodyType.male;
        }

        public static Vector3 GetBodyOffsetLargeWeapons(Rot4 pawnRotation, Pawn pawn = null, BodyType bodyType = BodyType.male)
        {
            BodyType body;

            if (pawn != null)
            {
                body = GetBodyType(pawn);
            }
            else
            {
                body = bodyType;
            }

            Vector3 offset = Vector3.zero;
            float modif = 0;

            switch (body)
            {
                case BodyType.hulk:
                    modif = 0.5f;
                    break;

                case BodyType.fat:
                    modif = 1f;
                    break;

                case BodyType.thin:
                    modif = -0.2f;
                    break;

                default:

                    break;
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

        public static Vector3 GetBodyOffsetSmallWeapons(Rot4 pawnRotation, Pawn pawn = null, BodyType bodyType = BodyType.male)
        {
            BodyType body;

            if (pawn != null)
            {
                body = GetBodyType(pawn);
            }
            else
            {
                body = bodyType;
            }

            Vector3 offset = Vector3.zero;
            float modif = 0;

            switch (body)
            {
                case BodyType.hulk:
                    modif = 0.5f;
                    break;

                case BodyType.fat:
                    modif = 1f;
                    break;

                case BodyType.thin:
                    modif = -0.2f;
                    break;

                case BodyType.male:
                    break;

                case BodyType.female:
                    break;
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