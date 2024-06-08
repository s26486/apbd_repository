using Lab6.Context;
using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly AppDbContext _context;

    public PatientController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatientsAsync()
    {
        var patients = await _context.Patients.ToListAsync();
        return Ok(patients);
    }
}