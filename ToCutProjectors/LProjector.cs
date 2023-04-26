using System.Net;

namespace ToCutProjectors
{
    public abstract class LProjector
    {
        public virtual IPAddress IPAddress { get; set; } = new IPAddress(new byte[] { 127, 0, 0, 1 });

        public virtual bool IsConnected { get; } = true;

        public virtual bool IsOn { get; set; } = true;

        public virtual double HeightResolution { get; set; } = 1000;
        public virtual double WidthResolutuon { get; set; } = 1000;

    }

    public enum DeviceType : int
    {
        Virtual = 0,
        MonchaNET = 1,
        VLT = 2,
        Pangolin = 3,
    }
}
