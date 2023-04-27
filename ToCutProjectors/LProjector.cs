using System.Collections.ObjectModel;
using ToCutProjectors.drawing;
using ToCutProjectors.services;

namespace ToCutProjectors
{
    public class LProjector : Collection<IFrameOperator>
    {
        public bool IsOn { get; set; } = true;
        private ProjectorFrame? Frame { get; set; }

        private CancellationTokenSource cancellation = new CancellationTokenSource();

        public async void UpdateFrame(ProjectorFrame? frame)
        {
            this.Frame = frame;
            await RenderFrame(frame);
        }

        private async Task RenderFrame(ProjectorFrame? frame)
        {
            cancellation.Cancel();

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

    public enum DeviceType : int
    {
        Virtual = 0,
        MonchaNET = 1,
        VLT = 2,
        Pangolin = 3,
    }
}
