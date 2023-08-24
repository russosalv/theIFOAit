namespace IFOA.DB.Entities;

public class CandidateExperience
{
    public int Id { get; set; }
    public Guid CandidateId { get; set; }
    public virtual Candidate Candidate { get; set; }
    public string JobTitle { get; set; }
    public string Description { get; set; }
    public string CompanyName { get; set; }
    public DateTime FromDate { get; set; } = DateTime.Today;
    public DateTime? ToDate { get; set; }
}