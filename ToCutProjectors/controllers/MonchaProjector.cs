﻿using MonchaNETDll.NETLaserDevices;
using StclLibrary.Laser;
using System.Net;
using System.Net.NetworkInformation;
using ToCutProjectors.interfaces;

namespace ToCutProjectors.controllers
{
    public class MonchaProjector : BaseProjector
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

        public IPAddress IPAddress
        {
            get => ipadress;
            set
            {
                ipadress = value;
                Reconnect();
            }
        }
        private IPAddress ipadress;
        public int Number { get; set; } = UDPBeginnerPort;
        public virtual bool IsConnected { get; } = true;

        public int FPS { get; set; } = 30;

        public DeviceType DeviceType { get; } = DeviceType.MonchaNET;

        private LaserDevice2? LaserDevice { get; set; }

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

        public MonchaProjector(IPAddress iPAddress, int Number)
        {
            this.ipadress = iPAddress;
            this.Number = Number;
        }

        public override drawing.ProjectorFrame? FrameOperation(drawing.ProjectorFrame modifierFrame)
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

        public async Task Reconnect()
        {
            await Task.Run(() =>
            {
                try
                {
                    Ping x = new Ping();
                    PingReply reply = x.Send(this.IPAddress);

                    if (reply.Status == IPStatus.Success)
                    {
                        this.Disconnect();

                        if (this.LaserDevice == null || this.LaserDevice.LD == null)
                        {
                            this.LaserDevice = new LaserDevice2(this.IPAddress, Number);

                            var ConnectAbortToken = new CancellationTokenSource();
                            CancellationToken CT = ConnectAbortToken.Token;

                            var task = Task.Run(() =>
                            {
                                while (this.LaserDevice.IsConnected(100) == false
                                && this.LaserDevice.DevType == LaserDevice2.DeviceType.None
                                && CT.IsCancellationRequested == false)
                                {
                                    this.LaserDevice.IsConnected(100);
                                }
                            }, ConnectAbortToken.Token);

                            Thread.Sleep(1000);
                            ConnectAbortToken.Cancel();

                            if (this.LaserDevice.DevType == LaserDevice2.DeviceType.MonchaNET1)
                            {
                                this.LaserDevice.TcpSocket.ReceiveTimeoutMs = 10000;
                            }

                            if (this.LaserDevice.LD != null)
                            {
                                while (this.LaserDevice.CanSendNextFrame(100) == false)
                                {
                                    this.LaserDevice.SendFrame(2000, new LFrame(), 10000);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    //ProjectorHub.Log?.Invoke($"No find ethernet", $"{this.NameID}_{this.IPAddress}");
                }
            });
        }

        public void Disconnect()
        {
            if (this.LaserDevice != null)
            {
                try
                {
                    this.LaserDevice.Close();
                    Thread.Sleep(200);
                }
                catch
                {
                    //ProjectorHub.Log?.Invoke($"{this.NameID} fail disconnect", $"{this.NameID}_{this.IPAddress}");
                }
            }
        }
    }
}
