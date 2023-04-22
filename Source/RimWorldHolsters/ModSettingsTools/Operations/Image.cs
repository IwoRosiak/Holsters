using UnityEngine;
using Verse;

namespace ModSettingsTools.Operations
{
    internal sealed class Image : Operation
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
