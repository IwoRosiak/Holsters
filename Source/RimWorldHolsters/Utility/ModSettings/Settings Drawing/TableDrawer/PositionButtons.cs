using RimWorldHolsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie.Operations;
using UnityEngine;


namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing.TableDrawer
{
    internal class PositionButtons : Operation
    {
        private Vector2 _modif;

        public PositionButtons(Rect area) : base(area)
        {
        }

        public override void ExecuteOperation()
        {
            _modif = default(Vector2);

            Section section = new Section(area, 10, 10);

            Rect west = new Rect(1, 4.5f, 1, 1);
            Rect east = new Rect(8, 4.5f, 1, 1);
            Rect north = new Rect(4.5f, 1, 1, 1);
            Rect south = new Rect(4.5f, 8, 1, 1);

            float currentIncrement = 0.05f;

            section.AddOperation(new Button(west, "-X", () => AddPosition(new Vector2(-currentIncrement, 0))));
            section.AddOperation(new Button(east, "+X", () => AddPosition(new Vector2(+currentIncrement, 0))));
            section.AddOperation(new Button(north, "+Y", () => AddPosition(new Vector2(0, currentIncrement))));
            section.AddOperation(new Button(south, "-Y", () => AddPosition(new Vector2(0, -currentIncrement))));

            section.DrawOperations();
        }


        public Vector2 ApplyProperty()
        {
            return _modif;
        }

        private void AddPosition(Vector2 position)
        {
            _modif = position;
        }
    }
}
