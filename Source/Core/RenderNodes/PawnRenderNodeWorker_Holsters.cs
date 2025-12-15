using Holsters;
using RimWorld;
using System;
using UnityEngine;
using Verse;
namespace RimWorldHolsters.Core.RenderNodes
{
    public class PawnRenderNodeWorker_Holsters : PawnRenderNodeWorker
    {
        // In RimWorld code -10f and 90f are the furthest back/front layer reserved for carried things. These values ensures holstered weapons are behind/ahead everything else.
        private const float BACK_LAYER = -5f;
        public override bool CanDrawNow(PawnRenderNode node, PawnDrawParms parms)
        {
            var holsterNode = node as PawnRenderNode_Holsters;
            Pawn pawn = parms.pawn;

            if (pawn.Dead)
                return false;

            if (!Enum.TryParse<BodyType>(pawn.story?.bodyType?.defName.ToLower(), out _))
                return false;

            if (parms.Portrait)
                return true;

            if (holsterNode.TreatAsMainWeapon && pawn.Drafted)
                return false;

            if (!IR_HolstersSettings.displayIndoors && pawn.GetRoom()?.ProperRoom == true)
                return false;

            if (pawn.GetPosture() != PawnPosture.Standing)
                return false;

            if (pawn.Swimming)
                return false;

            return true;
        }

        public override float LayerFor(PawnRenderNode node, PawnDrawParms parms)
        {
            var holsterNode = node as PawnRenderNode_Holsters;

            bool isFront = holsterNode.GetRenderData(parms.facing).IsAtFront;

            if (isFront)
                return base.LayerFor(node, parms);

            return BACK_LAYER;
        }

        public override Vector3 OffsetFor(PawnRenderNode node, PawnDrawParms parms, out Vector3 pivot)
        {
            var holsterNode = node as PawnRenderNode_Holsters;

            Vector3 pos = holsterNode.GetRenderData(parms.facing).Position; // TODO: Need to include body modifs!
            Vector3 vector = base.OffsetFor(node, parms, out pivot);
            vector += pos;

            return vector;
        }

        public override Quaternion RotationFor(PawnRenderNode node, PawnDrawParms parms)
        {
            var holsterNode = node as PawnRenderNode_Holsters;

            HolsterConfiguration renderData = holsterNode.GetRenderData(parms.facing);

            float rotation = renderData.Rotation; 

            if (renderData.IsFlipped)
                rotation += 180;

            rotation %= 360;
            var quaternion = Quaternion.AngleAxis(rotation, Vector3.up);

            return quaternion;
        }

        public override Vector3 ScaleFor(PawnRenderNode node, PawnDrawParms parms)
        {
            var holsterNode = node as PawnRenderNode_Holsters;

            var size = holsterNode.GetRenderData(parms.facing).Size;
            float UIIconScale = holsterNode.Thing.def.uiIconScale; // TODO: settings to toggle this!
            Vector3 scale = Vector3.one / UIIconScale * size;

            return scale;
        }
    }
}
