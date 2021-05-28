using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json.Linq;

namespace DynamoDB.API.Controllers
{
    /// <summary>
    /// UsersController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IAmazonDynamoDB _ddbClient;
        private readonly Table _usersTable;

        /// <summary>
        /// Users controller constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ddbClient"></param>
        public UsersController(ILogger<UsersController> logger, IAmazonDynamoDB ddbClient)
        {
            _logger = logger;
            _ddbClient = ddbClient;
            _usersTable = Table.LoadTable(_ddbClient, "Users");
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Getting users...");

            ScanFilter filter = new ScanFilter();
            var users = await _usersTable.Scan(filter).GetRemainingAsync();

            return Ok(users);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            Document user = await _usersTable.GetItemAsync(id);

            return Ok(user.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]JObject value)
        {
            try
            {
                Document user = Document.FromJson(value.ToString());
                await _usersTable.PutItemAsync(user);

                return Ok("User created.");
            }
            catch (Exception e)
            {
                throw new Exception($"Erro: {e.Message}");
            }
        }
    }
}
