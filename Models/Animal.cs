using Microsoft.EntityFrameworkCore;

namespace AnimalApi.Models {
    public class Animal {
        public long Id { get; set; }
        public string name { get; set; }
        public int hunger { get; set; }
        public int happiness { get; set; }

        public Animal(){
            // Both hunger and happiness can have values between -100 and +100, 0 being neutral
            this.hunger = 0;
            this.happiness = 0;
        }

        public Animal stroke() {
            this.happiness += 10;
            return this;
        }

        public Animal feed(int nutritionalValue) {
            this.hunger -= nutritionalValue;
            return this;
        }

        public Animal feed(Food food) {
            return this.feed(food.nutritionalValue);
        }

        public Animal step() {
            // Increases hunger and decreases happiness
            this.hunger += 1;
            this.happiness -= 1;
            return this;
        }
    }


    public class AnimalContext : DbContext
    {
        public AnimalContext(DbContextOptions<AnimalContext> options) : base(options){}
        public DbSet<Animal> Animals { get; set; }
    }


    public class AnimalDTO {
        public long Id { get; set; }
        public string name { get; set; }
        public int hunger { get; set; }
        public int happiness { get; set; }
    }
}
