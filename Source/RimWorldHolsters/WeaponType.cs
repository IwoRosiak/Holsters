using Verse;

namespace RimWorldHolsters
{
    [StaticConstructorOnStartup]
    public static class IR_WeaponType
    {
        public static WeaponType EstablishWeaponType(ThingWithComps weapon)
        {
            if (weapon.def.IsRangedWeapon)
            {
                if (weapon.def.defName.StartsWith("Bow_"))
                {
                    return WeaponType.bow;
                }
                if (weapon.def.Verbs[0].CausesExplosion)
                {
                    return WeaponType.grenades;
                }
                if (weapon.def.uiIconScale != 1)
                {
                    return WeaponType.shortRanged;
                }
                return WeaponType.longRanged;
            }
            if (weapon.def.IsMeleeWeapon)
            {
                if (weapon.def.uiIconScale != 1)
                {
                    return WeaponType.shortMelee;
                }
                return WeaponType.longMelee;
            }
            return WeaponType.longMelee;
        }

        public static bool EstablishWeaponSize(ThingWithComps weapon)
        {
            switch (EstablishWeaponType(weapon))
            {
                case WeaponType.longRanged:
                case WeaponType.longMelee:
                case WeaponType.bow:
                    return true;
                    break;

                case WeaponType.shortMelee:
                case WeaponType.shortRanged:
                    return false;
                    break;
            }
            return false;
        }
    }
}