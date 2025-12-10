using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Core.RenderNodes
{
    internal class PawnRenderNodeWorker_Holsters : PawnRenderNodeWorker
    {
        public override bool CanDrawNow(PawnRenderNode node, PawnDrawParms parms)
        {
            Pawn pawn = parms.pawn;

            if (pawn.Dead)
                return false;

            if (!IR_HolstersSettings.displayIndoors && pawn.GetRoom()?.ProperRoom == true)
                return false;

            if (pawn.GetPosture() != PawnPosture.Standing)
                return false;

            if (pawn.equipment?.Primary == null)
                return false;

            if (!Enum.TryParse<BodyType>(pawn.story?.bodyType?.defName.ToLower(), out _))
                return false;

            if (pawn.Swimming)
                return false;

            return true;
        }

        public override float LayerFor(PawnRenderNode node, PawnDrawParms parms)
        {
            Log.Message("Layer: " + base.LayerFor(node, parms).ToString());

            return base.LayerFor(node, parms);
        }

        public override Vector3 OffsetFor(PawnRenderNode node, PawnDrawParms parms, out Vector3 pivot)
        {
            Vector3 vector = base.OffsetFor(node, parms, out pivot);
            Log.Message("Offset: " + vector.ToString());
            return vector;
        }
        public override Quaternion RotationFor(PawnRenderNode node, PawnDrawParms parms)
        {
            Quaternion quaternion = base.RotationFor(node, parms);
            Log.Message("Rotation: " + quaternion.ToString());

            return quaternion;
        }

        public override Vector3 ScaleFor(PawnRenderNode node, PawnDrawParms parms)
        {
            Vector3 vector = base.ScaleFor(node, parms);
            Log.Message("Scale: " + vector.ToString());
            return vector;
        }
    }
}
