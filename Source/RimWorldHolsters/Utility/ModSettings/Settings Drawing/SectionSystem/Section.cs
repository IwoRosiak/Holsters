using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing
{
    internal class Section
    {
        private Vector2 _spacePerSegment;

        private Vector2 _start;

        private List<Operation> _operations = new List<Operation>();

        internal Section(Rect sectionArea, Vector2Int divisions, bool ensureMargins, bool ensureMarginsInBetweenElements) //TODO
        {
            _start = sectionArea.position;
            _spacePerSegment = sectionArea.size / divisions;
        }

        internal Section(Rect sectionArea, int divisionsX, int divisionsY)
        {
            _start = sectionArea.position;
            _spacePerSegment = new Vector2(sectionArea.width / divisionsX, sectionArea.height / divisionsY);
        }

        internal void AddOperation(Operation operation)
        {
            _operations.Add(operation);

            Rect sectionArea = operation.SectionArea;

            Rect area = new Rect(_start +  (sectionArea.position * _spacePerSegment), sectionArea.size * _spacePerSegment);

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
