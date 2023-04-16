using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Holsters.Settings
{
    public static class EquipmentLoader
    {
        public static List<ThingDef> LoadEquipment()
        {
            List<ThingDef> defs = GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(ThingDef)).Cast<ThingDef>().ToList();

            return defs;
        }
    }
}