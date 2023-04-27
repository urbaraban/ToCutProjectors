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
    }
}
