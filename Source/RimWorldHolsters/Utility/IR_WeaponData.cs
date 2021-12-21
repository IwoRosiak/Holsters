using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    public static class IR_WeaponData
    {
        public static Vector3 GetWeaponPos(WeaponType type, Rot4 rot, bool isSide)
        {
            Vector3 baseOffset = Vector3.zero;
            if (weaponData?.ContainsKey(type) == true)
            {
                if (isSide)
                {
                    baseOffset = weaponData[type].posSide[rot];
                }
                else
                {
                    baseOffset = weaponData[type].pos[rot];
                }
            }

            Vector3 settingsOffset = Vector3.zero;
            if (IR_HolstersSettings.WeaponDataSettings?.ContainsKey(type) == true && !IR_HolstersSettings.WeaponDataSettings[type].pos.NullOrEmpty())
            {
                if (isSide && !IR_HolstersSettings.WeaponDataSettings[type].posSide.NullOrEmpty())
                {
                    settingsOffset = IR_HolstersSettings.WeaponDataSettings[type].posSide[rot];
                }
                else
                {
                    settingsOffset = IR_HolstersSettings.WeaponDataSettings[type].pos[rot];
                }
            }

            return baseOffset + settingsOffset;
        }

        public static float GetWeaponAngle(WeaponType type, Rot4 rot, bool isSide)
        {
            float baseOffset = 0;
            if (weaponData?.ContainsKey(type) == true)
            {
                if (isSide)
                {
                    baseOffset = weaponData[type].angleSide[rot];
                }
                else
                {
                    baseOffset = weaponData[type].angle[rot];
                }
            }
            float settingsOffset = 0;
            if (IR_HolstersSettings.WeaponDataSettings?.ContainsKey(type) == true && !IR_HolstersSettings.WeaponDataSettings[type].angle.NullOrEmpty())
            {
                if (isSide && !IR_HolstersSettings.WeaponDataSettings[type].angleSide.NullOrEmpty())
                {
                    settingsOffset = IR_HolstersSettings.WeaponDataSettings[type].angleSide[rot];
                }
                else
                {
                    settingsOffset = IR_HolstersSettings.WeaponDataSettings[type].angle[rot];
                }
            }

            return baseOffset + settingsOffset;
        }

        public static bool GetWeaponFlip(WeaponType type, Rot4 rot, bool isSide)
        {
            bool isFlip = false;
            if (IR_HolstersSettings.WeaponDataSettings?.ContainsKey(type) == true && IR_HolstersSettings.WeaponDataSettings[type].flip?.ContainsKey(rot) == true && !IR_HolstersSettings.WeaponDataSettings[type].flip.NullOrEmpty())
            {
                isFlip = IR_HolstersSettings.WeaponDataSettings[type].GetFlip(rot, isSide);
            }
            else if (weaponData.ContainsKey(type) == true && weaponData[type].flip?.ContainsKey(rot) == true && !weaponData[type].flip.NullOrEmpty())
            {
                isFlip = weaponData[type].GetFlip(rot, isSide);
            }

            return isFlip;
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