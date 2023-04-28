using ToCutProjectors.drawing;

namespace ToCutProjectors.interfaces
{
    internal interface IGraphElement
    {
        public LPoint P1 { get; }

        public LPoint P2 { get; }

        public float Length { get; }

        public IGraphElement Reverse();
    }
}
