using Holsters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters.Settings
{
    public static class EquipmentLoader
    {
        public static List<ThingDef> LoadEquipment()
        {
            List<ThingDef> defs = GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(ThingDef)).Cast<ThingDef>().ToList();

            return new List<ThingDef>() { defs[0], defs[1], defs[2], defs[3], defs[4] };
        }
        
    }
}