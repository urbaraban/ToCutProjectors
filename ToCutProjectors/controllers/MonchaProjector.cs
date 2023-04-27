using MonchaNETDll.NETLaserDevices;
using StclLibrary.Laser;
using ToCutProjectors.services;

namespace ToCutProjectors.controllers
{
    public class MonchaProjector : IFrameOperator
    {
        public static int TCPBeginnerPort => 9764;
        public static int UDPBeginnerPort => 9765;

        public double HeightResolution
        {
            get => 65533;
            set { }
        }
        public double WidthResolutuon
        {
            get => 65533;
            set { }
        }

        public virtual bool IsConnected { get; } = true;

        public int FPS { get; set; } = 30;

        public DeviceType DeviceType { get; } = DeviceType.MonchaNET;

        private LaserDevice2? LaserDevice { get; }

        private LFrame Frame
        {
            get => _frame;
            set
            {
                _frame = value;
                if (this.LaserDevice != null)
                {
                    SendFrame();
                }
            }
        }
        private LFrame _frame = new LFrame()
        {
            Points = new LPoint[]
                {
                            new LPoint()
                            {
                                x = 0,
                                y = 0,
                                r = 255,
                                multiplier = 100
                            }
                }
        };

        private Task? WhileTask { get; set; }
        public bool IsOn { get; set; }

        public drawing.ProjectorFrame? FrameOperation(drawing.ProjectorFrame modifierFrame)
        {
            LFrame frame = new LFrame();
            this.Frame = frame;
            return modifierFrame;
        }

        private async Task SendFrame()
        {
            if (this.LaserDevice != null
                && this.LaserDevice.LD != null)
            {
                if (LaserDevice.DevType == LaserDevice2.DeviceType.MonchaNET2)
                {
                    uint scan = (uint)(this.Frame.TotalNumberOfPoints * this.FPS);
                    this.LaserDevice.SendFrame(this.Frame.TotalNumberOfPoints, this.Frame, scan);
                }
                else if ((WhileTask == null || WhileTask.Status != TaskStatus.Running) && LaserDevice.LD is MonchaNETDll.NETLaserDevices.Moncha.NET.NLDMonchaNET)
                {
                    WhileTask = While();
                }
            }
        }

        private async Task While()
        {
            await Task.Run(() => {
                while (IsOn == true)
                {
                    if (this.LaserDevice != null)
                    {
                        if (LaserDevice.LD is MonchaNETDll.NETLaserDevices.Moncha.NET.NLDMonchaNET monchaNet)
                        {
                            if (monchaNet.CanSendNextFrame(1000 / this.FPS) == true)
                            {
                                uint scan = (uint)(this.Frame.TotalNumberOfPoints * 1.3 * this.FPS);
                                monchaNet.SendFrame(this.Frame, scan);
                            }
                        }
                    }
                    Task.Delay(1000 / this.FPS);
                }

                this.LaserDevice?.SendFrame(1000, new LFrame(), 10000);
            });
        }
    }
}
