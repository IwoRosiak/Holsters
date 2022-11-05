using UnityEngine;

namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing
{
    internal abstract class Operation
    {
        protected const int buttonHeight = 40;
        protected const int buttonWidth = 180;
        protected const int mediumButtonWidth = 120;
        protected const int smallButtonWidth = 90;
        protected const int tinyButtonWidth = 45;
        protected const int weaponScale = 3;
        protected const int sliderWidth = 16;


        private RectInt _sectionArea;
        private Rect _area;

        internal Operation(RectInt area)
        {
            _sectionArea = area;
        }

        protected Rect area => _area;
        public RectInt SectionArea => _sectionArea;

        public void AllocateSpace(Rect area)
        {
            _area = area;
        }

        public abstract void ExecuteOperation();
    }
}
