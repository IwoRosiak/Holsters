using Holsters;
using RimWorldHolsters.Core.Defs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimWorldHolsters.Core
{
    public static class CategorySorter
    {
        public static HolsterPresetDef SortWeaponsIntoGroups(ThingDef thing)
        {
            //Log.Message(thing.defName);
            if (!thing.IsWeapon)
            {
                return null; 
            }
            //Log.Message(thing.defName + "is a weapon!");
            if (thing.defName.Equals("WoodLog") && thing.defName.Equals("Beer"))
            {
                return null;
            }

            if (thing.IsRangedWeapon)
            {
                //Log.Message(thing.defName + "is a ranged weapon!");
                if (thing.defName.StartsWith("Bow_"))
                {
                    return HolsterPresetsDefOf.Holster_BowHangedAroundPreset;
                }
                
                if (thing.defName.StartsWith("Weapon_Grenade"))
                {
                    return HolsterPresetsDefOf.Holster_HangedOnAbdomenPreset;
                }
                
                if (thing.uiIconScale > 1.1f)
                {
                    return HolsterPresetsDefOf.Holster_HolsteredOnBeltPreset;
                }

                return HolsterPresetsDefOf.Holster_HangedOnBackPreset;
            }
            if (thing.IsMeleeWeapon)
            {
                if (thing.uiIconScale > 1.1f)
                {
                    return HolsterPresetsDefOf.Holster_SheathedAtBeltPreset;
                }
                
                return HolsterPresetsDefOf.Holster_SheathedOnBackPreset;
            }

            return null;
        }
    }
}
