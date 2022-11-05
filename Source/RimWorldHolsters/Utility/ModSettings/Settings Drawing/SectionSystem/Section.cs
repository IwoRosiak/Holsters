using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing
{
    internal class Section
    {
        private Vector2 _spacePerSegment;

        private List<Operation> _operations = new List<Operation>();

        internal Section(Rect sectionArea, int divisionsX, int divisionsY)
        {
            _spacePerSegment = new Vector2(sectionArea.width / divisionsX, sectionArea.height / divisionsY);
        }

        internal void AddOperation(Operation operation)
        {
            _operations.Add(operation);

            RectInt sectionArea = operation.SectionArea;

            Rect area = new Rect(sectionArea.position * _spacePerSegment, sectionArea.size * _spacePerSegment);

            operation.AllocateSpace(area);
        }

        internal void DrawOperations()
        {
            foreach(Operation operation in _operations)
            {
                operation.ExecuteOperation();
            }
        }
    }
}
