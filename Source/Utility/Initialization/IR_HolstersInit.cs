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
            IR_HolstersSettings.InitBasicGroups();
        }

        public static List<HolsterWeaponRenderGroup> LoadDefaultWeaponGroups()
        {
            var defaultGroups = new List<HolsterWeaponRenderGroup>();

            defaultGroups.Add(new HolsterWeaponRenderGroup("Long Ranged")
            {
                HolsterRenderData = new HolsterRenderData
                {
                    Configuration = new Dictionary<Rot4, HolsterConfiguration>
                    {
                        { Rot4.South, new HolsterConfiguration(new Vector3(-1f, 0, 1f), 255f, 1f, true, false, Vector3.zero) },
                        { Rot4.North, new HolsterConfiguration(new Vector3(0f, 0, 0f), 310f, 1f, false, true, Vector3.zero) },
                        { Rot4.East, new HolsterConfiguration(new Vector3(-2f, 0, 0f), 295f, 1f, false, true, Vector3.zero) },
                        { Rot4.West, new HolsterConfiguration(new Vector3(2f, 0, 0f), 255f, 1f, true, false, Vector3.zero) }
                    },
                    SideConfiguration = new Dictionary<Rot4, HolsterConfiguration>
                    {
                        { Rot4.South, new HolsterConfiguration(new Vector3(1.5f, 0, 0f), 275f, 1f, false, false, Vector3.zero) },
                        { Rot4.North, new HolsterConfiguration(new Vector3(-1f, 0, 0.5f), 265f, 1f, true, true, Vector3.zero) },
                        { Rot4.East, new HolsterConfiguration(new Vector3(-2f, 0, 0.5f), 290f, 1f, false, false, Vector3.zero) },
                        { Rot4.West, new HolsterConfiguration(new Vector3(2f, 0, 0.5f), 255f, 1f, true, true, Vector3.zero) }
                    }
                }
            });

            // Short Ranged
            defaultGroups.Add(new HolsterWeaponRenderGroup("Short Ranged")
            {
                HolsterRenderData = new HolsterRenderData
                {
                    Configuration = new Dictionary<Rot4, HolsterConfiguration>
                    {
                        { Rot4.South, new HolsterConfiguration(new Vector3(1.5f, 0, -2.5f), 140f, 1f, true, false, Vector3.zero) },
                        { Rot4.North, new HolsterConfiguration(new Vector3(-1.5f, 0, -2.5f), 25f, 1f, false, true, Vector3.zero) },
                        { Rot4.East, new HolsterConfiguration(new Vector3(1f, 0, -2.5f), 25f, 1f, false, false, Vector3.zero) },
                        { Rot4.West, new HolsterConfiguration(new Vector3(-0.5f, 0, -2.5f), 145f, 1f, true, true, Vector3.zero) }
                    },
                    SideConfiguration = new Dictionary<Rot4, HolsterConfiguration>
                    {
                        { Rot4.South, new HolsterConfiguration(new Vector3(-1.5f, 0, -2.5f), 40f, 1f, false, false, Vector3.zero) },
                        { Rot4.North, new HolsterConfiguration(new Vector3(1.5f, 0, -2.5f), 155f, 1f, true, false, Vector3.zero) },
                        { Rot4.East, new HolsterConfiguration(new Vector3(0.5f, 0, -2.5f), 380f, 1f, false, true, Vector3.zero) },
                        { Rot4.West, new HolsterConfiguration(new Vector3(0f, 0, -2.5f), 145f, 1f, true, false, Vector3.zero) }
                    }
                }
            });

            // Bows
            defaultGroups.Add(new HolsterWeaponRenderGroup("Bows")
            {
                HolsterRenderData = new HolsterRenderData
                {
                    Configuration = new Dictionary<Rot4, HolsterConfiguration>
                    {
                        { Rot4.South, new HolsterConfiguration(new Vector3(-0.5f, 0, 0f), 135f, 1f, false, false, Vector3.zero) },
                        { Rot4.North, new HolsterConfiguration(new Vector3(0f, 0, 0f), 35f, 1f, false, true, Vector3.zero) },
                        { Rot4.East, new HolsterConfiguration(new Vector3(-2f, 0, 0f), 20f, 1f, false, true, Vector3.zero) },
                        { Rot4.West, new HolsterConfiguration(new Vector3(2f, 0, 0f), 340f, 1f, false, false, Vector3.zero) }
                    },
                    SideConfiguration = new Dictionary<Rot4, HolsterConfiguration>
                    {
                        { Rot4.South, new HolsterConfiguration(new Vector3(0.5f, 0, -1.5f), 110f, 1f, false, false, Vector3.zero) },
                        { Rot4.North, new HolsterConfiguration(new Vector3(0f, 0, -2f), 80f, 1f, false, true, Vector3.zero) },
                        { Rot4.East, new HolsterConfiguration(new Vector3(-0.5f, 0, -2f), 80f, 1f, false, true, Vector3.zero) },
                        { Rot4.West, new HolsterConfiguration(new Vector3(0f, 0, -2f), 95f, 1f, false, false, Vector3.zero) }
                    }
                }
            });

            // Long Melee
            defaultGroups.Add(new HolsterWeaponRenderGroup("Long Melee")
            {
                HolsterRenderData = new HolsterRenderData
                {
                    Configuration = new Dictionary<Rot4, HolsterConfiguration>
                    {
                        { Rot4.South, new HolsterConfiguration(new Vector3(-0.5f, 0, 0.5f), 55f, 1f, true, false, Vector3.zero) },
                        { Rot4.North, new HolsterConfiguration(new Vector3(0.5f, 0, -0.5f), 120f, 1f, false, true, Vector3.zero) },
                        { Rot4.East, new HolsterConfiguration(new Vector3(-2f, 0, 0f), 110f, 1f, false, true, Vector3.zero) },
                        { Rot4.West, new HolsterConfiguration(new Vector3(2f, 0, 0f), 75f, 1f, true, false, Vector3.zero) }
                    },
                    SideConfiguration = new Dictionary<Rot4, HolsterConfiguration>
                    {
                        { Rot4.South, new HolsterConfiguration(new Vector3(-1.5f, 0, -0.5f), 55f, 1f, false, false, Vector3.zero) },
                        { Rot4.North, new HolsterConfiguration(new Vector3(0f, 0, -0.5f), 115f, 1f, false, true, Vector3.zero) },
                        { Rot4.East, new HolsterConfiguration(new Vector3(-2f, 0, -0.5f), 110f, 1f, false, true, Vector3.zero) },
                        { Rot4.West, new HolsterConfiguration(new Vector3(1f, 0, 0.5f), 75f, 1f, false, false, Vector3.zero) }
                    }
                }
            });

            // Short Melee
            defaultGroups.Add(new HolsterWeaponRenderGroup("Short Melee")
            {
                HolsterRenderData = new HolsterRenderData
                {
                    Configuration = new Dictionary<Rot4, HolsterConfiguration>
                    {
                        { Rot4.South, new HolsterConfiguration(new Vector3(1.5f, 0, -2f), 60f, 0.8f, true, true, Vector3.zero) },
                        { Rot4.North, new HolsterConfiguration(new Vector3(-1.5f, 0, -2f), 110f, 0.8f, false, false, Vector3.zero) },
                        { Rot4.East, new HolsterConfiguration(new Vector3(0.5f, 0, -2f), 135f, 0.8f, false, true, Vector3.zero) },
                        { Rot4.West, new HolsterConfiguration(new Vector3(-0.5f, 0, -2f), 50f, 0.8f, true, false, Vector3.zero) }
                    },
                    SideConfiguration = new Dictionary<Rot4, HolsterConfiguration>
                    {
                        { Rot4.South, new HolsterConfiguration(new Vector3(-1.5f, 0, -2f), 115f, 0.8f, true, true, Vector3.zero) },
                        { Rot4.North, new HolsterConfiguration(new Vector3(1.5f, 0, -2f), 75f, 0.8f, false, false, Vector3.zero) },
                        { Rot4.East, new HolsterConfiguration(new Vector3(0.5f, 0, -2f), 135f, 0.8f, true, true, Vector3.zero) },
                        { Rot4.West, new HolsterConfiguration(new Vector3(-0.5f, 0, -2f), 50f, 0.8f, false, false, Vector3.zero) }
                    }
                }
            });

            // Grenades
            defaultGroups.Add(new HolsterWeaponRenderGroup("Grenades")
            {
                HolsterRenderData = new HolsterRenderData
                {
                    Configuration = new Dictionary<Rot4, HolsterConfiguration>
                    {
                        { Rot4.South, new HolsterConfiguration(new Vector3(0.5f, 0, -2.5f), -20f, 0.6f, false, true, Vector3.zero) },
                        { Rot4.North, new HolsterConfiguration(new Vector3(-0.5f, 0, -2.5f), 320f, 0.6f, false, false, Vector3.zero) },
                        { Rot4.East, new HolsterConfiguration(new Vector3(1f, 0, -2.5f), 280f, 0.6f, false, true, Vector3.zero) },
                        { Rot4.West, new HolsterConfiguration(new Vector3(-1f, 0, -2.5f), 30f, 0.6f, false, false, Vector3.zero) }
                    },
                    SideConfiguration = new Dictionary<Rot4, HolsterConfiguration>
                    {
                        { Rot4.South, new HolsterConfiguration(new Vector3(-1.5f, 0, -2f), 10f, 0.6f, true, true, Vector3.zero) },
                        { Rot4.North, new HolsterConfiguration(new Vector3(1.5f, 0, -2f), 320f, 0.6f, false, false, Vector3.zero) },
                        { Rot4.East, new HolsterConfiguration(new Vector3(2f, 0, -0.5f), 260f, 0.6f, false, false, Vector3.zero) },
                        { Rot4.West, new HolsterConfiguration(new Vector3(-2f, 0, -0.5f), 285f, 0.6f, true, true, Vector3.zero) }
                    }
                }
            });

            var doNotDisplayGroup = new HolsterWeaponRenderGroup("Do not display")
            {
                ShouldDisplay = false
            };

            defaultGroups.Add(doNotDisplayGroup);

            SortWeaponsIntoGroups(ref defaultGroups);

            return defaultGroups;
        }

        public static void SortWeaponsIntoGroups(ref List<HolsterWeaponRenderGroup> groups)
        {
            foreach (ThingDef thing in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(ThingDef)))
            {
                //Log.Message(thing.defName);
                if (!thing.IsWeapon)
                {
                    continue;
                }
                //Log.Message(thing.defName + "is a weapon!");
                if (thing.defName.Equals("WoodLog") && thing.defName.Equals("Beer"))
                {
                    groups[6].Weapons.Add(thing.defName);
                    continue;
                }

                if (thing.IsRangedWeapon)
                {
                    //Log.Message(thing.defName + "is a ranged weapon!");
                    if (thing.defName.StartsWith("Bow_"))
                    {
                        groups[2].Weapons.Add(thing.defName);
                        continue;
                    }
                    else if (thing.defName.StartsWith("Weapon_Grenade"))
                    {
                        groups[5].Weapons.Add(thing.defName);
                        continue;
                    }
                    else if (thing.uiIconScale > 1.1f)
                    {
                        groups[1].Weapons.Add(thing.defName);

                        continue;
                    }
                    else groups[0].Weapons.Add(thing.defName);
                    continue;
                }
                if (thing.IsMeleeWeapon)
                {
                    if (thing.uiIconScale > 1.1f)
                    {
                        groups[4].Weapons.Add(thing.defName);
                        continue;
                    }
                    else groups[3].Weapons.Add(thing.defName);
                    continue;
                }
                else groups[6].Weapons.Add(thing.defName);
                continue;
            }
        }

        // TODO: use tags to sort weapons defined by the user?
        public static void SortWeaponsIntoGroups(ref List<HolsterWeaponRenderGroup> groups, List<ThingDef> weapons)
        {
            foreach (ThingDef thing in weapons)
            {

                //Log.Message(thing.defName);
                if (!thing.IsWeapon)
                {
                    continue;
                }
                //Log.Message(thing.defName + "is a weapon!");
                if (thing.defName.Equals("WoodLog") && thing.defName.Equals("Beer"))
                {
                    if (groups.Count < 7)
                    {
                        groups[0].Weapons.Add(thing.defName);
                        continue;
                    }

                    groups[6].Weapons.Add(thing.defName);
                    continue;
                }

                if (thing.IsRangedWeapon)
                {
                    //Log.Message(thing.defName + "is a ranged weapon!");
                    if (thing.defName.StartsWith("Bow_"))
                    {
                        if (groups.Count < 7)
                        {
                            groups[0].Weapons.Add(thing.defName);
                            continue;
                        }
                        groups[2].Weapons.Add(thing.defName);
                        continue;
                    }
                    else if (thing.defName.StartsWith("Weapon_Grenade"))
                    {
                        if (groups.Count < 7)
                        {
                            groups[0].Weapons.Add(thing.defName);
                            continue;
                        }
                        groups[5].Weapons.Add(thing.defName);
                        continue;
                    }
                    else if (thing.uiIconScale > 1.1f)
                    {
                        if (groups.Count < 7)
                        {
                            groups[0].Weapons.Add(thing.defName);
                            continue;
                        }
                        groups[1].Weapons.Add(thing.defName);

                        continue;
                    }
                    else groups[0].Weapons.Add(thing.defName);
                    continue;
                }
                if (thing.IsMeleeWeapon)
                {
                    if (thing.uiIconScale > 1.1f)
                    {
                        if (groups.Count < 7)
                        {
                            groups[0].Weapons.Add(thing.defName);
                            continue;
                        }
                        groups[4].Weapons.Add(thing.defName);
                        continue;
                    }
                    else
                    {
                        if (groups.Count < 7)
                        {
                            groups[0].Weapons.Add(thing.defName);
                            continue;
                        }
                        groups[3].Weapons.Add(thing.defName);
                    }
                    continue;
                }
                else
                {
                    if (groups.Count < 7)
                    {
                        groups[0].Weapons.Add(thing.defName);
                        continue;
                    }
                    groups[6].Weapons.Add(thing.defName);

                }
                continue;
            }
        }
    }
}