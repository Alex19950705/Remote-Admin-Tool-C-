using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Socks5Client
{
    public class UdpAssociate
    {
        public class Constants
        {
            public enum AddressType : byte
            {
                IPv4 = 0x01,
                Domain = 0x03,
                IPv6 = 0x04
            }
        }

        public ushort Fragment { get; private set; }
        public Constants.AddressType AddressType { get; private set; }
        public string Domain { get; private set; }
        public IPAddress DestinationAddress { get; private set; }
        public ushort DestinationPort { get; private set; }
        public bool DnsSuccess { get; private set; }
        public byte[] Data { get; private set; }

        private Constants.AddressType PackDestinationAddress(string hostName, IPAddress address,
            out byte[] addressBytes)
        {
            Constants.AddressType addressType;
            if (address != null)
            {
                addressType = address.AddressFamily == AddressFamily.InterNetworkV6
                    ? Constants.AddressType.IPv6
                    : Constants.AddressType.IPv4;
                addressBytes = address.GetAddressBytes();
            }
            else
            {
                var isValid = IPAddress.TryParse(hostName, out address);
                if (isValid)
                {
                    addressType = address.AddressFamily == AddressFamily.InterNetworkV6
                        ? Constants.AddressType.IPv6
                        : Constants.AddressType.IPv4;
                    addressBytes = address.GetAddressBytes();
                }
                else
                {
                    addressType = Constants.AddressType.Domain;
                    addressBytes = Encoding.UTF8.GetBytes(hostName);
                }
            }

            return addressType;
        }

        public byte[] PackUdp(string destHost, IPAddress destAddress, int destPort, byte[] payloadBuffer, int bytes)
        {
            // Add socks UDP associate request header
            // +-----+------+------+----------+----------+----------+
            // | RSV | FRAG | ATYP | DST.ADDR | DST.PORT |   DATA   |
            // +-----+------+------+----------+----------+----------+
            // |  2  |  1   |  1   | Variable |    2     | Variable |
            // +-----+------+------+----------+----------+----------+

            if (!string.IsNullOrEmpty(destHost)) destAddress = null;
            var type = PackDestinationAddress(destHost, destAddress, out var addressBytes);

            // 1 byte of domain name length followed by 1–255 bytes the domain name if destination address is a domain
            var destAddressLength = addressBytes.Length + (type == Constants.AddressType.Domain ? 1 : 0);
            var buffer = new byte[4 + destAddressLength + 2 + bytes];

            using (var stream = new MemoryStream(buffer))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(ushort.MinValue);
                writer.Write(byte.MinValue);
                writer.Write((byte) type);

                switch (type)
                {
                    case Constants.AddressType.IPv4:
                    case Constants.AddressType.IPv6:
                        writer.Write(addressBytes);
                        break;
                    case Constants.AddressType.Domain:
                        writer.Write((byte) addressBytes.Length);
                        writer.Write(addressBytes);
                        break;
                    default:
                        throw new InvalidOperationException($"Unsupported type: {type}.");
                }

                writer.Write(IPAddress.HostToNetworkOrder((short) destPort));
                writer.Write(payloadBuffer, 0, bytes);
            }

            return buffer;
        }

        public bool Parse(byte[] response)
        {
            using (var stream = new MemoryStream(response))
            {
                using (var reader = new BinaryReader(stream))
                {
                    //      +----+------+------+----------+----------+----------+
                    //      |RSV | FRAG | ATYP | DST.ADDR | DST.PORT |   DATA   |
                    //      +----+------+------+----------+----------+----------+
                    //      | 2  |  1   |  1   | Variable |    2     | Variable |
                    //      +----+------+------+----------+----------+----------+
                    //
                    //     The fields in the UDP request header are:
                    //
                    //          o  RSV  Reserved X'0000'
                    //          o  FRAG    Current fragment number
                    //          o  ATYP    address type of following addresses:
                    //             o  IP V4 address: X'01'
                    //             o  DOMAINNAME: X'03'
                    //             o  IP V6 address: X'04'
                    //          o  DST.ADDR       desired destination address
                    //          o  DST.PORT       desired destination port
                    //          o  DATA     user data

                    if (reader.BaseStream.Length < 10)
                        return false;

                    if (reader.ReadByte() != 0)
                        return false;
                    if (reader.ReadByte() != 0)
                        return false;

                    Fragment = reader.ReadByte();

                    AddressType = (Constants.AddressType) reader.ReadByte();
                    if (!Enum.IsDefined(typeof(Constants.AddressType), AddressType))
                        return false;

                    if (AddressType == Constants.AddressType.IPv4)
                    {
                        DestinationAddress = new IPAddress((uint) reader.ReadInt32());
                    }
                    else if (AddressType == Constants.AddressType.IPv6)
                    {
                        DestinationAddress = new IPAddress(reader.ReadBytes(16));
                    }
                    else if (AddressType == Constants.AddressType.Domain)
                    {
                        var domainLength = reader.ReadByte();
                        Domain = Encoding.UTF8.GetString(reader.ReadBytes(domainLength));

                        try
                        {
                            var dnsResults = Dns.GetHostEntry(Domain);
                            DestinationAddress = dnsResults.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
                            DnsSuccess = true;
                        }
                        catch (Exception)
                        {
                        }

                        if (DestinationAddress == null)
                        {
                            DestinationAddress = IPAddress.Loopback;
                            DnsSuccess = false;
                        }
                    }

                    DestinationPort = (ushort) IPAddress.NetworkToHostOrder(reader.ReadInt16());
                    Data = reader.ReadBytes(Convert.ToInt32(reader.BaseStream.Length - reader.BaseStream.Position));
                    return true;
                }
            }
        }
    }
}