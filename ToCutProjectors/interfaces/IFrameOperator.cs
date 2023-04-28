using ToCutProjectors.drawing;

namespace ToCutProjectors.interfaces
{
    public interface IFrameOperator
    {
        public event EventHandler<bool>? StatusChanged;
        public bool IsOn { get; set; }
        public ProjectorFrame? FrameOperation(ProjectorFrame modifierFrame);
    }
}
