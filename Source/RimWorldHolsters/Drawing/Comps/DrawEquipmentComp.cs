using Holsters.Drawing;
using Verse;

namespace Holsters.Drawing.Comps
{
    public sealed class DrawEquipmentComp : ThingComp
    {
        public DrawEquipmentCompProperties Props => (DrawEquipmentCompProperties)props;

        public override void PostDraw()
        {
            base.PostDraw();

            DrawDecider.DrawWeaponsFor((Pawn)parent);
        }
    }
}
