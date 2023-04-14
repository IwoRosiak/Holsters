using UnityEngine;
using Verse;

namespace Holsters.Drawing
{
    public struct DrawProperties
    {

        private readonly ThingWithComps _item;
        private Vector3 _location;
        private readonly float _rotation;
        private readonly Mesh _mesh;

        private readonly float _size;

        public DrawProperties(ThingWithComps item, Vector3 drawLoc, Rot4 pawnRotation)
        {
            _item = item;
            _mesh = MeshPool.plane10; //that can be changed


            HolsterConfiguration configuration = IR_HolstersSettings.GetHolsterConfigurationFor(item.def, pawnRotation);

            _location = drawLoc + configuration.Position / 10;
            _rotation = configuration.Rotation;
            _size = configuration.Size;

            if (configuration.IsFlipped)
            {
                _rotation += 180;
                _mesh = MeshPool.plane10Flip;
            }
            _rotation %= 360f;

            if (configuration.IsAtFront)
            {
                _location.y += IR_HolstersSettings.FrontPos;

            }
            else
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
