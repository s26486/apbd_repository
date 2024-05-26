using Lab5.Context;
using Lab5.DTOs;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly MasterContext _masterContext;
    public TripsController(MasterContext masterContext)
    {
        _masterContext = masterContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetTripsAsync()
    {
        var trips = await _masterContext.Trips
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.IdClientNavigation)
            .Select(t => new TripDTO
            {
                IdTrip = t.IdTrip,
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Clients = t.ClientTrips.Select(ct => new ClientDTO
                {
                    IdClient = ct.IdClient,
                    FirstName = ct.IdClientNavigation.FirstName,
                    LastName = ct.IdClientNavigation.LastName,
                    Email = ct.IdClientNavigation.Email,
                    Telephone = ct.IdClientNavigation.Telephone,
                    Pesel = ct.IdClientNavigation.Pesel
                }).ToList()
            })
            .ToListAsync();
        return Ok(trips);
    }

    [HttpPost]
    public async Task<IActionResult> AddTripAsync(CreateTripDTO tripDto)
    {
        var trip = new Trip()
        {
            Name = tripDto.Name,
            Description = tripDto.Description,
            DateFrom = tripDto.DateFrom,
            DateTo = tripDto.DateTo,
            MaxPeople = tripDto.MaxPeople
        };
        await _masterContext.Trips.AddAsync(trip);
        await _masterContext.SaveChangesAsync();
        return Ok(trip.IdTrip);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTripAsync(int id)
    {
        var trip = await _masterContext.Trips.FindAsync(id);
        if (trip == null)
        {
            return NotFound();
        }

        _masterContext.Trips.Remove(trip);
        await _masterContext.SaveChangesAsync();
        return NoContent();
    }
}
