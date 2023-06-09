using System.ComponentModel.DataAnnotations;
using Netizine.Enums;

namespace IFOA.DB.Entities;

public class Candidate
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public GenderEnum Gender { get; set; } = GenderEnum.NotSpecified;
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime? BirthDate { get; set; } = DateTime.Now.AddYears(-18);
    public Country Nationality { get; set; } 
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public Country ResidenceCountry { get; set; } 
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    public int Ranking { get; set; } = 0;
    public string? ImageLink { get; set; }
    public string? CoverLetter { get; set; }
    public string? Biography { get; set; }
    
    public virtual List<CandidateCertification> CandidateCertifications { get; set; }
    public virtual List<CandidateProject> CandidateProjects { get; set; }
    public virtual List<CandidatePreferredJobFunction> CandidatePreferredJobFunctions { get; set; }
    public virtual List<CandidatePreferredLocation> CandidatePreferredLocations { get; set; }
    public virtual List<CandidateSpokenLanguage> CandidateSpokenLanguages { get; set; }
}