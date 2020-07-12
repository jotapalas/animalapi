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
            return new UserResponseDTO {
                name = this.name,
                Id = this.Id
            };
        }
    }

    public class UserDTO {
        public long Id { get; set; }
        public string name { get; set; }
        public ICollection<Pet> pets { get; set; }
    }

    public class UserResponseDTO {
        public long Id { get; set; }
        public string name { get; set; }
    }
}
