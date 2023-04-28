using ToCutProjectors.drawing;
using ToCutProjectors.interfaces;

namespace ToCutProjectors.modifiers
{
    internal class SolidFrame : IFrameOperator
    {
        public event EventHandler<bool>? StatusChanged;

        public bool IsOn { get; set; } = true;

        public ProjectorFrame? FrameOperation(ProjectorFrame modifierFrame)
        {
            LVectorCollection union = UnionFrame(modifierFrame);
            return new ProjectorFrame() { AddBridge(union) };

        }

        private LVectorCollection UnionFrame(ProjectorFrame drawingObjects)
        {
            LVectorCollection result = new LVectorCollection();
            foreach (IDrawingObject drawingObject in drawingObjects)
            {
                if (drawingObject is LVectorCollection vectors)
                {
                    result.AddRange(vectors);
                }
            }
            return result;
        }

        private LVectorCollection AddBridge(LVectorCollection vectors)
        {
            LVectorCollection result = new LVectorCollection();
            for (int j = 0; j < vectors.Count; j += 1)
            {
                LVector firstLine = vectors[j];
                LVector secondLine = vectors[(j + 1) % vectors.Count];
                if (firstLine.IsBlank == false && secondLine.IsBlank == false)
                {
                    double BridgeLenth = LPoint.GetLenth2D(firstLine.P2.X, firstLine.P2.Y, secondLine.P1.X, secondLine.P1.Y);
                    if (BridgeLenth > 0.0001)
                    {
                        vectors.Insert(j + 1, new LVector(firstLine.P2, secondLine.P1, true));
                        j += 1;
                        //IEnumerable<VectorLine> bridge = GetBridgeLines(firstLine, secondLine, device.ProjectionSetting.StartTail);
                        //lines.InsertRange(j + 1, bridge);
                        //j += bridge.Count() - 1;
                    }
                }
            }
            return result;
        }
    }
}
