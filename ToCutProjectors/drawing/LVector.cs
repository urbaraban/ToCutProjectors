using System.Numerics;
using ToCutProjectors.interfaces;

namespace ToCutProjectors.drawing
{
    public struct LVector : IGraphElement
    {
        public LPoint P1 { get; set; }
        public float Length { get; set; }
        public Vector3 Vector { get; set; }
        public bool IsBlank { get; set; } = false;
        public LPoint P2
        {
            get
            {
                if (this.IsNaN == false)
                {
                    return GetLenthPoint(this.Length);
                }
                return P1;
            }
        }

        public bool IsNaN => this.P1.IsNaN == true 
            || float.IsNaN(this.Vector.X) == true
            || float.IsNaN(this.Vector.Y) == true
            || float.IsNaN(this.Vector.Z) == true;

        public LVector(LPoint P1, Vector3 V, float Lenth, bool isBlank)
        {
            this.P1 = P1;
            this.Vector = V;
            this.Length = Lenth;
            this.IsBlank = isBlank;
        }

        public LVector(LPoint p1, LPoint p2, bool isBlank)
        {
            this.P1 = p1;
            this.Vector = Vector3.Normalize(new Vector3((float)(p2.X - p1.X), (float)(p2.Y - p1.Y), (float)(p2.Z - p1.Z)));
            this.Length = this.P1.GetLenth3D(p2);
            this.IsBlank = isBlank;
        }

        public LPoint GetLenthPoint(float lenth)
        {
            return new LPoint(
                this.P1.X + this.Vector.X * lenth,
                this.P1.Y + this.Vector.Y * lenth,
                this.P1.Z + this.Vector.Z * lenth);
        }

        IGraphElement IGraphElement.Reverse()
        {
            return new LVector()
            {
                P1 = this.P2,
                Vector = Vector3.Negate(this.Vector),
                Length = this.Length,
                IsBlank = this.IsBlank
            };
        }
    }
}
