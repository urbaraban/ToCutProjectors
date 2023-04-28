using System.Numerics;
using ToCutProjectors.drawing;
using ToCutProjectors.interfaces;

namespace ToCutProjectors.graphs
{
    internal class GraphConnection
    {
        public string ID { get; }
        public bool OutReverse { get; }
        public double Distance { get; }

        public IGraphElement Element { get; }
        public LPoint P1 { get; }
        public LPoint P2 { get; }
        public double Lenth => Element.Length;

        public Vector2 Vector { get; }

        public double GetEvaluation(Vector2 vector2)
        {
            float dot = Vector2.Dot(this.Vector, vector2) + 1.001f;
            double invertDot = Math.Pow(2 / dot, 2);
            return this.Distance * invertDot;
        }

        public GraphConnection(string ID, LPoint P1, LPoint P2, IGraphElement Element, double distance, Vector2 vector, bool Reverse)
        {
            this.ID = ID;
            this.P1 = P1;
            this.P2 = P2;
            this.Distance = distance;
            this.Element = Element;
            this.Vector = Vector2.Normalize(vector);
            this.OutReverse = Reverse;
        }

        public GraphConnection GetNearGraphConnection(LPoint point)
        {
            float _lenth1 = point.GetLenth2D(this.P1);
            float _lenth2 = point.GetLenth2D(this.P2);

            double lenth = Math.Min(_lenth1, _lenth2);

            LPoint P1 = _lenth2 < _lenth1 ? this.P2 : this.P1;
            LPoint P2 = _lenth2 < _lenth1 ? this.P1 : this.P2;

            Vector2 outVector = new Vector2(point.X - P1.X, point.Y - P1.Y);

            GraphConnection graphConnection =
                new GraphConnection(this.ID, P1, P2, this.Element,
                lenth, outVector, _lenth2 < _lenth1);

            return graphConnection;
        }

        public new string ToString() => $"{this.ID} : {this.OutReverse} : {Distance}";

    }
}
