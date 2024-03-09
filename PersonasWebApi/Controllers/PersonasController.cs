using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonasWebApi.Data;
using PersonasWebApi.Models;

namespace PersonasWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly PersonasContext _context;
        public PersonasController(PersonasContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
        {
            return await _context.Persona.ToListAsync();
        }



        [HttpGet("{id}")]

        public async Task<ActionResult<Persona>> GetPersona(int id) 
        {
            var persona = await _context.Persona.FindAsync(id);

            if (persona == null) 
            {
                return NotFound();  
            }
            return Ok(persona);
        }



        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(Persona persona) 
        {
            _context.Persona.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersona", new {id = persona.Id},persona);
        }





        [HttpDelete("{id}")]
        public async Task<ActionResult<Persona>> DeletePersona(int id) 
        {
            var persona = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            else 
            {
                _context.Persona.Remove(persona);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPersona(int id, Persona persona) 
        {
            if (id != persona.Id) 
            {
                return BadRequest();
            }

            _context.Entry(persona).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!PersonaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }




        private bool PersonaExists(int id)
        {
            return _context.Persona.Any(e => e.Id == id);
        }




    }
}
