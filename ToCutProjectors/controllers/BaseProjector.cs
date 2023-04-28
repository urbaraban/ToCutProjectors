using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToCutProjectors.drawing;
using ToCutProjectors.interfaces;

namespace ToCutProjectors.controllers
{
    public abstract class BaseProjector : IFrameOperator
    {
        public bool IsOn { get; set; }

        public event EventHandler<bool>? StatusChanged;

        public abstract ProjectorFrame? FrameOperation(ProjectorFrame modifierFrame);

    }
}
