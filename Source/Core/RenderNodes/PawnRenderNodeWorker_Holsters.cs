using RimWorld;
using System;
using UnityEngine;
using Verse;
namespace RimWorldHolsters.Core.RenderNodes
{
    internal class PawnRenderNodeWorker_Holsters : PawnRenderNodeWorker
    {
        // In RimWorld code -10f and 90f are the furthest back/front layer reserved for carried things. These values ensuresholstered weapons are behind/ahead everything else.
        private const float BACK_LAYER = -5f;
        public override bool CanDrawNow(PawnRenderNode node, PawnDrawParms parms)
        {
            Pawn pawn = parms.pawn;

            if (pawn.Dead)
                return false;

            if (!Enum.TryParse<BodyType>(pawn.story?.bodyType?.defName.ToLower(), out _))
                return false;

            if (parms.Portrait)
                return true;

            //if (pawn.Drafted)
            //    return false;

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

            ThingWithComps weapon = holsterNode.Thing;
            WeaponGroupCordInfo curGroup = IR_HolstersSettings.GetWeaponGroupOf(weapon.def.defName);

            bool isFront = IR_HolstersSettings.GetWeaponLayer(curGroup, parms.facing, false);

            if (isFront)
                return base.LayerFor(node, parms);

            return BACK_LAYER;
        }

        public override Vector3 OffsetFor(PawnRenderNode node, PawnDrawParms parms, out Vector3 pivot)
        {
            var holsterNode = node as PawnRenderNode_Holsters;

            WeaponGroupCordInfo curGroup = IR_HolstersSettings.GetWeaponGroupOf(holsterNode.Thing.def.defName);
            Vector3 pos = IR_HolstersSettings.GetWeaponPos(holsterNode.Thing.def.defName, parms.facing, false, parms.pawn, curGroup);
            Vector3 vector = base.OffsetFor(node, parms, out pivot);
            vector += pos;

            return vector;
        }

        public override Quaternion RotationFor(PawnRenderNode node, PawnDrawParms parms)
        {
            var holsterNode = node as PawnRenderNode_Holsters;

            ThingWithComps weapon = holsterNode.Thing;
            float rotation = IR_HolstersSettings.GetWeaponAngle(weapon.def.defName, parms.facing, false);

            if (IR_HolstersSettings.GetWeaponFlip(holsterNode.WeaponGroupCordInfo, parms.facing, false))
                rotation += 180;

            rotation %= 360;
            var quaternion = Quaternion.AngleAxis(rotation, Vector3.up);

            return quaternion;
        }

        public override Vector3 ScaleFor(PawnRenderNode node, PawnDrawParms parms)
        {
            var holsterNode = node as PawnRenderNode_Holsters;

            ThingWithComps weapon = holsterNode.Thing;
            var size = holsterNode.WeaponGroupCordInfo.GetSize(parms.facing);
            float UIIconScale = weapon.def.uiIconScale;
            Vector3 scale = Vector3.one / UIIconScale * size;

            return scale;
        }
    }
}
