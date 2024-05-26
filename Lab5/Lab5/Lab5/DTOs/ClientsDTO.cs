using System.ComponentModel.DataAnnotations;

namespace Lab5.DTOs;

public class TripDTO
{
    public int IdTrip { get; set; }
    public string Name { get; set; } 
    public string Description { get; set; } 
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public IEnumerable<ClientDTO> Clients { get; set; } // Corrected type to ClientDTO
}

public class CreateTripDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public DateTime DateFrom { get; set; }
    [Required]
    public DateTime DateTo { get; set; }
    [Required]
    public int MaxPeople { get; set; }
}

public class RemoveTripDTO
{
    [Required]
    public int IdTrip { get; set; }
}

public class ClientDTO
{
    public int IdClient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; } 
    public string Telephone { get; set; } 
    public string Pesel { get; set; } 
    public IEnumerable<TripDTO> Trips { get; set; }
}

public class AssignClientToTripDTO
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Telephone { get; set; }
    [Required]
    public string Pesel { get; set; }
    [Required]
    public int IdTrip { get; set; }
    public string TripName { get; set; }
    [Required]
    public DateTime? PaymentDate { get; set; } }

public class CreateClientDTO
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Telephone { get; set; }
    [Required]
    public string Pesel { get; set; }
}

public class DeleteClientDTO
{
    [Required]
    public int IdClient { get; set; }
}