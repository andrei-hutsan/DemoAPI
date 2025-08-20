using DemoAPI.DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceReference;

namespace DemoAPI.Controllers
{
    /// <summary>
    /// CRUD implementation
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonData _data;

        private readonly Func<PersonServiceClient> _clientFactory;

        public PeopleController(IPersonData data, Func<PersonServiceClient> clientFactory)
        {
            _data = data;
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// Returns a concrete person by ID from WCF
        /// </summary>
        /// <param name="id">Person ID</param>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var client = _clientFactory();
            try
            {
                var person = await client.GetPersonByIdAsync(id);
                if (person == null || person.Id == Guid.Empty)
                    return NotFound(ApiResponse<PersonModel>.Fail("Person not found."));

                return Ok(ApiResponse<Person>.Succeed(person));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PersonModel>.Fail("An error occurred while retrieving the person.", new List<string> { ex.Message }));
            }
        }

        /// <summary>
        /// ConnectionStatus
        /// </summary>
        /// <returns>Returns Status Code 200 OK()</returns>
        [Route("/status/")]
        [HttpGet]
        public IActionResult GetStatus() => Ok("API is working");

        /// <summary>
        /// Returns the first names of all persons
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetFirstNames()
        {
            try
            {
                var people = await _data.GetPersons();
                var names = people.Select(p => p.Firstname).ToList();
                return Ok(ApiResponse<List<string>>.Succeed(names));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<string>>.Fail(
                    "An error occurred while fetching first names.", 
                    new List<string> { ex.Message }
                ));
            }
        }

        /// <summary>
        /// Returns all persons
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var people = await _data.GetPersons();
                return Ok(ApiResponse<List<PersonModel>>.Succeed(people.ToList()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<PersonModel>>.Fail(
                    "An error occurred while fetching people.",
                    new List<string> { ex.Message }
                ));
            }
        }


        /// <summary>
        /// Returns a specific person by ID
        /// </summary>
        /// <param name="id">Person ID</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var person = await _data.GetPerson(id);
                if (person == null || person.Id == Guid.Empty)
                    return NotFound(ApiResponse<PersonModel>.Fail("Person not found."));

                return Ok(ApiResponse<PersonModel>.Succeed(person));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PersonModel>.Fail("An error occurred while retrieving the person.", new List<string> { ex.Message }));
            }
        }

        /// <summary>
        /// Adds a new person
        /// </summary>
        /// <param name="person">New person object</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonModel person)
        {
            try
            {
                if (person == null)
                    return BadRequest(ApiResponse<string>.Fail("Invalid person object."));

                await _data.InsertPerson(person);
                return Ok(ApiResponse<string>.Succeed("Person added successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail("An error occurred while adding the person.", new List<string> { ex.Message }));
            }
        }

        /// <summary>
        /// Deletes a person by ID
        /// </summary>
        /// <param name="id">Person ID</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var person = await _data.GetPerson(id);
                if (person == null)
                    return NotFound(ApiResponse<string>.Fail("Person not found."));

                await _data.DeletePerson(id);
                return Ok(ApiResponse<string>.Succeed("Person deleted successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail("An error occurred while deleting the person.", new List<string> { ex.Message }));
            }
        }
    }
}
