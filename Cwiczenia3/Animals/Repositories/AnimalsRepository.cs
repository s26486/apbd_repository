using System.Data.SqlClient;
using Animals.Models;


namespace Animals.Repositories;

public class AnimalsRepository : IAnimalsRepository
{
    
    private IConfiguration _configuration;

    public AnimalsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> GetAnimals()
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal";

        var dr = cmd.ExecuteReader();
        var animals = new List<Animal>();
        while (dr.Read())
        {
            var grade = new Animal()
            {
                IdAnimal = (int)dr["IdAnimal"],
                Name= dr["Name"].ToString(),
                Description = dr["Description"].ToString(),
                Category = dr["Category"].ToString(),
                Area = dr["Area"].ToString()
            };
            animals.Add(grade);
        }

        return animals;
    } 
    
    public Animal GetAnimal(int IdAnimal)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", IdAnimal);

        var dr = cmd.ExecuteReader();
        if (!dr.Read())
        {
            return null;
        }

        var animal = new Animal
        {
            IdAnimal = (int)dr["IdAnimal"],
            Name= dr["Name"].ToString(),
            Description = dr["Description"].ToString(),
            Category = dr["Category"].ToString(),
            Area = dr["Area"].ToString()
        };
        return animal;
    }
    
    public IEnumerable<Animal> FetchAnimals()
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal";

        var dr = cmd.ExecuteReader();
        var Animals = new List<Animal>();
        while (dr.Read())
        {
            var grade = new Animal
            {
                IdAnimal = (int)dr["IdAnimal"],
                Name= dr["Name"].ToString(),
                Description = dr["Description"].ToString(),
                Category = dr["Category"].ToString(),
                Area = dr["Area"].ToString()
            };
            Animals.Add(grade);
        }

        return Animals;
    }
    public void CreateAnimal(Animal animal)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "INSERT INTO Animal (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area)";
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);

        cmd.ExecuteNonQuery();
    }

    public void UpdateAnimal(Animal animal)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);

        cmd.ExecuteNonQuery();
    }

    public void DeleteAnimal(Animal animal)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
        cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);

        cmd.ExecuteNonQuery();
    }
}