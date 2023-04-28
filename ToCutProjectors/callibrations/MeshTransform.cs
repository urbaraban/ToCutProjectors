using ToCutProjectors.drawing;
using ToCutProjectors.interfaces;

namespace ToCutProjectors.callibrations
{
    public class MeshTransform : IFrameOperator
    {
        public bool IsOn { get; set; }

        public event EventHandler<bool>? StatusChanged;

        public ProjectorFrame? FrameOperation(ProjectorFrame modifierFrame)
        {
            throw new NotImplementedException();
        }
    }
}
