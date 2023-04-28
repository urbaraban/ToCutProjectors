using System.Numerics;
using ToCutProjectors;
using ToCutProjectors.callibrations;
using ToCutProjectors.drawing;

namespace ToCutProjectorsTest
{
    public class UnitTest1
    {
        private ProjectorFrame GetRecFrame(float margin = 0)
        {
            return new ProjectorFrame()
            {
                new LVectorCollection()
                {
                    new LVector(new LPoint(margin, margin, 0), new Vector3(1, 0, 0), 1 - margin * 2, false),
                    new LVector(new LPoint(1 - margin, margin, 0), new Vector3(0, 1, 0), 1 - margin * 2, false),
                    new LVector(new LPoint(1 - margin, 1 - margin, 0), new Vector3(-1, 0, 0), 1 - margin * 2, false),
                    new LVector(new LPoint(margin, 1 - margin, 0), new Vector3(0, -1, 0), 1 - margin * 2, false)
                }
            };
        }

        [Fact]
        public void RectTest()
        {
            ProjectorFrame drawingObjects = GetRecFrame(0.25f);
            AxisCorrectTransform axisCorrectTransform = new AxisCorrectTransform();
            for (int i = 0; i < axisCorrectTransform.XAxisCorrect.Count; i += 1)
            {
                axisCorrectTransform.XAxisCorrect[i] *= 0.5f;
            }

            LProjector projector = new LProjector()
            {
                axisCorrectTransform
            };

            ProjectorFrame after = projector.FrameOperation(drawingObjects);
        }
    }
}