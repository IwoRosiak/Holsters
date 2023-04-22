using ModSettingsTools;
using UnityEngine;
using Verse;

namespace Holsters.Settings
{
    public sealed class IR_HolstersMod : Mod
    {
        private IR_HolstersSettings _settings;

        private TabsManager _tabsManager;

        //private string instructions = "Guide: \nAll placement settings are group specific, not weapon specific. \nIf you use any sidearm mod you can also edit position for those seperately. \nPositions have to be manually adjusted for each side the pawn is looking at. \nBody offsets are there since some bodies have different dimensions. The position offsets are shared for all bodies but can be modified using impacts (impact 0 means body offsets do not affect this body type.)\n";


        public IR_HolstersMod(ModContentPack content) : base(content)
        {
            _settings = GetSettings<IR_HolstersSettings>();
            _tabsManager = new TabsManager(); // TODO: add here DI
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