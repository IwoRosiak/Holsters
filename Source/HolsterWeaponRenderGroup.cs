using Holsters;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    public class HolsterWeaponRenderGroup : IExposable
    {
        public List<string> Weapons = new List<string>();

        public HolsterRenderData HolsterRenderData;

        public string Name;

        public bool ShouldDisplay = true;

        public HolsterWeaponRenderGroup() { }

        public HolsterWeaponRenderGroup(string name)
        {
            Name = name;
            HolsterRenderData = new HolsterRenderData();
            HolsterRenderData.FillWithEmptyEntries();
        }

        public bool HasWeapon(string defName) => Weapons.Contains(defName);

        public bool IsEmpty() => HolsterRenderData.Configuration.Count == 0 || HolsterRenderData.SideConfiguration.Count == 0;
        public void ExposeData() 
        {
            Scribe_Values.Look(ref Name, "name");
            Scribe_Values.Look(ref ShouldDisplay, "shouldDisplay", true);

            Scribe_Collections.Look(ref Weapons, "weapons", LookMode.Value);
            Scribe_Deep.Look(ref HolsterRenderData, "renderData");
        }
    }
}
