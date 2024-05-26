using Lab5.Context;
using Lab5.DTOs;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly MasterContext _masterContext;
    public ClientsController(MasterContext masterContext)
    {
        _masterContext = masterContext;
    }
    [HttpGet]
    public async Task<IActionResult> GetClientsAsync()
    {
        var clients = await _masterContext.Clients
            .Include(c => c.ClientTrips)
            .ThenInclude(ct => ct.IdTripNavigation)
            .Select(c => new ClientDTO
                {
                    IdClient = c.IdClient,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Telephone = c.Telephone,
                    Pesel = c.Pesel,
                    Trips = c.ClientTrips.Select(ct => new TripDTO
                    {
                        IdTrip = ct.IdTrip,
                        Name = ct.IdTripNavigation.Name,
                        Description = ct.IdTripNavigation.Description,
                        DateFrom = ct.IdTripNavigation.DateFrom,
                        DateTo = ct.IdTripNavigation.DateTo,
                        MaxPeople = ct.IdTripNavigation.MaxPeople
                        
                    })
                })
            .ToListAsync();
        return Ok(clients);
    }

    [HttpPost]
    public async Task<IActionResult> AddClientAsync(CreateClientDTO clientDto)
    {
        var client = new Client()
        {
            FirstName = clientDto.FirstName,
            LastName = clientDto.LastName,
            Email = clientDto.Email,
            Telephone = clientDto.Telephone,
            Pesel = clientDto.Pesel

        };
        await _masterContext.Clients.AddAsync(client);
        await _masterContext.SaveChangesAsync();
        return Ok(client.IdClient);
    }

    [HttpPost("{idClient}/groups/{idTrip}")]
    public async Task<IActionResult> AssignClientToTripAsync([FromBody] AssignClientToTripDTO dto)
    {
        var trip = await _masterContext.Trips.FindAsync(dto.IdTrip);
        if (trip is null) return NotFound("Trip not found");
        
        var client = await _masterContext.Clients
            .FirstOrDefaultAsync(c => c.Pesel == dto.Pesel);
        if (client == null)
        {
            client = new Client
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Pesel = dto.Pesel
            };

            await _masterContext.Clients.AddAsync(client);
            await _masterContext.SaveChangesAsync();
        }
        
        

        var clientTrip = await _masterContext.ClientTrips
            .FirstOrDefaultAsync(ct => ct.IdClientNavigation.Pesel == dto.Pesel && dto.IdTrip == ct.IdTripNavigation.IdTrip);
        if (clientTrip is not null)
        {
            return Conflict("Client is already assigned to this trip");
        }
        var newClientTrip = new ClientTrip()
        {
            IdClient = client.IdClient,
            IdTrip = dto.IdTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = dto.PaymentDate
        };


        await _masterContext.ClientTrips.AddAsync(newClientTrip);
        await _masterContext.SaveChangesAsync();

        return NoContent();

    }
    

}