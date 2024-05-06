using Animals.Models;
using Animals.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Animals.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalsService _animalsService;

    public AnimalsController(IAnimalsService animalsService)
    {
        _animalsService = animalsService;
    }

    [HttpGet]
    public IActionResult GetAnimals([FromQuery] string orderBy = "name")
    {
        if (!IsValidOrderBy(orderBy))
        {
            return BadRequest("Invalid orderBy parameter. Available values: name, description, category, area.");
        }

        var animals = _animalsService.GetAnimals();
        
        switch (orderBy.ToLower())
        {
            case "name":
                animals = animals.OrderBy(a => a.Name);
                break;
            case "description":
                animals = animals.OrderBy(a => a.Description);
                break;
            case "category":
                animals = animals.OrderBy(a => a.Category);
                break;
            case "area":
                animals = animals.OrderBy(a => a.Area);
                break;
        }

        return Ok(animals);
    }
    
    [HttpGet("{id:int}")] 
    public IActionResult GetAnimal(int id)
    {
        var animal = _animalsService.GetAnimals().FirstOrDefault(s => s.IdAnimal == id);
        if (animal == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }

        return Ok(animal);
    }
    
    [HttpPost]
    public IActionResult CreateAnimal([FromBody] Animal animal)
    {
        if (animal == null)
        {
            return BadRequest("Animal object is null");
        }

        _animalsService.CreateAnimal(animal);
        return StatusCode(201);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, [FromBody] Animal animal)
    {
        var animalToUpdate = _animalsService.GetAnimals().FirstOrDefault(s => s.IdAnimal == id);
        if (animalToUpdate == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }
            
        animal.IdAnimal = id; 
        _animalsService.UpdateAnimal(animal);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animalToDelete = _animalsService.GetAnimals().FirstOrDefault(s => s.IdAnimal == id);
        if (animalToDelete == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }
        
        _animalsService.DeleteAnimal(animalToDelete);
        return NoContent();
    }
    
    private bool IsValidOrderBy(string orderBy)
    {
        var validValues = new List<string> { "name", "description", "category", "area" };
        return validValues.Contains(orderBy.ToLower());
    }
}