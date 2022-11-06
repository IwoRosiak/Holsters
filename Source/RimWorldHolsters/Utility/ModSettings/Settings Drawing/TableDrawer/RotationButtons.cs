using RimWorldHolsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing.TableDrawer
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
            Section section = new Section(area, 10, 10);

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
            Log.Message("luj");
            _rotation = Rot4.East;

            Log.Message(_rotation.ToStringWord());
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
