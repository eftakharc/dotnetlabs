using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Colors.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<FilesController> _logger;

        public FilesController(ILogger<FilesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Download a file. This demo will generate a txt file.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Download(int id)
        {
            return File(Encoding.ASCII.GetBytes("hello world"), "text/plain", $"test-{id}.txt");
        }

        /// <summary>
        /// Upload a file. This demo is dummy and only waits 2 seconds.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("single-file")]
        public async Task Upload(IFormFile file)
        {
            _logger.LogInformation($"validating the file {file.FileName}");
            _logger.LogInformation("saving file");
            await Task.Delay(2000);
            _logger.LogInformation("file saved.");
        }

        /// <summary>
        /// Upload multiple files. This demo is dummy and only waits 2 seconds.
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost("multiple-files")]
        public async Task UploadMultipleFiles(List<IFormFile> files)
        {
            // todo: Currently not working due to an issue in swagger-ui (https://github.com/swagger-api/swagger-ui/issues/4600)
            // todo: Can also follow this issue https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1029

            _logger.LogInformation($"validating {files.Count} files");
            _logger.LogInformation("saving files");
            await Task.Delay(2000);
            _logger.LogInformation("files saved.");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<bool> Delete(int id)
        {
            _logger.LogInformation($"deleting file ID=[{id}]");
            await Task.Delay(1500);
            return true;
        }
    }
}
