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

        public Pet stroke(int happiness = 10) {
            this.Happiness += happiness;
            if (this.Happiness < -100) {
                this.Happiness = -100;
            }
            if (this.Happiness > 100) {
                this.Happiness = 100;
            }
            return this;
        }

        public Pet feed(int nutritionalValue = 10) {
            this.Hunger -= nutritionalValue;
            if (this.Hunger < -100) {
                this.Hunger = -100;
            }
            if (this.Hunger > 100) {
                this.Hunger = 100;
            }
            return this;
        }

        public Pet step(int hungerIncrease = 1, int happinessDecrease = 1) {
            // Increases hunger and decreases happiness
            this.feed(-hungerIncrease);
            this.stroke(-happinessDecrease);
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


    public class PetStepDTO {
        public int HungerIncrease { get; set; }
        public int HappinessDecrease { get; set; }
    }


    public class PetFeedDTO {
        public int NutritionalValue { get; set; }
    }


    public class PetStrokeDTO {
        public int HappinessIncrease { get; set; }
    }
}
