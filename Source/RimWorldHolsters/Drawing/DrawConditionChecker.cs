﻿using Holsters.Settings;
using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace Holsters.Drawing
{
    internal static class DrawConditionChecker
    {
        internal static bool ShouldDraw(Pawn pawn)
        {
            if (pawn.Dead)
                return false;

            if (!IR_HolstersSettings.DisplayIndoors && pawn.GetRoom()?.ProperRoom == true)
                return false;

            if (pawn.GetPosture() != PawnPosture.Standing)
                return false;

            if (pawn.equipment?.Primary == null)
                return false;

            if (IsNaked(pawn))
                return false;

            return true;
        }

        private static bool IsNaked(Pawn pawn)
        {
            bool isNotDrawingAnyClothes = pawn.Drawer.renderer.graphics.apparelGraphics.NullOrEmpty();

            return isNotDrawingAnyClothes;
        }
    }
}
