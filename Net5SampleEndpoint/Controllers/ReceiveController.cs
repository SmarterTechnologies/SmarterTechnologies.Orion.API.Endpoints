using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Net5SampleEndpoint.Models;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Net5SampleEndpoint.Controllers
{
    /// <summary>
    /// Samples endpoints which can be used to receive data from the Smarter Technologies Group API Push Service.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ReceiveController : ControllerBase
    {
        private readonly ILogger<ReceiveController> _logger;

        public ReceiveController(ILogger<ReceiveController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Provides an endpoint which receives data from the Smarter Technologies API Push Service, and is deserialized by ASP.NET Core into a <see cref="TagData"/> object.
        /// </summary>
        /// <param name="tagData">An instance of <see cref="TagData"/> which has been deserialized from the JSON contained in the body of the request</param>
        [HttpPost("deserialize")]
        public IActionResult ReceiveSerializedDataFromPushService([FromBody] TagData tagData)
        {
            try
            {
                // TODO: Do something with the object.
                // For the purposes of this sample, we will just log the tag id.
                _logger.LogInformation($"Data received from tag id {tagData.TagId}");

                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());

                // Allow exception to be handled by exception handler
                // configured in Startup.
                throw;
            }
        }


        /// <summary>
        /// Provides an endpoint which simply receives the raw posted data from the Smarter Technologies API Push Service. 
        /// </summary>
        [HttpPost("raw")]
        public async Task<IActionResult> ReceiveRawDataFromPushService()
        {
            try
            {
                // Get the JSON sent by the Push Service from the body of the request.
                using StreamReader reader = new(Request.Body, Encoding.UTF8);
                string json = await reader.ReadToEndAsync();

                // Make sure data has been received.
                if (string.IsNullOrWhiteSpace(json))
                {
                    _logger.LogWarning("No data received in body of request");
                    return BadRequest("Body of request is empty");
                }

                // TODO: Process the data received from the API service.
                // For the purposes of this sample, we will just log the JSON received.
                _logger.LogInformation(json);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                // Allow exception to be handled by exception handler
                // configured in Startup.
                throw;
            }
        }
    }
}
