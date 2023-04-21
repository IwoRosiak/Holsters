using SettingsDrawer.Sections;
using UnityEngine;
using Verse;

namespace Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie.Operations
{
    internal class Image : Operation
    {
        private Texture _texture;

        public Image(Rect area, Texture texture) : base(area)
        {
            _texture = texture; 
        }

        public override void ExecuteOperation()
        {
            Widgets.DrawTextureFitted(area, _texture, 1);
        }
    }
}
