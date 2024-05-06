using Animals.Models;

namespace Animals.Repositories;

public interface IAnimalsRepository
{
    IEnumerable<Animal> GetAnimals();
    IEnumerable<Animal> FetchAnimals();
    void CreateAnimal(Animal animal);
    void UpdateAnimal(Animal animal);
    void DeleteAnimal(Animal animal);


}