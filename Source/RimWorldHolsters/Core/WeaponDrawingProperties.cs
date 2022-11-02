using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Core
{
    public struct WeaponDrawingProperties
    {

        private ThingWithComps _item;
        private Vector3 _location;
        private float _rotation;
        private Mesh _mesh;

        private float _size;

        public WeaponDrawingProperties(ThingWithComps item, WeaponGroupCordInfo groupSettings, Vector3 drawLoc,Rot4 pawnRotation, bool isSide)
        {
            _item = item;
            _location = drawLoc;

            _rotation = IR_HolstersSettings.GetWeaponAngle(groupSettings, pawnRotation, isSide);
            _mesh = MeshPool.plane10;

            _size = groupSettings.GetSize(pawnRotation);

            if (IR_HolstersSettings.GetWeaponFlip(groupSettings, pawnRotation, isSide))
            {
                _rotation += 180;
                _mesh = MeshPool.plane10Flip;
            }
            _rotation %= 360f;

            if (IR_HolstersSettings.GetWeaponLayer(groupSettings, pawnRotation, isSide))
            {
                _location.y += IR_HolstersSettings.FrontPos;

            } else
            {
                _location.y += IR_HolstersSettings.BackPos;
            }

        }

        public ThingWithComps Item => _item;
        public Vector3 Location => _location;
        public float Rotation => _rotation;
        public float Size => _size;
        public Mesh Mesh => _mesh;
    }
}
