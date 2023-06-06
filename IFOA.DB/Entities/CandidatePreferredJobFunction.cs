namespace IFOA.DB.Entities;

public class CandidatePreferredJobFunction
{
    public int Id { get; set; }
    public Guid CandidateId { get; set; }
    public virtual Candidate Candidate { get; set; }
    public int JobFunctionId { get; set; }
    public virtual JobFunction JobFunction { get; set; }
}