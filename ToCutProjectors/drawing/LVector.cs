using System.Numerics;
using ToCutProjectors.services;

namespace ToCutProjectors.drawing
{
    public struct LVector
    {
        public LPoint P1 { get; set; }
        public float Length { get; set; }
        public Vector3 Vector { get; set; }
        public bool IsBlank { get; set; } = false;
        public LPoint P2
        {
            get
            {
                if (float.IsNaN(this.Vector.X) == false && float.IsNaN(this.Vector.Y) && float.IsNaN(this.Vector.Z))
                {
                    return GetLenthPoint(this.Length);
                }
                return P1;
            }
        }

        public bool IsNaN => this.P1.IsNaN;

        public LVector(LPoint P1, Vector3 V, float Lenth, bool IsBlank)
        {
            this.P1 = P1;
            this.Vector = V;
            this.Length = Lenth;
            this.IsBlank = IsBlank;
        }


        public LPoint GetLenthPoint(float lenth)
        {
            return new LPoint(
                this.P1.X + this.Vector.X * lenth,
                this.P1.Y + this.Vector.Y * lenth,
                this.P1.Z + this.Vector.Z * lenth);
        }
    }
}
