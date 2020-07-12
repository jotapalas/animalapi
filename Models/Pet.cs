using System.ComponentModel.DataAnnotations;

namespace AnimalApi.Models {
    public class Pet {
        public long Id { get; set; }
        public long AnimalId { get; set; }
        public Animal Animal { get; set; }
        public long OwnerId { get; set; }
        public User Owner { get; set; }
        public string Name { get; set; }
        public int Hunger { get; set; }
        public int Happiness { get; set; }

        public Pet() : base() {
            // Both hunger and happiness can have values between -100 and +100, 0 being neutral
            this.Hunger = 0;
            this.Happiness = 0;
        }

        public Pet stroke() {
            this.Happiness += 10;
            return this;
        }

        public Pet feed() {
            this.Hunger -= 10;
            return this;
        }

        public Pet step() {
            // Increases hunger and decreases happiness
            this.Hunger += 1;
            this.Happiness -= 1;
            return this;
        }

        public PetResponseDTO toDTO() => new PetResponseDTO {
            Id = this.Id,
            Name = this.Name,
            AnimalId = this.AnimalId,
            OwnerId = this.OwnerId,
            Hunger = this.Hunger,
            Happiness = this.Happiness
        };
    }


    public class PetDTO {
        public long Id { get; set; }
        public long AnimalId { get; set; }
        public long OwnerId { get; set; }
        public string Name { get; set; }
    }

    public class PetResponseDTO {
        public long Id { get; set; }
        public long AnimalId { get; set; }
        public long OwnerId { get; set; }
        public string Name { get; set; }
        public int Hunger { get; set; }
        public int Happiness { get; set; }
    }
}
