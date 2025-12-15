using Holsters;
using RimWorldHolsters.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    public class IR_HolstersSettings : ModSettings
    {
        public static bool isFirstLaunch;

        public static bool displaySide;

        public static bool smartSideDisplay;

        public static float backLayerOffset = 0;
        public static float frontLayerOffset = 0;

        public static bool displayIndoors = true;

        public static List<HolsterWeaponRenderGroup> RenderGroups;

        public IR_HolstersSettings() { }


        public override void ExposeData()
        {
            // New
            Scribe_Collections.Look(ref RenderGroups, "renderGroups", LookMode.Deep);
            
            
            // Old
            //Scribe_Collections.Look(ref groups,"groupsCordSettings4" /*changed from 3 to 4 for development for now*/, LookMode.Deep);
            Scribe_Values.Look(ref displaySide, "displaySide", true);
            Scribe_Values.Look(ref smartSideDisplay, "smartSideDisplay", true);
            Scribe_Values.Look(ref isFirstLaunch, "isFirstLaunch7", true);

            Scribe_Values.Look(ref backLayerOffset, "backLayerOffset", 0);
            Scribe_Values.Look(ref frontLayerOffset, "frontLayerOffset", 0);
            Scribe_Values.Look(ref displayIndoors, "displayIndoors", true);

            base.ExposeData();
        }

        //MANAGING GROUPS
        public static void InitBasicGroups()
        {
            if (ShouldInitializeGroups())
            {
                Log.Message("[Holsters] Groups initialised.");
                ResetAllGroups();
            } 
            else
            {
                CheckIfAllWeaponsBelongToAGroup();
            }

            isFirstLaunch = false;
        }

        private static bool ShouldInitializeGroups() => isFirstLaunch || RenderGroups.NullOrEmpty();


        public static void ResetAllGroups()
        {
            RenderGroups = IR_HolstersInit.LoadDefaultWeaponGroups();
        }


        public static void CheckIfAllWeaponsBelongToAGroup()
        {
            List<ThingDef> weaponsWithoutGroup = new List<ThingDef>();

            foreach (ThingDef thing in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(ThingDef)))
            {
                if (GetWeaponGroupOf(thing.defName).Name.Equals("noGroup") )
                {
                    weaponsWithoutGroup.Add(thing);
                }
            } 

            IR_HolstersInit.SortWeaponsIntoGroups(ref RenderGroups, weaponsWithoutGroup);
            
        }


        public static void ResetGroup(HolsterWeaponRenderGroup group)
        {
            //var newGroup = group;

            group.HolsterRenderData.Reset();
            //RenderGroups[RenderGroups.IndexOf(group)]= newGroup;
        }
        public static void ChangeGroupsName(HolsterWeaponRenderGroup group, string name)
        {
            //var newGroup = group;

            //newGroup.Name = name;
            group.Name = name;
            //RenderGroups[RenderGroups.IndexOf(group)] = newGroup;
        }
        public static void RemoveGroup(HolsterWeaponRenderGroup group) => _ = RenderGroups.Remove(group);

        public static void AddNewSettingsGroup(string name) => RenderGroups.Add(new HolsterWeaponRenderGroup(name));

        public static HolsterWeaponRenderGroup GetWeaponGroupOf(string weaponDefName) =>
            RenderGroups.SingleOrDefault(rg => rg.HasWeapon(weaponDefName)) ?? new HolsterWeaponRenderGroup("noGroup");

        //GETTING DATA
        public static Vector3 GetWeaponPos(string weaponDefName, Rot4 rot, bool isSide, Pawn pawn, HolsterWeaponRenderGroup group)
        {
            BodyType bodyType = (BodyType)Enum.Parse(typeof(BodyType), pawn.story?.bodyType?.defName.ToLower());

            return GetWeaponPos(group, rot, isSide, bodyType);
        }

        public static Vector3 GetWeaponPos(HolsterWeaponRenderGroup group, Rot4 rot, bool isSide, BodyType body)
        {
            Vector3 pos = group.HolsterRenderData.GetConfiguration(rot, isSide).Position;

            //Vector3 offset = group.GetBodyOffset(rot, isSide) * group.GetBodyOffsetModif(body,isSide);

            //pos += offset;

            return pos;
        }

        public static Vector3 GetWeaponPos(HolsterWeaponRenderGroup group, Rot4 rot, bool isSide)
        {
            Vector3 pos = group.HolsterRenderData.GetConfiguration(rot, isSide).Position;

            return pos;
        }

        public static bool GetWeaponLayer(HolsterWeaponRenderGroup group, Rot4 rot, bool isSide)
        {
            return group.HolsterRenderData.GetConfiguration(rot, isSide).IsAtFront;
        }

        public static float GetWeaponAngle(string weaponDefName, Rot4 rot, bool isSide)
        {
            HolsterWeaponRenderGroup group = GetWeaponGroupOf(weaponDefName);
            return GetWeaponAngle(group, rot, isSide);
        }

        public static float GetWeaponAngle(HolsterWeaponRenderGroup group, Rot4 rot, bool isSide)
        {
            return group.HolsterRenderData.GetConfiguration(rot, isSide).Rotation;
        }
        /*
        public static bool GetWeaponFlip(string weaponDefName, Rot4 rot, bool isSide)
        {
            WeaponGroupCordInfo group = GetWeaponGroupOf(weaponDefName);
            return GetWeaponFlip(group, rot, isSide);
        }*/

        public static bool GetWeaponFlip(HolsterWeaponRenderGroup group, Rot4 rot, bool isSide)
        {
            return group.HolsterRenderData.GetConfiguration(rot, isSide).IsFlipped;
        }
    }
}