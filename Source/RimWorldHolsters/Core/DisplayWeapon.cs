using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Core
{
    public static class IR_DisplayWeapon
    {
        public static void DrawEquipmentHolstered(WeaponDrawingProperties properties)
        {
            Mesh mesh = properties.Mesh;

            Material material = ResolveMaterial(properties.Item);
            Matrix4x4 matrix = AssembleDrawMatrix(properties);

            Graphics.DrawMesh(mesh, matrix, material, 0);
        }

        private static Matrix4x4 AssembleDrawMatrix(WeaponDrawingProperties properties)
        {
            Vector3 drawLoc = properties.Location;

            Quaternion rotation = Quaternion.AngleAxis(properties.Rotation, Vector3.up);

            float UIIconScale = properties.Item.def.uiIconScale;
            Vector3 scale = Vector3.one / UIIconScale * properties.Size;

            return Matrix4x4.TRS(drawLoc, rotation, scale);
        }

        private static Material ResolveMaterial(ThingWithComps item)
        {
            return item.Graphic.MatSingleFor(item);
        }
    }
}