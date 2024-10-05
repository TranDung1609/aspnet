using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using testapi.InputRequest;
using testapi.Models;

namespace testapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionControlController : ControllerBase
    {
        private readonly IMongoCollection<UserTest> _userTestsCollection;

        public VersionControlController(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("local");
            _userTestsCollection = database.GetCollection<UserTest>("user_tests");
        }

        [HttpGet("user-tests")]
        public async Task<ActionResult<List<UserTest>>> GetUserTests()
        {
            var userTests = await _userTestsCollection.Find(_ => true).ToListAsync();
            return Ok(userTests);
        }

        [HttpGet("user-tests/{id}")]
        public async Task<ActionResult<UserTest>> GetUserTestById(string id)
        {
            var objectId = ObjectId.Parse(id);
            var userTest = await _userTestsCollection.Find(x => x.Id == objectId).FirstOrDefaultAsync();
            if (userTest == null)
            {
                return NotFound();
            }
            return Ok(userTest);
        }

        [HttpPost("user-tests")]
        public async Task<ActionResult<UserTest>> CreateUserTest([FromBody] UserTestRequest newUserTestRequest)
        {
            var newUserTest = new UserTest
            {
                name = newUserTestRequest.name,
                gender = newUserTestRequest.gender,
                is_deleted = 0,
                Id = ObjectId.GenerateNewId()
            };

            await _userTestsCollection.InsertOneAsync(newUserTest);

            return CreatedAtAction(nameof(GetUserTestById), new { id = newUserTest.Id.ToString() }, newUserTest);
        }

        [HttpPut("user-tests/{id}")]
        public async Task<IActionResult> UpdateUserTest(string id, [FromBody] UserTestRequest updatedUserTestRequest)
        {
            var objectId = ObjectId.Parse(id);

            var existingUserTest = await _userTestsCollection.Find(x => x.Id == objectId).FirstOrDefaultAsync();
            if (existingUserTest == null)
            {
                return NotFound();
            }

            existingUserTest.name = updatedUserTestRequest.name;
            existingUserTest.gender = updatedUserTestRequest.gender;

            var result = await _userTestsCollection.ReplaceOneAsync(x => x.Id == objectId, existingUserTest);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("user-tests/{id}")]
        public async Task<IActionResult> DeleteUserTest(string id)
        {
            var objectId = ObjectId.Parse(id);
            var result = await _userTestsCollection.DeleteOneAsync(x => x.Id == objectId);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("user-tests/delete/{id}")]
        public async Task<IActionResult> UpdateDeleteUserTest(string id)
        {
            var objectId = ObjectId.Parse(id);

            var existingUserTest = await _userTestsCollection.Find(x => x.Id == objectId).FirstOrDefaultAsync();
            if (existingUserTest == null)
            {
                return NotFound();
            }

            existingUserTest.is_deleted = 1;

            var result = await _userTestsCollection.ReplaceOneAsync(x => x.Id == objectId, existingUserTest);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
