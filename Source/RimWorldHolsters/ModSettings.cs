using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    public class IR_HolsterMod : Mod
    {
        private IR_HolsterSettings settings;

        public IR_HolsterMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<IR_HolsterSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            //Widgets.DrawTextureFitted(new Rect(inRect.x, inRect.y, inRect.width, inRect.height), Textures.SettingsBackGround, 1);

            Rect masterRect = new Rect(inRect.x + (0.1f * inRect.width), inRect.y + 40, 0.8f * inRect.width, 936);

            Listing_Standard listingTop = new Listing_Standard();
            Rect TopSettings = new Rect(masterRect.x, masterRect.y, masterRect.width, 45);

            listingTop.Begin(TopSettings); //column 1
            listingTop.ColumnWidth = TopSettings.width / 3.2f;

            if (listingTop.ButtonText("Mod active: " + IR_HolsterSettings.isHolstersActive.ToString()))
            {
                IR_HolsterSettings.isHolstersActive = !IR_HolsterSettings.isHolstersActive;
            }

            Rect MidSettings = new Rect(TopSettings.x, TopSettings.y + listingTop.CurHeight, masterRect.width, 180f);

            listingTop.NewColumn(); // column 2
            if (IR_HolsterSettings.isHolstersActive)
            {
                if (listingTop.ButtonText("Default settings: " + IR_HolsterSettings.isDefaultSettings.ToString()))
                {
                    IR_HolsterSettings.isDefaultSettings = !IR_HolsterSettings.isDefaultSettings;
                }
            }

            listingTop.NewColumn(); //column 3
            if (listingTop.ButtonText("Reset settings", "Set now"))
            {
                //ResetValues();
            }

            listingTop.End();

            Listing_Standard listingMid = new Listing_Standard();
            if (IR_HolsterSettings.isHolstersActive && !IR_HolsterSettings.isDefaultSettings)
            {
                listingMid.Begin(MidSettings);

                listingMid.GapLine();

                //Widgets.CheckboxMulti(MidSettings, MultiCheckboxState.On)

                //Rect TableSettings = new Rect(masterRect.x, MidSettings.y + listingMid.CurHeight, masterRect.width, inRect.height);
                //TableSettings.height = inRect.height - TableSettings.y;

                DrawBodies(new Rect(inRect.x, TopSettings.y, masterRect.width, 180f));
                listingMid.GapLine();

                listingMid.End();

                //Rect TagTable = new Rect(TableSettings.x, TableSettings.y, TableSettings.width - 10f, columnHeight * CountDisplayableTags());
                //Widgets.BeginScrollView(TableSettings, ref scrollPos, TagTable);

                //Widgets.EndScrollView();
            }
        }

        private void DrawBodies(Rect rect)
        {
            var quartOfRect = rect.width / 4;

            for (int i = 0; i < 4; i++)
            {
                DrawBody(new Rect(rect.x + (quartOfRect * i), rect.y, quartOfRect, quartOfRect));
            }
        }

        private void DrawBody(Rect rect)
        {
            Widgets.DrawBox(rect);
        }

        public override string SettingsCategory()
        {
            return "Rimmersive Workshop: Holsters";
        }

        private static Vector2 scrollPos = Vector2.zero;
    }
}