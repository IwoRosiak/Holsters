using ModSettingsTools;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters.Settings.Drawing.Equipment.Operations
{
    internal sealed class SelectedEquipmentDisplayOperation : Operation
    {
        public SelectedEquipmentDisplayOperation(Rect area) : base(area)
        {
        }

        public override void ExecuteOperation()
        {
            DisplaySelectedEquipment();
        }

        private void DisplaySelectedEquipment()
        {
            List<Rect> parts = SplitToParts(SelectedEquipmentTracker.SelectedEquipment.Count);

            foreach (ThingDef def in SelectedEquipmentTracker.SelectedEquipment)
            {
                int index = SelectedEquipmentTracker.SelectedEquipment.IndexOf(def);

                Rect belongingRect = parts[index];

                DisplayEquipment(def, belongingRect);
            }
        }

        private List<Rect> SplitToParts(int numOfParts)
        {
            int root = Mathf.CeilToInt(Mathf.Sqrt(numOfParts));

            float oneSegmentSize = area.height/root;
            Vector2 size = new Vector2(oneSegmentSize, oneSegmentSize);

            Vector2 zeroPos = area.position;

            List<Rect> parts = new List<Rect>();

            for (int i = 0; i < root; i++) 
            {
                for (int j = 0; j < root; j++)
                {
                    Vector2 pos = new Vector2(j * oneSegmentSize, i * oneSegmentSize) + zeroPos;

                    parts.Add(new Rect(pos, size));
                }
            }

            return parts;
        }

        private void DisplayEquipment(ThingDef def, Rect area)
        {
            Texture text = def.graphic.MatNorth.mainTexture;

            Vector2 center = area.center;

            Rect displayRect = new Rect(center.x - (area.width / 2), center.y - (area.height / 2), area.width, area.height);

            Widgets.DrawTextureRotated(displayRect, text, -45);
        }
    }
}