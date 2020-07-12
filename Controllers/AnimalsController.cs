using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalApi.Models;
using AnimalApi.Context;

namespace AnimalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalContext _context;

        public AnimalsController(AnimalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalDTO>>> GetAnimals()
        {
            return await _context.Animals
                .Select(x => x.toDTO())
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalDTO>> GetAnimal(long id)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal.toDTO();
        }

        [HttpPost]
        public async Task<ActionResult<AnimalDTO>> CreateAnimal(AnimalDTO animalDTO)
        {
            var animal = new Animal
            {
                name = animalDTO.name
            };

            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(CreateAnimal),
                new { id = animal.Id },
                animal.toDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(long id)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimalExists(long id) =>
            _context.Animals.Any(e => e.Id == id);

    }
}
