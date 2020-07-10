using Microsoft.EntityFrameworkCore;

public class Food {
    public long Id { get; set; }
    public string name { get; set; }
    public int nutritionalValue { get; set; }
    public int happiness { get; set; }
}

public class FoodContext : DbContext
{
    public FoodContext(DbContextOptions<FoodContext> options) : base(options){}
    public DbSet<Food> Foods { get; set; }
}
