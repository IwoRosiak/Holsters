using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    public static class IR_WeaponData
    {
        public static Vector3 GetWeaponPos(WeaponType type, Rot4 rot)
        {
            return weaponData[type].pos[rot] + IR_HolstersSettings.WeaponDataSettings[type].pos[rot];
        }

        public static float GetWeaponAngle(WeaponType type, Rot4 rot)
        {
            return weaponData[type].angle[rot] + IR_HolstersSettings.WeaponDataSettings[type].angle[rot];
        }

        public static Dictionary<WeaponType, WeaponPos> weaponData;

        public static WeaponPos longRanged = new WeaponPos();
        public static WeaponPos shortRanged = new WeaponPos();
        public static WeaponPos longMelee = new WeaponPos();
        public static WeaponPos shortMelee = new WeaponPos();
        public static WeaponPos bow = new WeaponPos();
    }
}