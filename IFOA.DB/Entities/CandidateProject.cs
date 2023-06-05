namespace IFOA.DB.Entities;

public class CandidateProject
{
    public int Id { get; set; }
    public int CandicateId { get; set; }
    public virtual Candidate Candidate { get; set; }
    public string Description { get; set; }
    public string CompanyName { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}