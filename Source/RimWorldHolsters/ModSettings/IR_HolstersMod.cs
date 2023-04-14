using RimWorld;
using Holsters.Utility.ModSettings.Settings_Drawing;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Holsters
{
    public class IR_HolstersMod : Mod
    {
        private IR_HolstersSettings settings;

        private TabsManager _tabsManager;

        private string instructions = "Guide: \nAll placement settings are group specific, not weapon specific. \nIf you use any sidearm mod you can also edit position for those seperately. \nPositions have to be manually adjusted for each side the pawn is looking at. \nBody offsets are there since some bodies have different dimensions. The position offsets are shared for all bodies but can be modified using impacts (impact 0 means body offsets do not affect this body type.)\n";


        public IR_HolstersMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<IR_HolstersSettings>();
            _tabsManager = new TabsManager();
        }
        public override string SettingsCategory()
        {
            return "Holsters";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            _tabsManager.DrawTabs(inRect);
        }

    }
}