using Netizine.Enums;

namespace IFOA.DB.Entities;

public class CandidatePreferredLocation
{
    public int Id { get; set; }
    public Guid CandidateId { get; set; }
    public virtual Candidate Candidate { get; set; }
    public Country Country { get; set; } = Country.NotSet;
    public string? City { get; set; }
}