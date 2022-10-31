using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Core
{
    internal class WeaponDrawingHandler
    {
        private Pawn _pawn;
        private Vector3 _rootLoc;
        private Rot4 _pawnRotation;

        private List<WeaponGroupCordInfo> _filledSlots = new List<WeaponGroupCordInfo>();

        internal WeaponDrawingHandler(Pawn pawn, Vector3 rootLoc, Rot4 pawnRotation) 
        {
            _pawn = pawn;
            _rootLoc = rootLoc;
            _pawnRotation = pawnRotation;
        }

        public void DrawEquipment(bool isCarryingWeapon)
        {
            WeaponGroupCordInfo curGroup = IR_HolstersSettings.GetWeaponGroupOf(_pawn.equipment.Primary.def.defName);

            _filledSlots.Add(curGroup);

            if (!isCarryingWeapon)
            {
                DrawPrimary(curGroup);
                
            }

            if (IR_HolstersSettings.displaySide)
            {
                DrawSides();
            }
        }

        private void DrawPrimary(WeaponGroupCordInfo curGroup)
        {
            Vector3 positionOnPawn = Vector3.zero;

            positionOnPawn += IR_DisplayWeapon.GetWeaponPosition(_rootLoc, _pawnRotation, _pawn, _pawn.equipment.Primary, curGroup);
            positionOnPawn.y = 0;
            positionOnPawn += _rootLoc;
            float angle = IR_HolstersSettings.GetWeaponAngle(curGroup, _pawnRotation, false);
            bool isFrontLayer = IR_HolstersSettings.GetWeaponLayer(curGroup, _pawnRotation, false);

            IR_DisplayWeapon.DrawEquipmentHolstered(curGroup, _pawn.equipment.Primary, positionOnPawn, angle, _pawnRotation, isFrontLayer, false);
            
        }

        private void DrawSides()
        {
            foreach (Thing thing in _pawn.inventory.innerContainer)
            {
                if (thing.def.IsWeapon == false) continue;
                

                DrawSide(thing);
            }
        }

        private void DrawSide(Thing weapon)
        {
            WeaponGroupCordInfo curGroup = IR_HolstersSettings.GetWeaponGroupOf(weapon.def.defName);

            bool isSide = IsSide(curGroup);

            Vector3 positionOnPawn = IR_DisplayWeapon.GetWeaponPosition(_rootLoc, _pawnRotation, _pawn, (ThingWithComps)weapon, curGroup, isSide);
            positionOnPawn += _rootLoc;

            float weaponRotation = IR_HolstersSettings.GetWeaponAngle(curGroup, _pawnRotation, isSide);

            bool isFrontLayer = IR_HolstersSettings.GetWeaponLayer(curGroup, _pawnRotation, isSide);

            IR_DisplayWeapon.DrawEquipmentHolstered(curGroup, weapon, positionOnPawn, weaponRotation, _pawnRotation, isFrontLayer, isSide);

            _filledSlots.Add(curGroup);
        }

        private bool IsSide(WeaponGroupCordInfo curGroup)
        {
            bool isSide = true;

            if (IR_HolstersSettings.smartSideDisplay && !_filledSlots.Contains(curGroup))
            {
                isSide = false;
            }

            return isSide;
        }
    }
}

