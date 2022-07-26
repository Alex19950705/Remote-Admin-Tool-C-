using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Socks5Server.Socks5;

namespace Socks5Server
{
    [AllowAnonymous]
    public class HttpListenerController : Controller
    {
        [DisableRequestSizeLimit]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Get()
        {
            try
            {
                var dequeueElement = QueueManager.DequeueElement("socks");
                var response = string.Empty;
                if (dequeueElement != null)
                {
                    var serializer = new Serializer();
                    response = serializer.Serialize(dequeueElement);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(null);
            }
        }

        [DisableRequestSizeLimit]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Post()
        {
            try
            {
                using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var body = await reader.ReadToEndAsync();
                    var serializer = new Serializer();
                    var result = serializer.Deserialize(body);
                    ConnectionManager.UpdateConnection("socks", result);
                    var response = string.Empty;
                    return Ok(response);
                }
            }
            catch
            {
                return Ok(null);
            }
        }
    }
}