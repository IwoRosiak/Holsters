using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Utility
{
    public class BodyTypeData
    {
        public BodyTypeData(BodyTypeDef bodyTypeDef)
        {
            DefName = bodyTypeDef.defName;
            Label = bodyTypeDef.label;
            Offset = bodyTypeDef.headOffset;

            if (bodyTypeDef.defName == "Child")
                HeadSizeFactor = 0.75f;

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
        }

        public Dictionary<Rot4, Texture2D> BodyTextures { get; } = new Dictionary<Rot4, Texture2D>();
        public Vector2 Offset { get; }
        public string DefName { get; }
        public string Label { get; }
        public float HeadSizeFactor { get; } = 1f;
    }
}
