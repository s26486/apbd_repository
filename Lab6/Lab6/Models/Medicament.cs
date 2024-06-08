namespace Lab6.Models;

public class Medicament
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Type { get; set; }
    public virtual ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; }

}