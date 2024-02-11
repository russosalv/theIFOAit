namespace IFOA.DB.Entities;

public class CandidateCertification
{
    public int Id { get; set; }
    public Guid CandidateId { get; set; }
    public virtual Candidate Candidate { get; set; }
    public string Description { get; set; }
    public string IssuerName { get; set; }
    public string Graduation { get; set; }
    public DateTime IssuedDate { get; set; } = DateTime.Today;
    public DateTime? ExpirationDate { get; set; }
    public string? DocumentLink { get; set; }
}