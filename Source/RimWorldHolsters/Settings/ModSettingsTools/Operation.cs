using UnityEngine;

namespace SettingsDrawer.Sections
{
    public abstract class Operation
    {
        protected const int BUTTON_HEIGHT = 40;
        protected const int BUTTON_WIDTH = 180;
        protected const int MEDIUM_BUTTON_WIDTH = 120;
        protected const int SMALL_BUTTON_WIDTH = 90;
        protected const int TINY_BUTTON_WIDTH = 45;
        protected const int WEAPON_SCALE = 3;
        protected const int SLIDER_WIDTH = 16;


        private Rect _sectionArea;
        private Rect _area;

        public Operation(Rect area)
        {
            _sectionArea = area;
        }

        protected Rect area => _area;
        public Rect SectionArea => _sectionArea;

        public void AllocateSpace(Rect area)
        {
            _area = area;

            Initialise();
        }

        public virtual void Initialise() { }

        public abstract void ExecuteOperation();
    }
}
