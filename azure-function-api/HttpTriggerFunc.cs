using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CDV.Project
{
    public static class HttpTriggerFunc
    {
        [FunctionName("HttpTriggerFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                string connectionString = Environment.GetEnvironmentVariable("ProjectDb");
                log.LogInformation(connectionString);
                var db = new DatabaseContext(connectionString);
                var user = db.GetUsers();
                return new JsonResult(user);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return new JsonResult(ex);
            }
        }
    }
}
