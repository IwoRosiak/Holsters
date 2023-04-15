using SettingsDrawer.Sections;
using UnityEngine;
using Verse;

namespace Holsters.Utility.ModSettings.Settings_Drawing.TableDrawer
{
    internal class WeaponDrawer : Operation
    {
        private ThingDef _weaponDef;
        private Rot4 _rotation;


        private HolsterPreset _tempConfig;

        public WeaponDrawer(Rect area, Rot4 rotation,ThingDef weaponDef, HolsterPreset config) : base(area)
        {
            _rotation = rotation;
            _weaponDef = weaponDef;
            _tempConfig = config;
        }

        public override void ExecuteOperation()
        {
            HolsterConfiguration config = _tempConfig.Configuration[_rotation];

            Vector2 offset = new Vector2(config.Position.x, -config.Position.z)/10;

            Texture text = _weaponDef.graphic.MatNorth.mainTexture;
            float scale = (((1 / _weaponDef.uiIconScale) / (text.width / 64)) * 1.35f * _weaponDef.graphic.drawSize.x) * config.Size;

            //Widgets.DrawTextureRotated(rect.center + (offset * pixelRatio), text, IR_WeaponData.GetWeaponAngle(GetCurGroup()sIndex, currentDir), scale);

            Vector2 center = area.center + (offset * ModSettingsUtilities.PixelRatio);
            float num = (float)text.width * scale;
            float num2 = (float)text.height * scale;
            if (config.IsFlipped)
            {
                num2 = (float)text.height * -scale;
            }

            float angle = config.Rotation;

            Widgets.DrawTextureRotated(new Rect(center.x - num / 2f, center.y - num2 / 2f, num, num2), text, angle);

        }
    }
}
