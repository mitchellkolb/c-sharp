using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallyBoard.Data;
using TallyBoard.Models;

namespace TallyBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PeopleController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/people
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            var peopleList = await _dbContext.People.ToListAsync();
            return Ok(peopleList);
        }

        // GET: api/people/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _dbContext.People.FindAsync(id);
            return person is null ? NotFound() : Ok(person);
        }

        // POST: api/people
        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson(Person newPerson)
        {
            _dbContext.People.Add(newPerson);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPerson), new { id = newPerson.Id }, newPerson);
        }

        // PUT: api/people/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePerson(int id, Person updatedPerson)
        {
            if (id != updatedPerson.Id)
                return BadRequest();

            _dbContext.Entry(updatedPerson).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/people/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _dbContext.People.FindAsync(id);
            if (person is null)
                return NotFound();

            _dbContext.People.Remove(person);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        private bool PersonExists(int id) =>
            _dbContext.People.Any(e => e.Id == id);
    }
}

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    public PeopleController(ApplicationDbContext dbContext)
        => this.dbContext = dbContext;

    // ... existing GET/POST/PUT/DELETE actions ...

    // PATCH: api/people/5/increment
    [HttpPatch("{id:int}/increment")]
    public async Task<ActionResult<Person>> IncrementTally(int id)
    {
        var person = await dbContext.People.FindAsync(id);
        if (person is null)
            return NotFound();

        person.TallyAmount++;
        await dbContext.SaveChangesAsync();

        return Ok(person);
    }

    // PATCH: api/people/5/decrement
    [HttpPatch("{id:int}/decrement")]
    public async Task<ActionResult<Person>> DecrementTally(int id)
    {
        var person = await dbContext.People.FindAsync(id);
        if (person is null)
            return NotFound();

        person.TallyAmount--;
        await dbContext.SaveChangesAsync();

        return Ok(person);
    }
}
