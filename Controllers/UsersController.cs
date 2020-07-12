using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalApi.Models;
using AnimalApi.Context;

namespace AnimalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AnimalContext _context;

        public UsersController(AnimalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return await _context.Users
                .Select(x => UserToDTO(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return UserToDTO(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.name = userDTO.name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!UserExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO userDTO)
        {
            var user = new User
            {
                name = userDTO.name
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(CreateUser),
                new { id = user.Id },
                UserToDTO(user));
        }

        [HttpPost("{id}/addPet")]
        public async Task<IActionResult> AddPetToUser(long id, PetDTO petDTO)
        {
            if (id != petDTO.OwnerId)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(petDTO.AnimalId);
            if (animal == null)
            {
                return NotFound();
            }

            var pet = new Pet {
                Name = petDTO.Name,
                AnimalId = petDTO.AnimalId,
                Animal = animal,
                OwnerId = petDTO.OwnerId,
                Owner = user
            };

            user.pets.Add(pet);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!UserExists(id))
            {
                return NotFound();
            }

            return CreatedAtAction(
                nameof(AddPetToUser),
                new { id = user.Id },
                UserToDTO(user));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var animal = await _context.Users.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            _context.Users.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id) =>
            _context.Users.Any(e => e.Id == id);

        private static UserDTO UserToDTO(User user) =>
            new UserDTO
            {
                Id = user.Id,
                name = user.name,
                pets = user.pets
            };
        }
}
