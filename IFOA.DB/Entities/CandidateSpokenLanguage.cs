namespace IFOA.DB.Entities;

public class CandidateSpokenLanguage
{
    public int Id { get; set; }
    public Guid CandidateId { get; set; }
    public virtual Candidate Candidate { get; set; }
    public string LanguageCode { get; set; } = string.Empty;

    public CEFRLevelEnum ReadingCefrLevel { get; set; } = CEFRLevelEnum.A1;
    public CEFRLevelEnum WritingCefrLevel { get; set; } = CEFRLevelEnum.A1;
    public CEFRLevelEnum SpeakingCefrLevel { get; set; } = CEFRLevelEnum.A1;
    public CEFRLevelEnum ListeningCefrLevel { get; set; } = CEFRLevelEnum.A1;
}

public enum CEFRLevelEnum
{
    A1,
    A2,
    B1,
    B2,
    C1,
    C2
}