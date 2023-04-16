using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Holsters.Settings
{
    public static class EquipmentLoader
    {
        public static List<ThingDef> LoadedAndUsedDefs { get; private set; }

        public static List<ThingDef> LoadEquipment()
        {
            LoadedAndUsedDefs = new List<ThingDef>();
            List<ThingDef> defs = GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(ThingDef)).Cast<ThingDef>().ToList();

            return defs;
        }
        public static void SaveAsLoades(ThingDef def)
        {
            LoadedAndUsedDefs.Add(def); 
        }
        
    }
}