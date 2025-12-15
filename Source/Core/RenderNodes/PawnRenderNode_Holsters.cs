using Holsters;
using System.Collections.Generic;
using Verse;

namespace RimWorldHolsters.Core.RenderNodes
{
    public class PawnRenderNode_Holsters : PawnRenderNode
    {
        public PawnRenderNode_Holsters(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree, ThingWithComps thing, bool drawInAlternativePosition = false, bool treatAsMainWeapon = false) : base(pawn, props, tree)
        {
            Thing = thing;
            DrawInAlternativePosition = drawInAlternativePosition;
            TreatAsMainWeapon = treatAsMainWeapon;

            RenderData = IR_HolstersSettings.GetWeaponGroupOf(thing.def.defName).HolsterRenderData;
        }

        public ThingWithComps Thing { get; }
        public HolsterRenderData RenderData { get; }

        public bool DrawInAlternativePosition { get; }
        public bool TreatAsMainWeapon { get; }

        public override GraphicMeshSet MeshSetFor(Pawn pawn) => new GraphicMeshSet(MeshPool.GridPlane(this.props.overrideMeshSize ?? this.props.drawSize));
        
        public HolsterConfiguration GetRenderData(Rot4 facing)
        {
            Dictionary<Rot4, HolsterConfiguration> configurations = DrawInAlternativePosition 
                ? RenderData.SideConfiguration : RenderData.Configuration;

            return configurations[facing];
        }

        public override bool FlipGraphic(PawnDrawParms parms) => GetRenderData(parms.facing).IsFlipped;

        protected override IEnumerable<Graphic> GraphicsFor(Pawn pawn)
        {
            if (Thing == null)
                yield break;

            yield return Thing.Graphic;
        }
    }
}
