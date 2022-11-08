using Holsters;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    [StaticConstructorOnStartup]
    public static class IR_HolstersInit
    {
        static IR_HolstersInit()
        {
            IR_HolstersSettings.Initialise();
        }
        /*
        public static List<WeaponGroupCordInfo> LoadDefaultWeaponGroups()
        {
            List<WeaponGroupCordInfo> defaultGroups = new List<WeaponGroupCordInfo>();


            var doNotDisplayGroup = new WeaponGroupCordInfo("Do not display");
            doNotDisplayGroup.isDisplay = false;

            defaultGroups.Add(doNotDisplayGroup);

            SortWeaponsIntoGroups(ref defaultGroups);

            return defaultGroups;
        }*/

    }
}