namespace ToCutProjectors.drawing
{
    public struct LPoint
    {
        public bool IsNaN => double.IsNaN(this.X) && double.IsNaN(this.Y);

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public byte T { get; set; }

        public LPoint(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.T = 1;
        }

        public new string ToString() =>
            $"{Math.Round(X, 8)} : " +
            $"{Math.Round(Y, 8)} : " +
            $"{Math.Round(Z, 8)} " +
            $"T:{T}";

        public float GetLenth2D(LPoint point)
        {
            return LPoint.GetLenth2D(this.X, this.Y, point.X, point.Y);
        }

        public float GetLenth3D(LPoint point)
        {
            return GetLenth3D(this.X, this.Y, this.Z, point.X, point.Y, point.Z);
        }

        public static float GetLenth2D(float X1, float Y1, float X2, float Y2)
        {
            float lenth = (float)Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
            return lenth;
        }

        public static float GetLenth3D(double X1, double Y1, double Z1, double X2, double Y2, double Z2)
        {
            return (float)Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2));
        }
    }
}
