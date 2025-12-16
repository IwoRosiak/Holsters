using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Utility
{
    internal class HeadTypeData
    {
        public HeadTypeData(HeadTypeDef headTypeDef)
        {
            DefName = headTypeDef.defName;

            HeadTextures = new Dictionary<Rot4, Texture2D>();
            Graphic graphic = GraphicDatabase.Get<Graphic_Multi>(headTypeDef.graphicPath);
            if (graphic == null)
            {
                Log.Error($"Could not find graphic for body type {headTypeDef.defName} at path {headTypeDef.graphicPath}");
                return;
            }

            if (graphic.MatNorth == null || graphic.MatSouth == null || graphic.MatEast == null || graphic.MatWest == null)
            {
                Log.Error($"Graphic for body type {headTypeDef.defName} is missing one or more materials.");
                return;
            }

            HeadTextures[Rot4.North] = graphic.MatNorth.mainTexture as Texture2D;
            HeadTextures[Rot4.South] = graphic.MatSouth.mainTexture as Texture2D;
            HeadTextures[Rot4.East] = graphic.MatEast.mainTexture as Texture2D;
            HeadTextures[Rot4.West] = graphic.MatWest.mainTexture as Texture2D;

            if (HeadTextures[Rot4.North] == null || HeadTextures[Rot4.South] == null || HeadTextures[Rot4.East] == null || HeadTextures[Rot4.West] == null)
            {
                Log.Error($"One or more textures for body type {headTypeDef.defName} could not be loaded.");
            }
        }

        public Dictionary<Rot4, Texture2D> HeadTextures = new Dictionary<Rot4, Texture2D>();

        public string DefName { get; }
    }
}
