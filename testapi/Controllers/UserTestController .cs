using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using cloud.core;
using cloud.core.mongodb;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using testapi.InputRequest;

namespace testapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTestController : ControllerBase
    {
        private readonly AdsMongoDbContext _db;

        public UserTestController(AdsMongoDbContext db)
        {
            _db = db;
        }

        // GET: api/usertest
        [HttpGet]
        public async Task<ActionResult<List<UserTest>>> GetUsers()
        {
            var users = _db.user_tests.ToList();
            return Ok(users);
        }

        // GET: api/usertest/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTest>> GetUser(string id)
        {
            var user = _db.user_tests.Where(u => u.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/usertest
        [HttpPost]
        public async Task<ActionResult<UserTest>> CreateUser([FromBody] UserTestRequest newUserTestRequest)
        {
            var newUserTest = new UserTest
            {
                name = newUserTestRequest.name,
                gender = newUserTestRequest.gender,
                is_deleted = 0,
                Id = ObjectId.GenerateNewId()
            };
             await _db.user_tests.Insert(newUserTest);

            return CreatedAtAction(nameof(GetUser), new { id = newUserTest.Id }, newUserTest);
        }

        // PUT: api/usertest/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserTestRequest updatedUserTestRequest)
        {
            if (updatedUserTestRequest == null || string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Invalid user data or id.");
            }

            var user = await _db.user_tests.Where(u => u.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.name = updatedUserTestRequest.name;
            user.gender = updatedUserTestRequest.gender;

            await _db.user_tests.Update(user);

            return NoContent(); 
        }



        // DELETE: api/usertest/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _db.user_tests.Delete(ObjectId.Parse(id));
        
            return NoContent();
        }
    }
}