using System.Collections.ObjectModel;
using System.Net;
using ToCutProjectors.controllers;
using ToCutProjectors.drawing;
using ToCutProjectors.interfaces;

namespace ToCutProjectors
{
    public class LProjector : Collection<IFrameOperator>, IFrameOperator
    {
        public event EventHandler<bool>? StatusChanged;

        public bool IsOn { get; set; } = true;
        private ProjectorFrame? Frame { get; set; }

        private CancellationTokenSource cancellation = new CancellationTokenSource();

        public ProjectorFrame? FrameOperation(ProjectorFrame modifierFrame)
        {
            this.Frame = modifierFrame;
            RenderFrame(modifierFrame);
            return modifierFrame;
        }

        private async void RenderFrame(ProjectorFrame? frame)
        {
            cancellation.Cancel();
            cancellation = new CancellationTokenSource();
            CancellationToken ct = cancellation.Token;
            ProjectorFrame? result = this.Frame;

            if (result != null)
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < this.Count && result != null && ct.IsCancellationRequested == false; i += 1)
                    {
                        if (this[i].IsOn == true)
                        {
                            result = this[i].FrameOperation(result);
                        }
                    }
                }, ct);
            }
        }
    }

    public static class DevicesMg
    {
        public static async Task<BaseProjector> GetDeviceAsync(IPAddress iP, DeviceType deviceType, int NumberDevice)
        {
            switch (deviceType)
            {
                case DeviceType.MonchaNET:
                    MonchaProjector monchaprojector = new MonchaProjector(iP, NumberDevice);
                    return monchaprojector;
                case DeviceType.VLT:
                    break;
                case DeviceType.Pangolin:
                    break;
            }
            return new VirtualProjector();
        }
    }

    public enum DeviceType : int
    {
        Virtual = 0,
        MonchaNET = 1,
        VLT = 2,
        Pangolin = 3,
    }
}
