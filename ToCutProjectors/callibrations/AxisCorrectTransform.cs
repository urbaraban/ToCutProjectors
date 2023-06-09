﻿using ToCutProjectors.drawing;
using ToCutProjectors.interfaces;

namespace ToCutProjectors.callibrations
{
    public class AxisCorrectTransform : IFrameOperator
    {
        public bool IsOn { get; set; } = true;
        public bool Status { get; set; } = true;
        public List<float> XAxisCorrect { get; } = new List<float>() { 0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f };
        public List<float> YAxisCorrect { get; } = new List<float>() { 0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f };

        public event EventHandler<bool>? StatusChanged;

        public ProjectorFrame? FrameOperation(ProjectorFrame modifierFrame)
        {
            ProjectorFrame result = new ProjectorFrame();
            foreach (IDrawingObject drawingObject in modifierFrame)
            {
                if (drawingObject is LVectorCollection vcollection)
                {
                    result.Add(Transform(vcollection));
                }
                else
                {
                    result.Add(drawingObject);
                }

            }
            return result;
        }

        private LVectorCollection Transform(LVectorCollection drawingObjects)
        {
            LVectorCollection result = new LVectorCollection();
            foreach (LVector vector in drawingObjects)
            {
                result.Add(Transform(vector));
            }
            return result;
        }

        private LVector Transform(LVector vector)
        {
            LPoint p1 = Transform(vector.P1);
            LPoint p2 = Transform(vector.P2);
            return new LVector(
                p1,
                p2,
                vector.IsBlank);
        }

        private LPoint Transform(LPoint point)
        {
            point.X = GetAxisCorrect(point.X, XAxisCorrect);
            point.Y = GetAxisCorrect(point.Y, YAxisCorrect);
            return point;
        }


        private static float GetAxisCorrect(float value, List<float> axisCorrect)
        {
            float step = 1f / (axisCorrect.Count - 1);
            int index = (int)(value / step);

            float rvalue = value - step * index;
            float percent = rvalue / step;
            float between = axisCorrect[index + 1] - axisCorrect[index];
            value = axisCorrect[index] + percent * between;

            return value;
        }
    }
}
