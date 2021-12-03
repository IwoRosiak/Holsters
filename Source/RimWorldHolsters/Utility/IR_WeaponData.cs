using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    public static class IR_WeaponData
    {
        public static Vector3 GetWeaponPos(WeaponType type, Rot4 rot)
        {
            Vector3 baseOffset = Vector3.zero;
            if (weaponData?.ContainsKey(type) == true)
            {
                baseOffset = weaponData[type].pos[rot];
            }

            Vector3 settingsOffset = Vector3.zero;
            if (IR_HolstersSettings.WeaponDataSettings?.ContainsKey(type) == true && !IR_HolstersSettings.WeaponDataSettings[type].pos.NullOrEmpty())
            {
                settingsOffset = IR_HolstersSettings.WeaponDataSettings[type].pos[rot];
            }

            return baseOffset + settingsOffset;
        }

        public static float GetWeaponAngle(WeaponType type, Rot4 rot)
        {
            float baseOffset = 0;
            if (weaponData?.ContainsKey(type) == true)
            {
                baseOffset = weaponData[type].angle[rot];
            }
            float settingsOffset = 0;
            if (IR_HolstersSettings.WeaponDataSettings?.ContainsKey(type) == true && !IR_HolstersSettings.WeaponDataSettings[type].angle.NullOrEmpty())
            {
                settingsOffset = IR_HolstersSettings.WeaponDataSettings[type].angle[rot];
            }

            return baseOffset + settingsOffset;
        }

        public static Dictionary<WeaponType, WeaponPos> weaponData;

        public static WeaponPos longRanged = new WeaponPos();
        public static WeaponPos shortRanged = new WeaponPos();
        public static WeaponPos longMelee = new WeaponPos();
        public static WeaponPos shortMelee = new WeaponPos();
        public static WeaponPos bow = new WeaponPos();

        public static WeaponPos custom1 = new WeaponPos();
        public static WeaponPos custom2 = new WeaponPos();
        public static WeaponPos custom3 = new WeaponPos();
        public static WeaponPos grenades = new WeaponPos();
        public static WeaponPos doNotDisplay = new WeaponPos();
    }
}