using System.Collections.Generic;

namespace AnimalApi.Models{
    public class User {
        public long Id { get; set; }
        public string name { get; set; }
        public ICollection<Pet> pets { get; set; }

        public User () {
            this.pets = new List<Pet>();
        }

        public UserResponseDTO toDto () {
            List<PetResponseDTO> pets = new List<PetResponseDTO>();
            foreach (Pet pet in this.pets) {
                pets.Add(pet.toDTO());
            }
            return new UserResponseDTO
            {
                Id = this.Id,
                name = this.name,
                pets = pets
            };
        }
    }

    public class UserDTO {
        public long Id { get; set; }
        public string name { get; set; }
    }

    public class UserResponseDTO {
        public long Id { get; set; }
        public string name { get; set; }
        public ICollection<PetResponseDTO> pets { get; set; }
    }
}
