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
    public class PetsController : ControllerBase
    {
        private readonly AnimalContext _context;

        public PetsController(AnimalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetResponseDTO>>> GetPets()
        {
            return await _context.Pets
                .Select(x => x.toDTO())
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetResponseDTO>> GetPet(long id)
        {
            var pet = await _context.Pets.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            return pet.toDTO();
        }

        [HttpPut("{id}/feed")]
        public async Task<ActionResult<PetResponseDTO>> feedPet(long id) {
            return await this.performAction(id, "feed");
        }

        [HttpPut("{id}/stroke")]
        public async Task<ActionResult<PetResponseDTO>> strokePet(long id) {
            return await this.performAction(id, "stroke");
        }

        [HttpPatch("step")]
        public async Task<ActionResult<PetResponseDTO>> StepAllPets() {
            foreach (Pet pet in _context.Pets) {
                pet.step();
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/step")]
        public async Task<ActionResult<PetResponseDTO>> StepPet(long id) {
            return await performAction(id, "step");
        }

        [HttpPost]
        public async Task<ActionResult<PetResponseDTO>> CreatePet(PetDTO petDTO)
        {
            long ownerId = petDTO.OwnerId;
            var owner = await _context.Users.FindAsync(ownerId);
            if (owner == null) {
                return NotFound();
            }

            long animalId = petDTO.AnimalId;
            var animal = await _context.Animals.FindAsync(animalId);
            if (animal == null) {
                return NotFound();
            }

            var pet = new Pet
            {
                Name = petDTO.Name,
                AnimalId = animalId,
                Animal = animal,
                OwnerId = ownerId,
                Owner = owner
            };

            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(CreatePet),
                new { id = pet.Id },
                pet.toDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(long id)
        {
            var pet = await _context.Pets.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetExists(long id) =>
            _context.Pets.Any(e => e.Id == id);

        private async Task<ActionResult<PetResponseDTO>> performAction(long id, string action)
        {
            List<string> possibleActions = new List<string>(){
                "feed",
                "stroke",
                "step"
            };

            if (!possibleActions.Contains(action)) {
                return BadRequest();
            }

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            MethodInfo method = pet.GetType().GetMethod(action);
            method.Invoke(pet, new object[]{});

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PetExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
