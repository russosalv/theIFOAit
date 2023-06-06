namespace IFOA.DB.Entities;

public class CandidateSpokenLanguage
{
    public int Id { get; set; }
    public Guid CandidateId { get; set; }
    public virtual Candidate Candidate { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
}