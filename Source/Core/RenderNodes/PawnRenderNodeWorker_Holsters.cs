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
            //Log.Message("Layer: " + base.LayerFor(node, parms).ToString());

            return base.LayerFor(node, parms);
        }

        public override Vector3 OffsetFor(PawnRenderNode node, PawnDrawParms parms, out Vector3 pivot)
        {
            ThingWithComps weapon = parms.pawn.equipment.Primary;
            WeaponGroupCordInfo curGroup = IR_HolstersSettings.GetWeaponGroupOf(weapon.def.defName);
            var pos = IR_HolstersSettings.GetWeaponPos(weapon.def.defName, parms.pawn.Rotation, false, parms.pawn, curGroup);
            Vector3 vector = base.OffsetFor(node, parms, out pivot);
            vector += pos;
            //Log.Message($"Offset: {vector.ToString()} Original offset: {(vector-pos).ToString()}" );
            return vector;
        }
        public override Quaternion RotationFor(PawnRenderNode node, PawnDrawParms parms)
        {
            ThingWithComps weapon = parms.pawn.equipment.Primary;
            WeaponGroupCordInfo curGroup = IR_HolstersSettings.GetWeaponGroupOf(weapon.def.defName);
            var rotation = IR_HolstersSettings.GetWeaponAngle(weapon.def.defName, parms.pawn.Rotation, false);
            if (IR_HolstersSettings.GetWeaponFlip(curGroup, parms.pawn.Rotation, false))
                rotation += 180;

            rotation %= 360;
            Quaternion quaternion = Quaternion.AngleAxis(rotation, Vector3.up);
            //Log.Message($"Quaternion: {quaternion.ToString()} Euler: {quaternion.eulerAngles.ToString()}");

            return quaternion;
        }

        public override Vector3 ScaleFor(PawnRenderNode node, PawnDrawParms parms)
        {
            ThingWithComps weapon = parms.pawn.equipment.Primary;
            WeaponGroupCordInfo curGroup = IR_HolstersSettings.GetWeaponGroupOf(weapon.def.defName);
            var size = curGroup.GetSize(parms.pawn.Rotation);
            float UIIconScale = weapon.def.uiIconScale;
            Vector3 scale = Vector3.one / UIIconScale * size;

            //Log.Message("Scale: " + scale.ToString());
            return scale;
        }

        protected override Graphic GetGraphic(PawnRenderNode node, PawnDrawParms parms) => base.GetGraphic(node, parms);
        protected override GraphicStateDef GetGraphicState(PawnRenderNode node, PawnDrawParms parms) => base.GetGraphicState(node, parms);
        protected override Vector3 PivotFor(PawnRenderNode node, PawnDrawParms parms) => base.PivotFor(node, parms);
    }
}
