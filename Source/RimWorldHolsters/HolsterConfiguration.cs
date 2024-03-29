﻿using UnityEngine;
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

        public static HolsterConfiguration EmptyConfiguration => new HolsterConfiguration
        {
            Position = Vector3.zero,
            Rotation = 0,
            Size = 1,
            IsFlipped = false,
            IsAtFront = true,
            BodyOffset = Vector3.zero
        };
    }
}
