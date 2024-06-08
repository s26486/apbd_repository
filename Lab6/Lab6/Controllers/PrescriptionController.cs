using Lab6.Context;
using Lab6.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly AppDbContext _context;

    public PrescriptionController(AppDbContext context)
    {
        _context = context;
    }
    
}