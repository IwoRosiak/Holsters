using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Utility
{
    public class BodyTypeData
    {
        private const float CHILD_HEAD_SIZE_FACTOR = 0.75f;

        public BodyTypeData(BodyTypeDef bodyTypeDef)
        {
            DefName = bodyTypeDef.defName;
            Label = bodyTypeDef.label;
            Offset = bodyTypeDef.headOffset;

            if (bodyTypeDef.defName == "Child")
            {
                Offset *= Mathf.Sqrt(0.8f);
                Offset = new Vector2(-Offset.x, Offset.y);
            }

            if (bodyTypeDef.defName == "Child")
                HeadSizeFactor = CHILD_HEAD_SIZE_FACTOR;

            BodyTextures = new Dictionary<Rot4, Texture2D>();
            Graphic graphic = GraphicDatabase.Get<Graphic_Multi>(bodyTypeDef.bodyNakedGraphicPath);
            if (graphic == null)
            {
                Log.Error($"Could not find graphic for body type {bodyTypeDef.defName} at path {bodyTypeDef.bodyNakedGraphicPath}");
                return;
            }

            if (graphic.MatNorth == null || graphic.MatSouth == null || graphic.MatEast == null || graphic.MatWest == null)
            {
                Log.Error($"Graphic for body type {bodyTypeDef.defName} is missing one or more materials.");
                return;
            }

            BodyTextures[Rot4.North] = graphic.MatNorth.mainTexture as Texture2D;
            BodyTextures[Rot4.South] = graphic.MatSouth.mainTexture as Texture2D;
            BodyTextures[Rot4.East] = graphic.MatEast.mainTexture as Texture2D;
            BodyTextures[Rot4.West] = graphic.MatWest.mainTexture as Texture2D;

            if (BodyTextures[Rot4.North] == null || BodyTextures[Rot4.South] == null || BodyTextures[Rot4.East] == null || BodyTextures[Rot4.West] == null)
            {
                Log.Error($"One or more textures for body type {bodyTypeDef.defName} could not be loaded.");
            }

            foreach (BodyTypeDef.WoundAnchor anchor in bodyTypeDef.woundAnchors)
            {
                if (anchor.group == null)
                    continue;

                if (anchor.group.defName != "FullHead")
                    continue;

                if (anchor.rotation == null)
                    continue;

                Vector3 anchorOffset = anchor.offset;

                Vector2 bodyGraphicScale = bodyTypeDef.bodyGraphicScale;
                float num = (bodyGraphicScale.x + bodyGraphicScale.y) / 2f;
                anchorOffset *= num;

                Log.Message($"Anchor for {bodyTypeDef.defName} {anchor.group.defName} {anchor.rotation.ToString()} offset: {anchorOffset}");
                if (anchor.rotation == Rot4.West)
                {
                    var westOffset2d = new Vector2(anchorOffset.x, anchorOffset.z);
                    var eastOffset2d = new Vector2(-anchorOffset.x, anchorOffset.z);

                    HeadAnchors.SetOrAdd(Rot4.West, westOffset2d);
                    HeadAnchors.SetOrAdd(Rot4.East, eastOffset2d);
                }
                else if (anchor.rotation == Rot4.South)
                {
                    var offset2d = new Vector2(anchorOffset.x, anchorOffset.z);

                    HeadAnchors.SetOrAdd(Rot4.North, offset2d);
                    HeadAnchors.SetOrAdd(Rot4.South, offset2d);
                }
            }
        }

        public Dictionary<Rot4, Texture2D> BodyTextures { get; } = new Dictionary<Rot4, Texture2D>();
        public Vector2 Offset { get; }
        public string DefName { get; }
        public string Label { get; }
        public float HeadSizeFactor { get; } = 1f;

        public Dictionary<Rot4, Vector2> HeadAnchors { get; } = new Dictionary<Rot4, Vector2>();
    }
}
