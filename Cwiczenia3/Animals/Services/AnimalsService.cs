using Animals.Models;
using Animals.Repositories;

namespace Animals.Services;

public class AnimalsService : IAnimalsService
{
    private readonly IAnimalsRepository _animalsRepository;

    public AnimalsService(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }

    public IEnumerable<Animal> GetAnimals()
    {
        return _animalsRepository.FetchAnimals();
    }

    public void CreateAnimal(Animal animal)
    {
        _animalsRepository.CreateAnimal(animal);
    }

    public void UpdateAnimal(Animal animal)
    {
        _animalsRepository.UpdateAnimal(animal);
    }

    public void DeleteAnimal(Animal animal)
    {
        _animalsRepository.DeleteAnimal(animal);
    }
}