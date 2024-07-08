using FluentModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace wpf_modbus
{
    public class Modbus : IModbusBridge
    {
        IModbusBridge bridge;


        public Modbus(IModbusBridge inputBridge)
        {
            bridge = inputBridge;
        }

        public void Connect(string port)
        {
            bridge.Connect(port);
        }

        public bool IsConnected()
        {
            return bridge.IsConnected();
        }

        public Span<byte> ReadCoils(int startingAddress, int quantity)
        {
            return bridge.ReadCoils(startingAddress, quantity);
        }

        public Span<T> ReadHoldingRegisters<T>(int startingAddress, int count) where T : unmanaged
        {
            return ReadHoldingRegisters<T>(startingAddress, count);
        }

        public void WriteSingleCoil(int registerAddress, bool value)
        {
            bridge.WriteSingleCoil(registerAddress, value);
        }

        public void WriteSingleRegister(int registerAddress, short value)
        {
            bridge.WriteSingleRegister(registerAddress, value);
        }
    }

    public interface IModbusBridge
    {
        bool IsConnected();
        void Connect(string port);
        Span<byte> ReadCoils(int startingAddress, int quantity);
        void WriteSingleCoil(int registerAddress, bool value);
        Span<T> ReadHoldingRegisters<T>(int startingAddress, int count) where T : unmanaged;
        void WriteSingleRegister(int registerAddress, short value);
    }

    /// <summary>
    /// Disconnects the current connection
    /// </summary>
    class ModbusBridge : IModbusBridge, IDisposable
    {
        public string Port { get; private set; } = string.Empty;
        public int ServerId {  get; private set; }

        ModbusRtuClient client;
        


        public ModbusBridge(int serverId)
        {
            client = new ModbusRtuClient();
            ServerId = serverId;
        }

        public bool IsConnected()
        {
            return client.IsConnected;
        }

        public void Connect(string port)
        {
            try
            {
                client.Connect(port, ModbusEndianness.BigEndian);
                Port = port;
            }

            catch
            {

            }
        }

        private void AssertConnected()
        {
            if (client.IsConnected == false)
            {
                throw new InvalidOperationException("Connect first");
            }
        }

        public Span<byte> ReadCoils(int startingAddress, int quantity)
        {
            AssertConnected();
            return client.ReadCoils(ServerId, startingAddress, quantity);
        }

        public void WriteSingleCoil(int registerAddress, bool value)
        {
            AssertConnected();
            client.WriteSingleCoil(ServerId, registerAddress, value);
        }

        public Span<T> ReadHoldingRegisters<T>(int startingAddress, int count) where T : unmanaged
        {
            AssertConnected();
            return client.ReadHoldingRegisters<T>(ServerId, startingAddress, count);
        }

        public void WriteSingleRegister(int registerAddress, short value)
        {
            AssertConnected();
            client.WriteSingleRegister(ServerId, registerAddress, value);
        }

        public void Dispose()
        {
            if(client.IsConnected)
            {
                client.Dispose();
            }
        }
    }
}
