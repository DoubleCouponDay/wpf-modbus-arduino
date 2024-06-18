using FluentModbus;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace wpf_modbus_webview2
{
    class Modbus
    {

        IModbusBridge bridge;

        public Modbus(IModbusBridge inputBridge)
        {
            bridge = inputBridge;
        }
    }

    interface IModbusBridge
    {
        void ModbusRequest(string command);
        Task<string> ModbusResponse();
    }

    class ModbusBridge : IModbusBridge
    {
        ModbusTcpServer server;

        public ModbusBridge(ILogger logger, IPAddress sawAddress, IPAddress trolleyAddress)
        {
            server = new ModbusTcpServer(logger);
            server.Start();
        }
        public void ModbusRequest(string command)
        {
            server.
        }

        public Task<string> ModbusResponse()
        {
            
        }
    }
}