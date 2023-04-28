using ToCutProjectors.drawing;
using ToCutProjectors.interfaces;

namespace ToCutProjectors.graphs
{
    public class PathOptimization : IFrameOperator
    {
        public bool IsOn { get; set; } = true;
        public uint FindDepth { get; set; } = 1;

        public event EventHandler<bool>? StatusChanged;

        public ProjectorFrame? FrameOperation(ProjectorFrame modifierFrame)
        {
            throw new NotImplementedException();
        }
    }
}
