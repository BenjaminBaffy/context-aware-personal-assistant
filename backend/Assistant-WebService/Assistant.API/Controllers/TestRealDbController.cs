using System.Threading.Tasks;
using Assistant.Application.Interfaces;
using Assistant.Domain.Database;
using Microsoft.AspNetCore.Mvc;

namespace Assistant.API.Controllers
{
    [ApiController]
    [Route("api/test-realdb")]
    public class TestRealDbController : Controller
    {
        private readonly IDatabaseService<TestData> _testDataDatabaseService;

        public TestRealDbController(IDatabaseService<TestData> testDataDatabaseService)
        {
            _testDataDatabaseService = testDataDatabaseService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var createdData = await _testDataDatabaseService.Add
            (
                new TestData
                {
                    FirstName = firstName,
                    LastName = lastName
                }
            );

            return Created("", createdData.Id);
        }

        [HttpGet("collection")]
        public async Task<IActionResult> GetAll()
        {
            var testDatas = await _testDataDatabaseService.GetAll();

            return Ok(testDatas);
        }
    }
}
