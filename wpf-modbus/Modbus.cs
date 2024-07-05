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

        public Span<byte> ReadCoils(int unitIdentifier, int startingAddress, int quantity)
        {
            return bridge.ReadCoils(unitIdentifier, startingAddress, quantity);
        }

        public Span<T> ReadHoldingRegisters<T>(int unitIdentifier, int startingAddress, int count) where T : unmanaged
        {
            return ReadHoldingRegisters<T>(unitIdentifier, startingAddress, count);
        }

        public void WriteSingleCoil(int unitIdentifier, int startingAddress, bool value)
        {
            bridge.WriteSingleCoil(unitIdentifier, startingAddress, value);
        }

        public void WriteSingleRegister(int unitIdentifier, int registerAddress, short value)
        {
            bridge.WriteSingleRegister(unitIdentifier, registerAddress, value);
        }
    }

    public interface IModbusBridge
    {
        bool IsConnected();
        void Connect(string port);
        Span<byte> ReadCoils(int unitIdentifier, int startingAddress, int quantity);
        void WriteSingleCoil(int unitIdentifier, int startingAddress, bool value);
        Span<T> ReadHoldingRegisters<T>(int unitIdentifier, int startingAddress, int count) where T : unmanaged;
        void WriteSingleRegister(int unitIdentifier, int registerAddress, short value);
    }

    /// <summary>
    /// Disconnects the current connection
    /// </summary>
    class ModbusBridge : IModbusBridge, IDisposable
    {
        public string Port { get; private set; } = string.Empty;
        ModbusRtuClient client;


        public ModbusBridge()
        {
            client = new ModbusRtuClient();
        }

        public bool IsConnected()
        {
            return client.IsConnected;
        }

        public void Connect(string port)
        {
            client.Connect(port);
            Port = port;
        }

        private void AssertConnected()
        {
            if (client.IsConnected == false)
            {
                throw new InvalidOperationException("Connect first");
            }
        }

        public Span<byte> ReadCoils(int unitIdentifier, int startingAddress, int quantity)
        {
            AssertConnected();
            return client.ReadCoils(unitIdentifier, startingAddress, quantity);
        }

        public void WriteSingleCoil(int unitIdentifier, int startingAddress, bool value)
        {
            AssertConnected();
            client.WriteSingleCoil(unitIdentifier, startingAddress, value);
        }

        public Span<T> ReadHoldingRegisters<T>(int unitIdentifier, int startingAddress, int count) where T : unmanaged
        {
            AssertConnected();
            return client.ReadHoldingRegisters<T>(unitIdentifier, startingAddress, count);
        }

        public void WriteSingleRegister(int unitIdentifier, int registerAddress, short value)
        {
            AssertConnected();
            client.WriteSingleRegister(unitIdentifier, registerAddress, value);
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
