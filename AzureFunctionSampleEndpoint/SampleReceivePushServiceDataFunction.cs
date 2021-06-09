using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctionSampleEndpoint
{
    public static class SampleReceivePushServiceDataFunction
    {
        [FunctionName("ReceiveJsonData")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("POST request received from API service. Attempting to deserialize JSON in body of request...");

                // Get access to the request body, and read the posted JSON.            
                using StreamReader streamReader = new StreamReader(req.Body);
                string json = await streamReader.ReadToEndAsync();

                // Make sure data has been received.
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new BadRequestResult();
                }

                // TODO: process the data received.
                // For the purposes of this sample, we will deserialize the JSON into a dynamic object 
                // to access the name of the tag.
                dynamic data = JsonConvert.DeserializeObject(json);

                // Get the Tag name from the data
                string tagId = data?.tag_id;
                log.LogInformation($"Received event data for Tag {tagId}");

                return new OkObjectResult($"Successfully processed data received for Tag Id {tagId}");
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Exception occurred attempting to read and deserialize JSON in body of request");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }           
        }
    }
}
