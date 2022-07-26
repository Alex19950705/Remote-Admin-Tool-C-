using System.IO;
using System.Xml.Serialization;

namespace Socks5Server
{
    public class Serializer
    {
        public string Serialize(Socks5State data)
        {
            var xmlSerializer = new XmlSerializer(typeof(Socks5State));
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, data);
                return stringWriter.ToString();
            }
        }

        public Socks5State Deserialize(string data)
        {
            var xmlSerializer = new XmlSerializer(typeof(Socks5State));
            using (var stringReader = new StringReader(data))
            {
                return xmlSerializer.Deserialize(stringReader) as Socks5State;
            }
        }
    }
}