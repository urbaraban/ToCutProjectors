using ToCutProjectors.drawing;
using ToCutProjectors.services;

namespace ToCutProjectors.transforming
{
    public class AxisCorrectTransform : IFrameOperator
    {
        public List<float> XAxisCorrect { get; } = new List<float>() { 0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f };
        public List<float> YAxisCorrect { get; } = new List<float>() { 0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f };

        public LPoint Transform(LPoint point)
        {
            point.X = GetAxisCorrect(point.X, XAxisCorrect);
            point.Y = GetAxisCorrect(point.Y, YAxisCorrect);
            return point;
        }



        public LVectorCollection Transform(LVectorCollection objects)
        {
            throw new NotImplementedException();
        }

        private static float GetAxisCorrect(float value, List<float> axisCorrect)
        {
            if (value >= axisCorrect[0] && value < axisCorrect.Last())
            {
                float step = 1f / (axisCorrect.Count - 1);
                int index = (int)(value / step);

                float rvalue = value - step * index;
                float percent = rvalue / step;
                float between = axisCorrect[index + 1] - axisCorrect[index];
                value = axisCorrect[index] + percent * between;
            }
            return value;
        }

    }
}
