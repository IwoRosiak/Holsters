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

            if (pawn.equipment?.Primary == null)
                return false;

            if (!Enum.TryParse<BodyType>(pawn.story?.bodyType?.defName.ToLower(), out _))
                return false;

            if (parms.Portrait)
                return true;

            if (pawn.Drafted)
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
            Log.Message("Layer: " + base.LayerFor(node, parms).ToString());

            ThingWithComps weapon = parms.pawn.equipment.Primary;
            WeaponGroupCordInfo curGroup = IR_HolstersSettings.GetWeaponGroupOf(weapon.def.defName);

            bool isFront = IR_HolstersSettings.GetWeaponLayer(curGroup, parms.facing, false);

            if (isFront)
                return base.LayerFor(node, parms);

            return -5f;
        }

        public override Vector3 OffsetFor(PawnRenderNode node, PawnDrawParms parms, out Vector3 pivot)
        {
            ThingWithComps weapon = parms.pawn.equipment.Primary;
            WeaponGroupCordInfo curGroup = IR_HolstersSettings.GetWeaponGroupOf(weapon.def.defName);
            var pos = IR_HolstersSettings.GetWeaponPos(weapon.def.defName, parms.facing, false, parms.pawn, curGroup);
            Vector3 vector = base.OffsetFor(node, parms, out pivot);
            vector += pos;
            //Log.Message($"Offset: {vector.ToString()} Original offset: {(vector-pos).ToString()}" );
            return vector;
        }
        public override Quaternion RotationFor(PawnRenderNode node, PawnDrawParms parms)
        {
            ThingWithComps weapon = parms.pawn.equipment.Primary;
            WeaponGroupCordInfo curGroup = IR_HolstersSettings.GetWeaponGroupOf(weapon.def.defName);
            var rotation = IR_HolstersSettings.GetWeaponAngle(weapon.def.defName, parms.facing, false);

            if (IR_HolstersSettings.GetWeaponFlip(curGroup, parms.facing, false))
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
            var size = curGroup.GetSize(parms.facing);
            float UIIconScale = weapon.def.uiIconScale;
            Vector3 scale = Vector3.one / UIIconScale * size;

            //Log.Message("Scale: " + scale.ToString());
            return scale;
        }
    }
}
