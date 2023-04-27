﻿using ToCutProjectors.drawing;

namespace ToCutProjectors.services
{
    public interface IFrameOperator
    {
        public bool IsOn { get; set; }
        public ProjectorFrame? ModifierFrame(ProjectorFrame modifierFrame);
    }
}
