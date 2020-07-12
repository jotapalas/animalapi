using System.Collections.Generic;

namespace AnimalApi.Models {
    public class Animal {
        public long Id { get; set; }
        public string name { get; set; }
        public ICollection<Pet> owners { get; set; }

        public AnimalDTO toDTO() {
            return new AnimalDTO {
                Id = this.Id,
                name = this.name
            };
        }
    }


    public class AnimalDTO {
        public long Id { get; set; }
        public string name { get; set; }
    }
}
