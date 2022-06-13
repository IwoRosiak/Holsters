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

        public static List<WeaponGroupCordInfo> LoadDefaultWeaponGroups()
        {
            List<WeaponGroupCordInfo> defaultGroups = new List<WeaponGroupCordInfo>();

            //Long Ranged
            defaultGroups.Add(new WeaponGroupCordInfo("Long Ranged",

            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-1f, 0, 1f) },
                {Rot4.North, new Vector3(0f, 0, 0f) },
                {Rot4.East, new Vector3(-2f, 0, 0f) },
                {Rot4.West, new Vector3(2f, 0, 0f) }
            },

            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 255f},
                {Rot4.North, 310f},
                {Rot4.East, 295f},
                {Rot4.West, 255f}
            },

            new Dictionary<Rot4, bool>()
            {
                { Rot4.South, true},
                { Rot4.North, false},
                { Rot4.East, false},
                { Rot4.West, true}
            },  
            
            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(1.5f, 0, 0f) },
                {Rot4.North, new Vector3(-1f, 0, 0.5f) },
                {Rot4.East, new Vector3(-2f, 0, 0.5f) },
                {Rot4.West, new Vector3(2f, 0, 0.5f) }
            },

            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 275f},
                {Rot4.North, 265f},
                {Rot4.East, 290f},
                {Rot4.West, 255f}
            },

             new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, true},
                {Rot4.East, false},
                {Rot4.West, true}
            },
            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 1},
                {Rot4.North, 1},
                {Rot4.East, 1},
                {Rot4.West, 1}
            }, 
            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, true},
                {Rot4.East, true},
                {Rot4.West, false}
            },
            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, true},
                {Rot4.East, false},
                {Rot4.West, true}
            },
            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(0, 0, 0f) },
                {Rot4.North, new Vector3(0, 0, 0f) },
                {Rot4.East, new Vector3(-2f, 0, 0f) },
                {Rot4.West, new Vector3(2f, 0, 0f) }
            },
            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(1f, 0, 0f) },
                {Rot4.North, new Vector3(-1f, 0, 0f) },
                {Rot4.East, new Vector3(-2f, 0, 0f) },
                {Rot4.West, new Vector3(2f, 0, 0f) }
            },
            new Dictionary<BodyType, float>()
            {
                {BodyType.male, 0},
                {BodyType.female, 0},
                {BodyType.fat, 1},
                {BodyType.hulk, 0.5f},
                {BodyType.thin, -0.2f}
            }));


            //Short Ranged
            defaultGroups.Add(new WeaponGroupCordInfo("Short Ranged",

            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(1.5f, 0, -2.5f) },
                {Rot4.North, new Vector3(-1.5f,0, -2.5f) },
                {Rot4.East, new Vector3(1f, 0, -2.5f) },
                {Rot4.West, new Vector3(-0.5f, 0, -2.5f)}
            },

            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 140f },
                {Rot4.North, 25f },
                {Rot4.East, 25f },
                {Rot4.West, 145f }
            },

            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, true},
                {Rot4.North, false},
                {Rot4.East, false},
                {Rot4.West, true}
            },

            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-1.5f, 0, -2.5f) },
                {Rot4.North, new Vector3(1.5f,0, -2.5f) },
                {Rot4.East, new Vector3(0.5f, 0, -2.5f) },
                {Rot4.West, new Vector3(0f, 0, -2.5f)}
            },

            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 40f },
                {Rot4.North, 155f },
                {Rot4.East, 380f },
                {Rot4.West, 145f }
            },

            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, true},
                {Rot4.East, false},
                {Rot4.West, true}
            },
            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 1},
                {Rot4.North, 1},
                {Rot4.East, 1},
                {Rot4.West, 1}
            },
            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, true},
                {Rot4.North, false},
                {Rot4.East, false},
                {Rot4.West, true}
            },
            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, true},
                {Rot4.North, false},
                {Rot4.East, true},
                {Rot4.West, false}
            },
            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(2f, 0, -0.5f) },
                {Rot4.North, new Vector3(-2f, 0, -0.5f) },
                {Rot4.East, new Vector3(-2f, 0, 0f) },
                {Rot4.West, new Vector3(2f, 0, 0f) }
            },
            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-2f, 0, -0.5f) },
                {Rot4.North, new Vector3(2f, 0, -0.5f) },
                {Rot4.East, new Vector3(-2f, 0, 0f) },
                {Rot4.West, new Vector3(2f, 0, 0f) }
            },
            new Dictionary<BodyType, float>()
            {
                {BodyType.male, 0},
                {BodyType.female, 0},
                {BodyType.fat, 1},
                {BodyType.hulk, 0.8f},
                {BodyType.thin, -0.2f}
            }));



            //Bows
            defaultGroups.Add(new WeaponGroupCordInfo("Bows",

            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-0.5f, 0, 0f) },
                {Rot4.North, new Vector3(0f, 0, 0f) },
                {Rot4.East, new Vector3(-2f, 0, 0f) },
                {Rot4.West, new Vector3(2f,0 , 0f) }
            },

            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 135},
                {Rot4.North, 35f},
                {Rot4.East, 20f},
                {Rot4.West, 340f}
            },

            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, false},
                {Rot4.East, false},
                {Rot4.West, false}
            },
            
            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(0.5f, 0, -1.5f) },
                {Rot4.North, new Vector3(0f, 0, -2f) },
                {Rot4.East, new Vector3(-0.5f, 0, -2f) },
                {Rot4.West, new Vector3(0f,0 , -2f) }
            },

            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 110f},
                {Rot4.North, 80f},
                {Rot4.East, 80f},
                {Rot4.West, 95f}
            },

            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, false},
                {Rot4.East, false},
                {Rot4.West, false}
            },
            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 1},
                {Rot4.North, 1},
                {Rot4.East, 1},
                {Rot4.West, 1}
            },
            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, true},
                {Rot4.East, true},
                {Rot4.West, false}
            },
            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, true},
                {Rot4.East, true},
                {Rot4.West, false}
            },
            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(0, 0, 0f) },
                {Rot4.North, new Vector3(0, 0, 0f) },
                {Rot4.East, new Vector3(-1f, 0, 0f) },
                {Rot4.West, new Vector3(1f, 0, 0f) }
            },
            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(0f, 0, 0f) },
                {Rot4.North, new Vector3(0f, 0, 0f) },
                {Rot4.East, new Vector3(0f, 0, 0f) },
                {Rot4.West, new Vector3(0f, 0, 0f) }
            },
            new Dictionary<BodyType, float>()
            {
                {BodyType.male, 0},
                {BodyType.female, 0},
                {BodyType.fat, 1},
                {BodyType.hulk, 1},
                {BodyType.thin, -0.2f}
            }));

            //Long Melee

            defaultGroups.Add(new WeaponGroupCordInfo("Long Melee",

            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-0.5f, 0, 0.5f) },
                {Rot4.North, new Vector3(0.5f, 0, -0.5f) },
                {Rot4.East, new Vector3(-2f, 0, 0f) },
                {Rot4.West, new Vector3(2f, 0, 0f) }
            },

            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 55f},
                {Rot4.North, 120f},
                {Rot4.East, 110f},
                {Rot4.West, 75f}
            },

            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, true},
                {Rot4.North, false},
                {Rot4.East, false},
                {Rot4.West, true}
            },

            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-1.5f, 0, -0.5f) },
                {Rot4.North, new Vector3(0f, 0, -0.5f) },
                {Rot4.East, new Vector3(-2f, 0, -0.5f) },
                {Rot4.West, new Vector3(1f, 0, 0.5f) }
            },

            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 55f},
                {Rot4.North, 115f},
                {Rot4.East, 110f},
                {Rot4.West, 75f}
            },

            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, false},
                {Rot4.East, false},
                {Rot4.West, false}
            },
            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 1},
                {Rot4.North, 1},
                {Rot4.East, 1},
                {Rot4.West, 1}
            },
            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, true},
                {Rot4.East, true},
                {Rot4.West, false}
            },
            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, true},
                {Rot4.East, true},
                {Rot4.West, false}
            },
            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(0f, 0, 0.5f) },
                {Rot4.North, new Vector3(0f, 0, 0.5f) },
                {Rot4.East, new Vector3(-2f, 0, 0f) },
                {Rot4.West, new Vector3(2f, 0, 0f) }
            },
            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(0, 0, 0f) },
                {Rot4.North, new Vector3(0, 0, 0f) },
                {Rot4.East, new Vector3(-2f, 0, 0f) },
                {Rot4.West, new Vector3(2f, 0, 0f) }
            },
            new Dictionary<BodyType, float>()
            {
                {BodyType.male, 0.3f},
                {BodyType.female, 0.3f},
                {BodyType.fat, 1},
                {BodyType.hulk, 0.8f},
                {BodyType.thin, 0f}
            }));

            //Short Melee

            defaultGroups.Add(new WeaponGroupCordInfo("Short Melee",

            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South,  new Vector3(1.5f, 0, -2f) },
                {Rot4.North, new Vector3(-1.5f, 0, -2f) },
                {Rot4.East, new Vector3(0.5f, 0, -2f) },
                {Rot4.West,  new Vector3(-0.5f, 0, -2f) }
            },

            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 60f},
                {Rot4.North, 110f},
                {Rot4.East, 135f},
                {Rot4.West, 50f}
            },

            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, true},
                {Rot4.North, false},
                {Rot4.East, false},
                {Rot4.West, true}
            },

            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South,  new Vector3(-1.5f, 0, -2f) },
                {Rot4.North, new Vector3(1.5f, 0, -2f) },
                {Rot4.East, new Vector3(0.5f, 0, -2f) },
                {Rot4.West,  new Vector3(-0.5f, 0, -2f) }
            },

            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 115f},
                {Rot4.North, 75f},
                {Rot4.East, 135f},
                {Rot4.West, 50f}
            },

            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, true},
                {Rot4.East, false},
                {Rot4.West, true}
            },
            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 0.8f},
                {Rot4.North, 0.8f},
                {Rot4.East, 0.8f},
                {Rot4.West, 0.8f}
            },
            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, true},
                {Rot4.North, false},
                {Rot4.East, false},
                {Rot4.West, true}
            },
            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, true},
                {Rot4.North, false},
                {Rot4.East, true},
                {Rot4.West, false}
            },
            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(2f, 0, 0f) },
                {Rot4.North, new Vector3(-2f, 0, 0f) },
                {Rot4.East, new Vector3(-1f, 0, 0f) },
                {Rot4.West, new Vector3(1f, 0, 0f) }
            },
            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-2f, 0, 0f) },
                {Rot4.North, new Vector3(2f, 0, 0f) },
                {Rot4.East, new Vector3(-1f, 0, 0f) },
                {Rot4.West, new Vector3(1f, 0, 0f) }
            },
            new Dictionary<BodyType, float>()
            {
                {BodyType.male, 0},
                {BodyType.female, 0},
                {BodyType.fat, 1},
                {BodyType.hulk, 0.25f},
                {BodyType.thin, -0.2f}
            }));

            //Grenades

            defaultGroups.Add(new WeaponGroupCordInfo("Grenades",

            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(0.5f, 0, -2.5f) },
                {Rot4.North, new Vector3(-0.5f,0, -2.5f) },
                {Rot4.East, new Vector3(1f, 0, -2.5f) },
                {Rot4.West, new Vector3(-1f, 0, -2.5f)}
            },

            new Dictionary<Rot4, float>()
            {
               {Rot4.South, -20f },
                {Rot4.North, 320f },
                {Rot4.East, 280f },
                {Rot4.West, 30f }
            },

            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, false},
                {Rot4.East, false},
                {Rot4.West, false}
            },

            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-1.5f, 0, -2f) },
                {Rot4.North, new Vector3(1.5f, 0, -2f) },
                {Rot4.East, new Vector3(2f, 0, -0.5f) },
                {Rot4.West, new Vector3(-2f, 0, -0.5f)}
            },

            new Dictionary<Rot4, float>()
            {
               {Rot4.South, 10f },
                {Rot4.North, 320f },
                {Rot4.East, 260f },
                {Rot4.West, 285f }
            },

            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, true},
                {Rot4.East, false},
                {Rot4.West, true}
            },
            new Dictionary<Rot4, float>()
            {
                {Rot4.South, 0.6f},
                {Rot4.North, 0.6f},
                {Rot4.East, 0.6f},
                {Rot4.West, 0.6f}
            },
            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, true},
                {Rot4.North, false},
                {Rot4.East, false},
                {Rot4.West, true}
            },
            new Dictionary<Rot4, bool>()
            {
                {Rot4.South, true},
                {Rot4.North, false},
                {Rot4.East, false},
                {Rot4.West, true}
            },
            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-1f, 0, -0.5f) },
                {Rot4.North, new Vector3(1f, 0, -0.5f) },
                {Rot4.East, new Vector3(1f, 0, 0f) },
                {Rot4.West, new Vector3(-1f, 0, 0f) }
            },
            new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-1.5f, 0, 0f) },
                {Rot4.North, new Vector3(1.5f, 0, 0f) },
                {Rot4.East, new Vector3(1f, 0, 0f) },
                {Rot4.West, new Vector3(-1f, 0, 0f) }
            },
            new Dictionary<BodyType, float>()
            {
                {BodyType.male, 0},
                {BodyType.female, 0},
                {BodyType.fat, 1},
                {BodyType.hulk, 0.5f},
                {BodyType.thin, -0.20f}
            }));

            var doNotDisplayGroup = new WeaponGroupCordInfo("Do not display");
            doNotDisplayGroup.isDisplay = false;

            defaultGroups.Add(doNotDisplayGroup);

            SortWeaponsIntoGroups(ref defaultGroups);

            return defaultGroups;
        }

        public static void SortWeaponsIntoGroups(ref List<WeaponGroupCordInfo> groups)
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
                    groups[6].weapons.Add(thing.defName);
                    continue;
                }

                if (thing.IsRangedWeapon)
                {
                    //Log.Message(thing.defName + "is a ranged weapon!");
                    if (thing.defName.StartsWith("Bow_"))
                    {
                        groups[2].weapons.Add(thing.defName);
                        continue;
                    } 
                    else if (thing.defName.StartsWith("Weapon_Grenade"))
                    {
                        groups[5].weapons.Add(thing.defName);
                        continue;
                    } 
                    else if (thing.uiIconScale > 1.1f)
                    {
                        groups[1].weapons.Add(thing.defName);
                        
                        continue;
                    } 
                    else groups[0].weapons.Add(thing.defName);
                    continue;
                }
                if (thing.IsMeleeWeapon)
                {
                    if (thing.uiIconScale > 1.1f)
                    {
                        groups[4].weapons.Add(thing.defName);
                        continue;
                    } else groups[3].weapons.Add(thing.defName);
                    continue;
                } else groups[6].weapons.Add(thing.defName);
                continue;
            }
        }

        public static void SortWeaponsIntoGroups(ref List<WeaponGroupCordInfo> groups, List<ThingDef> weapons)
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
                        groups[0].weapons.Add(thing.defName);
                        continue;
                    }

                    groups[6].weapons.Add(thing.defName);
                    continue;
                }

                if (thing.IsRangedWeapon)
                {
                    //Log.Message(thing.defName + "is a ranged weapon!");
                    if (thing.defName.StartsWith("Bow_"))
                    {
                        if (groups.Count < 7)
                        {
                            groups[0].weapons.Add(thing.defName);
                            continue;
                        }
                        groups[2].weapons.Add(thing.defName);
                        continue;
                    }
                    else if (thing.defName.StartsWith("Weapon_Grenade"))
                    {
                        if (groups.Count < 7)
                        {
                            groups[0].weapons.Add(thing.defName);
                            continue;
                        }
                        groups[5].weapons.Add(thing.defName);
                        continue;
                    }
                    else if (thing.uiIconScale > 1.1f)
                    {
                        if (groups.Count < 7)
                        {
                            groups[0].weapons.Add(thing.defName);
                            continue;
                        }
                        groups[1].weapons.Add(thing.defName);

                        continue;
                    }
                    else groups[0].weapons.Add(thing.defName);
                    continue;
                }
                if (thing.IsMeleeWeapon)
                {
                    if (thing.uiIconScale > 1.1f)
                    {
                        if (groups.Count < 7)
                        {
                            groups[0].weapons.Add(thing.defName);
                            continue;
                        }
                        groups[4].weapons.Add(thing.defName);
                        continue;
                    }
                    else 
                    {
                        if (groups.Count < 7)
                        {
                            groups[0].weapons.Add(thing.defName);
                            continue;
                        }
                        groups[3].weapons.Add(thing.defName); 
                    }
                    continue;
                }
                else 
                {
                    if (groups.Count < 7)
                    {
                        groups[0].weapons.Add(thing.defName);
                        continue;
                    }
                    groups[6].weapons.Add(thing.defName); 
                    
                }
                continue;
            }
        }
    }
}