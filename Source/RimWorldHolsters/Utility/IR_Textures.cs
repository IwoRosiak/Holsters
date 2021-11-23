using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    [StaticConstructorOnStartup]
    internal static class IR_Textures
    {
        public static readonly Texture2D sword = ContentFinder<Texture2D>.Get("Weapons/LongSword", true);
        public static readonly Texture2D rifle = ContentFinder<Texture2D>.Get("Weapons/Rifle", true);
        public static readonly Texture2D bow = ContentFinder<Texture2D>.Get("Weapons/Bow", true);
        public static readonly Texture2D grenades = ContentFinder<Texture2D>.Get("Weapons/Grenades", true);
        public static readonly Texture2D revolver = ContentFinder<Texture2D>.Get("Weapons/Revolver", true);
        public static readonly Texture2D knife = ContentFinder<Texture2D>.Get("Weapons/Knife", true);

        //public static readonly Texture2D femaleHeadEast = ContentFinder<Texture2D>.Get("BodyType/Female/Head/North", true);

        //public static readonly Texture2D femaleHeadWest = ContentFinder<Texture2D>.Get("BodyType/Female/Head/North", true);
        //public static readonly Texture2D femaleHeadSouth = ContentFinder<Texture2D>.Get("BodyType/Female/Head/North", true);
        public static Dictionary<BodyType, Dictionary<Rot4, Texture2D>> bodies = new Dictionary<BodyType, Dictionary<Rot4, Texture2D>>()
        {
            {BodyType.fat,fatBody },
            {BodyType.male, maleBody},
            {BodyType.female,femaleBody },
            {BodyType.hulk,hulkBody },
            {BodyType.thin, thinBody}
        };

        public static Dictionary<Rot4, Texture2D> femaleHead = new Dictionary<Rot4, Texture2D>()
        {
            {Rot4.North, ContentFinder<Texture2D>.Get("BodyType/FemaleHead/North", true)},
            {Rot4.South, ContentFinder<Texture2D>.Get("BodyType/FemaleHead/South", true) },
            {Rot4.West, ContentFinder<Texture2D>.Get("BodyType/FemaleHead/East", true)},
            {Rot4.East, ContentFinder<Texture2D>.Get("BodyType/FemaleHead/East", true)}
        };

        public static Dictionary<Rot4, Texture2D> maleHead = new Dictionary<Rot4, Texture2D>()
        {
            {Rot4.North, ContentFinder<Texture2D>.Get("BodyType/MaleHead/North", true)},
            {Rot4.South, ContentFinder<Texture2D>.Get("BodyType/MaleHead/South", true) },
            {Rot4.West, ContentFinder<Texture2D>.Get("BodyType/MaleHead/East", true)},
            {Rot4.East, ContentFinder<Texture2D>.Get("BodyType/MaleHead/East", true)}
        };

        public static Dictionary<Rot4, Texture2D> fatBody = new Dictionary<Rot4, Texture2D>()
        {
            {Rot4.North, ContentFinder<Texture2D>.Get("BodyType/Fat/North", true)},
            {Rot4.South, ContentFinder<Texture2D>.Get("BodyType/Fat/South", true) },
            {Rot4.West, ContentFinder<Texture2D>.Get("BodyType/Fat/East", true)},
            {Rot4.East, ContentFinder<Texture2D>.Get("BodyType/Fat/East", true)}
        };

        public static Dictionary<Rot4, Texture2D> thinBody = new Dictionary<Rot4, Texture2D>()
        {
            {Rot4.North, ContentFinder<Texture2D>.Get("BodyType/Thin/North", true)},
            {Rot4.South, ContentFinder<Texture2D>.Get("BodyType/Thin/South", true) },
            {Rot4.West, ContentFinder<Texture2D>.Get("BodyType/Thin/East", true)},
            {Rot4.East, ContentFinder<Texture2D>.Get("BodyType/Thin/East", true)}
        };

        public static Dictionary<Rot4, Texture2D> hulkBody = new Dictionary<Rot4, Texture2D>()
        {
            {Rot4.North, ContentFinder<Texture2D>.Get("BodyType/Hulk/North", true)},
            {Rot4.South, ContentFinder<Texture2D>.Get("BodyType/Hulk/South", true) },
            {Rot4.West, ContentFinder<Texture2D>.Get("BodyType/Hulk/East", true)},
            {Rot4.East, ContentFinder<Texture2D>.Get("BodyType/Hulk/East", true)}
        };

        public static Dictionary<Rot4, Texture2D> femaleBody = new Dictionary<Rot4, Texture2D>()
        {
            {Rot4.North, ContentFinder<Texture2D>.Get("BodyType/Female/North", true)},
            {Rot4.South, ContentFinder<Texture2D>.Get("BodyType/Female/South", true)},
            {Rot4.West, ContentFinder<Texture2D>.Get("BodyType/Female/East", true)},
            {Rot4.East, ContentFinder<Texture2D>.Get("BodyType/Female/East", true)}
        };

        public static Dictionary<Rot4, Texture2D> maleBody = new Dictionary<Rot4, Texture2D>()
        {
            {Rot4.North, ContentFinder<Texture2D>.Get("BodyType/Male/North", true)},
            {Rot4.South, ContentFinder<Texture2D>.Get("BodyType/Male/South", true) },
            {Rot4.West, ContentFinder<Texture2D>.Get("BodyType/Male/East", true)},
            {Rot4.East, ContentFinder<Texture2D>.Get("BodyType/Male/East", true)}
        };

        //public static readonly Texture2D femaleBodyNorth = ContentFinder<Texture2D>.Get("BodyType/Female/North", true);
        //public static readonly Texture2D femaleBodySouth = ContentFinder<Texture2D>.Get("BodyType/Female/South", true);
        //public static readonly Texture2D femaleBodyWest = ContentFinder<Texture2D>.Get("BodyType/Female/West", true);
        //public static readonly Texture2D femaleBodyEast = ContentFinder<Texture2D>.Get("BodyType/Female/East", true);
    }
}