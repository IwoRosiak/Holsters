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

        public static bool isFirstLaunch = true;

        public static List<WeaponGroupCordInfo> groups;

        public static bool displaySide;

        public static bool smartSideDisplay;

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref groups,"groupsCordSettings", LookMode.Deep);
            Scribe_Values.Look(ref displaySide, "displaySide", true);
            Scribe_Values.Look(ref smartSideDisplay, "smartSideDisplay", true);
            Scribe_Values.Look(ref isFirstLaunch, "isFirstLaunch", false);

            base.ExposeData();
        }

        //MANAGING GROUPS
        public static void InitBasicGroups()
        {
            if (isFirstLaunch || groups.NullOrEmpty())
            {
                Log.Message("Holsters: groups initialised.");
                ResetAllGroups();
            }

            isFirstLaunch = false;
        }

        public static void ResetAllGroups()
        {
            groups.Clear();

            groups = IR_HolstersInit.LoadDefaultWeaponGroups();
        }

        public static void ResetGroup(WeaponGroupCordInfo group)
        {
            var newGroup = group;

            newGroup.Reset();

            groups[groups.IndexOf(group)]= newGroup;
        }
        public static void ChangeGroupsName(WeaponGroupCordInfo group, string name)
        {
            var newGroup = group;

            newGroup.Name = name;

            groups[groups.IndexOf(group)] = newGroup;
        }
        public static void RemoveGroup(WeaponGroupCordInfo group)
        {
            groups.Remove(group);
        }

        public static void AddNewSettingsGroup(string name)
        {
            groups.Add(new WeaponGroupCordInfo(name));
        }

        public static WeaponGroupCordInfo GetWeaponGroupOf(string weaponDefName)
        {
            foreach (WeaponGroupCordInfo group in groups)
            {
                foreach (string weaponInGroup in group.weapons)
                {
                    if (weaponDefName.Equals(weaponInGroup))
                    {
                        return group;
                    }
                }
            }

            return new WeaponGroupCordInfo("noGroup");
        }

        //GETTING DATA
        public static Vector3 GetWeaponPos(string weaponDefName, Rot4 rot, bool isSide, Pawn pawn)
        {
            BodyType bodyType = IR_DisplayWeapon.GetBodyType(pawn);
            if (pawn != null)
            {
                bodyType = IR_DisplayWeapon.GetBodyType(pawn);
            }
            
            WeaponGroupCordInfo group = GetWeaponGroupOf(weaponDefName);

            return GetWeaponPos(group, rot, isSide, bodyType);
        }

        public static Vector3 GetWeaponPos(WeaponGroupCordInfo group, Rot4 rot, bool isSide, BodyType body)
        {
            Vector3 pos = group.GetPos(rot, isSide);

            Vector3 offset = group.GetBodyOffset(rot, isSide) * group.GetBodyOffsetModif(body,isSide);

            pos += offset;

            return pos;
        }

        public static Vector3 GetWeaponPos(WeaponGroupCordInfo group, Rot4 rot, bool isSide)
        {
            Vector3 pos = group.GetPos(rot, isSide);

            return pos;
        }

        public static bool GetWeaponLayer(string weaponDefName, Rot4 rot, bool isSide)
        {
            WeaponGroupCordInfo group = GetWeaponGroupOf(weaponDefName);

            return group.GetLayer(rot, isSide);
        }

        public static float GetWeaponAngle(string weaponDefName, Rot4 rot, bool isSide)
        {
            WeaponGroupCordInfo group = GetWeaponGroupOf(weaponDefName);
            return GetWeaponAngle(group, rot, isSide);
        }

        public static float GetWeaponAngle(WeaponGroupCordInfo group, Rot4 rot, bool isSide)
        {
            return group.GetAngle(rot, isSide);
        }

        public static bool GetWeaponFlip(string weaponDefName, Rot4 rot, bool isSide)
        {
            WeaponGroupCordInfo group = GetWeaponGroupOf(weaponDefName);
            return GetWeaponFlip(group, rot, isSide);
        }

        public static bool GetWeaponFlip(WeaponGroupCordInfo group, Rot4 rot, bool isSide)
        {
            return group.GetFlip(rot, isSide);
        }
    }
}