using Holsters;
using RimWorldHolsters.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    public class IR_HolstersSettings : ModSettings
    {
        public IR_HolstersSettings()
        {
        }

        private const float forwardPos = 0.026f;
        private const float middlePos = 0.0128957527f;
        private const float backPos = -0.0128957527f;

        public static bool isFirstLaunch;
        public static bool displaySide;

        public static float backLayerOffset = 0;
        public static float frontLayerOffset = 0;

        public static bool displayIndoors = true;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref displaySide, "displaySide", true);
            Scribe_Values.Look(ref isFirstLaunch, "isFirstLaunch7", true);

            Scribe_Values.Look(ref backLayerOffset, "backLayerOffset", 0);
            Scribe_Values.Look(ref frontLayerOffset, "frontLayerOffset", 0);
            Scribe_Values.Look(ref displayIndoors, "displayIndoors", true);

            base.ExposeData();
        }


        private static HolsterPresetDef GetConfiguration(ThingDef def)
        {
            //foreach (HolsterPresetDef def in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(HolsterPresetDef)))
            //{
            //    return def;
            //}

            return CategorySorter.SortWeaponsIntoGroups(def);
        }

        public static HolsterConfiguration GetHolsterConfigurationFor(ThingDef def, Rot4 rot)
        {
            return GetConfiguration(def).Configuration[rot];
        }

        public static float FrontPos
        {
            get { return forwardPos + frontLayerOffset; }
        }

        public static float BackPos
        {
            get { return backPos + backLayerOffset; }
        }
    }
}