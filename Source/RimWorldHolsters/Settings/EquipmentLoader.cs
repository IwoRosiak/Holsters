using Holsters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Settings
{
    public static class EquipmentLoader
    {
        public static List<ThingDef> LoadEquipment()
        {
            return GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(ThingDef)).Cast<ThingDef>().ToList();
        }
        
    }
}