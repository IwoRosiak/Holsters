using RimWorld;
using RimWorldHolsters.Core;
using RimWorldHolsters.Core.WeaponDrawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimWorldHolsters
{
    public class HolstersComp : ThingComp
    {

        public HolstersCompProperties Props
        {
            get
            {
                return (HolstersCompProperties)this.props;
            }
        }

        public Pawn pawn
        {
            get
            {
                return (Pawn)this.parent;
            }
        }

        public override void PostDraw()
        {
            base.PostDraw();

            WeaponDrawingManager.DrawWeaponFor(pawn.Drawer.renderer, pawn.DrawPos, pawn.Rotation, pawn);
        }

       
    }
}
