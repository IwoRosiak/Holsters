using RimWorld;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Verse;

namespace RimWorldHolsters.Core.RenderNodes
{
    internal class DynamicPawnRenderNodeSetup_Holsters : DynamicPawnRenderNodeSetup
    {
        public override bool HumanlikeOnly => true;

        public override IEnumerable<(PawnRenderNode node, PawnRenderNode parent)> GetDynamicNodes(Pawn pawn, PawnRenderTree tree) 
        {
            if (pawn.equipment == null || pawn.equipment.AllEquipmentListForReading.Count == 0)
                yield break;
         
            Log.Message("-----Generating Dynamic Nodes-----");


            var layerOffsets = new Dictionary<PawnRenderNode, int>();
            PawnRenderNode holsterNode = tree.TryGetNodeByTag(PawnRenderNodeTagDefOf_Holsters.Holster,  out PawnRenderNode node) ? node : null;


            if (pawn.equipment.Primary != null && ShouldAddHolsterNode(pawn.equipment.Primary))
            {
                foreach ((PawnRenderNode node, PawnRenderNode parent) result in ProcessWeapons(pawn, tree, pawn.equipment.Primary, holsterNode, layerOffsets))
                {
                    if (result.node != null)
                        yield return result;

                    Log.Message("Adding main weapon");
                }
            }
            Log.Message("Things in inventory: " + pawn.inventory.innerContainer.Count);

            foreach (ThingWithComps item in pawn.inventory.innerContainer)
            {
                if (!ShouldAddHolsterNode(item))
                    continue;

                Log.Message("Processing: " + item.def.defName);

                foreach ((PawnRenderNode node, PawnRenderNode parent) result in ProcessWeapons(pawn, tree, item, holsterNode, layerOffsets))
                {
                    if (result.node != null)
                        yield return result;

                    Log.Message("Adding secondary weapon");

                    //if (result.parent != null && !layerOffsets.TryAdd(result.parent, 1))
                    //{
                    //    layerOffsets[result.parent]++;
                    //}
                }
            }

            Log.Message("-----Ending Generating Dynamic Nodes-----");
            Log.Message(".");
        }

        private static bool ShouldAddHolsterNode(ThingWithComps gear) => gear.def.IsWeapon;

        private static IEnumerable<(PawnRenderNode node, PawnRenderNode parent)> ProcessWeapons(Pawn pawn, PawnRenderTree tree, ThingWithComps item, PawnRenderNode parentNode, Dictionary<PawnRenderNode, int> layerOffsets)
        {
            PawnRenderNodeProperties pawnRenderNodeProperties = null;
            PawnRenderNode pawnRenderNode2 = null;
            //DrawData drawData = item.def.apparel.drawData;
            if (parentNode != null)
            {
                if (pawnRenderNode2 == null)
                {
                    pawnRenderNode2 = parentNode;
                }
                pawnRenderNodeProperties = new PawnRenderNodeProperties
                {
                    debugLabel = item.def.defName,
                    workerClass = typeof(PawnRenderNodeWorker_Holsters),
                    baseLayer = pawnRenderNode2.Props.baseLayer,
                    //drawData = drawData,
                    parentTagDef = PawnRenderNodeTagDefOf_Holsters.Holster
                };
            }

            if (tree.ShouldAddNodeToTree(pawnRenderNodeProperties))
            {
                yield return (node: new PawnRenderNode_Holsters(pawn, pawnRenderNodeProperties, tree, item), parent: pawnRenderNode2);
            }
            else
            {
                yield return (node: null, parent: pawnRenderNode2);
            }
        }
    }
}
