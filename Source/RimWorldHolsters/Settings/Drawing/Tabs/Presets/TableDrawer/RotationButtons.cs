using ModSettingsTools;
using ModSettingsTools.Operations;
using UnityEngine;
using Verse;

namespace Holsters.Utility.ModSettings.Settings_Drawing.TableDrawer
{
    internal class RotationButtons : Operation
    {
        private Rot4 _rotation = Rot4.South;

        public RotationButtons(Rect area) : base(area)
        {
        }

        public Rot4 Rotation => _rotation;

        public override void ExecuteOperation()
        {
            DrawDirectionsButtons();
        }

        private void DrawDirectionsButtons()
        {
            ModSettingsTools.Section section = new ModSettingsTools.Section(area, 10, 10);

            Rect west = new Rect(0, 4.5f, 1, 1);
            Rect east = new Rect(9, 4.5f, 1, 1);
            Rect north = new Rect(4.5f , 0, 1, 1);
            Rect south = new Rect(4.5f, 9, 1, 1);

            section.AddOperation(new Button(west, "W", ChangeW));
            section.AddOperation(new Button(east, "E", ChangeE));
            section.AddOperation(new Button(north, "N", ChangeN));
            section.AddOperation(new Button(south, "S", ChangeS));

            section.DrawOperations();
        }

        private void ChangeE()
        {
            _rotation = Rot4.East;
        }
        private void ChangeW()
        {
            _rotation = Rot4.West;
        }
        private void ChangeN()
        {
            _rotation = Rot4.North;
        }
        private void ChangeS()
        {
            _rotation = Rot4.South;
        }
    }
}
