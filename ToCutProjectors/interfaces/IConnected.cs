using System.Net;

namespace ToCutProjectors.interfaces
{
    internal interface IConnected
    {
        public IPAddress IPAddress { get; set; }

        public bool IsConnected { get; }

        public Task Reconnect();
    }
}
