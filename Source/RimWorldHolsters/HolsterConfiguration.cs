using UnityEngine;
using Verse;

namespace Holsters
{
    public sealed class HolsterConfiguration : IExposable
    {
        public Vector3 Position;
        public float Rotation;
        public float Size;
        public bool IsFlipped;
        public bool IsAtFront;
        public Vector3 BodyOffset;

        public void ExposeData()
        {
            Scribe_Values.Look(ref Position, "position");
            Scribe_Values.Look(ref Rotation, "rotation");
            Scribe_Values.Look(ref Size, "size");
            Scribe_Values.Look(ref IsFlipped, "isFlipped");
            Scribe_Values.Look(ref IsAtFront, "isAtFront");
            Scribe_Values.Look(ref BodyOffset, "bodyOffset");

        }
    }
}
