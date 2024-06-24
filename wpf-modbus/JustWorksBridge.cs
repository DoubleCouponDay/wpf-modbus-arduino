using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using wpf_modbus_webview2;

namespace wpf_modbus_webview2
{
    public class JustWorksBridge : IModbusBridge
    {
        byte[] coils;
        byte[] registers;

        public JustWorksBridge(byte inputCoil, byte inputRegister)
        {
            coils = [inputCoil];
            registers = [inputRegister];
        }

        public void Connect(IPAddress address)
        {

        }

        public bool IsConnected()
        {
            return true;
        }

        public Span<byte> ReadCoils(int unitIdentifier, int startingAddress, int quantity)
        {
            return new Span<byte>(coils);
        }

        public Span<T> ReadHoldingRegisters<T>(int unitIdentifier, int startingAddress, int count) where T : unmanaged
        {
            var asSpan = registers.AsSpan();
            ReadOnlySpan<T> readonlySpan = MemoryMarshal.Cast<byte, T>(asSpan);
            var output = new Span<T>();
            readonlySpan.CopyTo(output);
            return output;
        }

        public void WriteSingleCoil(int unitIdentifier, int startingAddress, bool value)
        {
            if (value) {
                coils[0] |= (byte)(1 << startingAddress);
            }
            
            else
            {
                coils[0] |= (byte)(0 << startingAddress);
            }
        }

        public void WriteSingleRegister(int unitIdentifier, int registerAddress, short value)
        {
            registers[0] = (byte)value;
        }
    }
}
