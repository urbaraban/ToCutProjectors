using ToCutProjectors.drawing;

namespace ToCutProjectors.services
{
    public interface IDrawingTransform
    {
        public LPoint Transform(LPoint point);
    }
}
