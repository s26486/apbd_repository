using Animals.Models;

namespace Animals.Services;

public interface IAnimalsService
{
    IEnumerable<Animal> GetAnimals();

    void CreateAnimal(Animal animal);
        
    void UpdateAnimal(Animal animal);
        
    void DeleteAnimal(Animal animal);
    
}