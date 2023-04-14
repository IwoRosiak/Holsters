using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Holsters.Drawing
{
    internal class DrawRequestProcessor
    {
        private Pawn _pawn;
        private Vector3 _rootLoc;
        private Rot4 _pawnRotation;

        // private List<WeaponGroupCordInfo> _filledSlots = new List<WeaponGroupCordInfo>();

        internal DrawRequestProcessor(Pawn pawn, Vector3 rootLoc, Rot4 pawnRotation)
        {
            _pawn = pawn;
            _rootLoc = rootLoc;
            _pawnRotation = pawnRotation;
        }

        public void DrawEquipment(bool isCarryingWeapon)
        {

            if (!isCarryingWeapon)
            {
                DrawWeapon(_pawn.equipment.Primary);
            }
            else
            {
                //WeaponGroupCordInfo curGroup = IR_HolstersSettings.GetWeaponGroupOf(_pawn.equipment.Primary.def.defName);
                //_filledSlots.Add(curGroup);

            }

            if (true)//IR_HolstersSettings.displaySide)
            {
                foreach (Thing thing in _pawn.inventory.innerContainer)
                {
                    if (thing.def.IsWeapon == false) continue;

                    DrawWeapon((ThingWithComps)thing);
                }
            }
        }

        private void DrawWeapon(ThingWithComps weapon)
        {

            DrawProperties drawingProperties = new DrawProperties(weapon, _rootLoc, _pawnRotation);

            EquipmentRenderer.DrawEquipmentHolstered(drawingProperties);

            //_filledSlots.Add(curGroup); //Holster points!
        }
    }
}

