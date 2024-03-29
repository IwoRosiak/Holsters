﻿using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Holsters.Settings
{
    [StaticConstructorOnStartup]
    internal static class IR_Textures
    {
        public static Texture2D background = ContentFinder<Texture2D>.Get("Background", true);
        public static Texture2D backgroundPawn = ContentFinder<Texture2D>.Get("BackgroundPawn", true);

        public static Dictionary<Rot4, Texture2D> FemaleHead = new Dictionary<Rot4, Texture2D>()
        {
            {Rot4.North, ContentFinder<Texture2D>.Get("BodyType/FemaleHead/North", true)},
            {Rot4.South, ContentFinder<Texture2D>.Get("BodyType/FemaleHead/South", true) },
            {Rot4.West, ContentFinder<Texture2D>.Get("BodyType/FemaleHead/West", true)},
            {Rot4.East, ContentFinder<Texture2D>.Get("BodyType/FemaleHead/East", true)}
        };

        public static Dictionary<Rot4, Texture2D> MaleHead = new Dictionary<Rot4, Texture2D>()
        {
            {Rot4.North, ContentFinder<Texture2D>.Get("BodyType/MaleHead/North", true)},
            {Rot4.South, ContentFinder<Texture2D>.Get("BodyType/MaleHead/South", true) },
            {Rot4.West, ContentFinder<Texture2D>.Get("BodyType/MaleHead/West", true)},
            {Rot4.East, ContentFinder<Texture2D>.Get("BodyType/MaleHead/East", true)}
        };

        public static Dictionary<Rot4, Texture2D> FatBody = new Dictionary<Rot4, Texture2D>()
        {
            {Rot4.North, ContentFinder<Texture2D>.Get("BodyType/Fat/North", true)},
            {Rot4.South, ContentFinder<Texture2D>.Get("BodyType/Fat/South", true) },
            {Rot4.West, ContentFinder<Texture2D>.Get("BodyType/Fat/West", true)},
            {Rot4.East, ContentFinder<Texture2D>.Get("BodyType/Fat/East", true)}
        };

        public static Dictionary<Rot4, Texture2D> ThinBody = new Dictionary<Rot4, Texture2D>()
        {
            {Rot4.North, ContentFinder<Texture2D>.Get("BodyType/Thin/North", true)},
            {Rot4.South, ContentFinder<Texture2D>.Get("BodyType/Thin/South", true) },
            {Rot4.West, ContentFinder<Texture2D>.Get("BodyType/Thin/West", true)},
            {Rot4.East, ContentFinder<Texture2D>.Get("BodyType/Thin/East", true)}
        };

        public static Dictionary<Rot4, Texture2D> HulkBody = new Dictionary<Rot4, Texture2D>()
        {
            {Rot4.North, ContentFinder<Texture2D>.Get("BodyType/Hulk/North", true)},
            {Rot4.South, ContentFinder<Texture2D>.Get("BodyType/Hulk/South", true) },
            {Rot4.West, ContentFinder<Texture2D>.Get("BodyType/Hulk/West", true)},
            {Rot4.East, ContentFinder<Texture2D>.Get("BodyType/Hulk/East", true)}
        };

        public static Dictionary<Rot4, Texture2D> FemaleBody = new Dictionary<Rot4, Texture2D>()
        {
            {Rot4.North, ContentFinder<Texture2D>.Get("BodyType/Female/North", true)},
            {Rot4.South, ContentFinder<Texture2D>.Get("BodyType/Female/South", true)},
            {Rot4.West, ContentFinder<Texture2D>.Get("BodyType/Female/West", true)},
            {Rot4.East, ContentFinder<Texture2D>.Get("BodyType/Female/East", true)}
        };

        public static Dictionary<Rot4, Texture2D> MaleBody = new Dictionary<Rot4, Texture2D>()
        {
            {Rot4.North, ContentFinder<Texture2D>.Get("BodyType/Male/North", true)},
            {Rot4.South, ContentFinder<Texture2D>.Get("BodyType/Male/South", true) },
            {Rot4.West, ContentFinder<Texture2D>.Get("BodyType/Male/West", true)},
            {Rot4.East, ContentFinder<Texture2D>.Get("BodyType/Male/East", true)}
        };

        public static Dictionary<BodyType, Dictionary<Rot4, Texture2D>> Bodies = new Dictionary<BodyType, Dictionary<Rot4, Texture2D>>()
        {
            {BodyType.fat,FatBody},
            {BodyType.male, MaleBody},
            {BodyType.female,FemaleBody},
            {BodyType.hulk, HulkBody},
            {BodyType.thin, ThinBody}
        };
    }
}