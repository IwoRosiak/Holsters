using UnityEngine;
using Verse;

namespace Holsters
{
    public class HolsterConfiguration : IExposable
    {
        private Vector3 _position;

        public float Rotation;
        public float Size;
        public bool IsFlipped;
        public bool IsAtFront;
        public Vector3 BodyOffset;

        public HolsterConfiguration() { }

        public HolsterConfiguration(Vector3 position, float rotation, float size, bool isFlipped, bool isAtFront, Vector3 bodyOffset)
        {
            _position = position;
            Rotation = rotation;
            Size = size;
            IsFlipped = isFlipped;
            IsAtFront = isAtFront;
            BodyOffset = bodyOffset;
        }

        public static HolsterConfiguration EmptyConfiguration => new HolsterConfiguration
        {
            Position = Vector3.zero,
            Rotation = 0,
            Size = 1,
            IsFlipped = false,
            IsAtFront = true,
            BodyOffset = Vector3.zero
        };

        public Vector3 Position
        {
            get => _position / 10f;
            set => _position = value * 10f;
        }


        public void ExposeData()
        {
            Scribe_Values.Look(ref _position, "position");
            Scribe_Values.Look(ref Rotation, "rotation");
            Scribe_Values.Look(ref Size, "size");
            Scribe_Values.Look(ref IsFlipped, "isFlipped");
            Scribe_Values.Look(ref IsAtFront, "isAtFront");
            Scribe_Values.Look(ref BodyOffset, "bodyOffset");

        }
        public HolsterConfiguration Copy()
        {
            return new HolsterConfiguration
            {
                Position = Position,
                Rotation = Rotation,
                Size = Size,
                IsFlipped = IsFlipped,
                IsAtFront = IsAtFront,
                BodyOffset = BodyOffset
            };
        }
    }
}
