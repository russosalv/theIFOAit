using IFOA.DB.Entities;

namespace IFOA.Shared.Dtos;

public class CandidateExperienceDto : CandidateExperience
{
    public string ShortDescription => (Description is not null && Description?.Length > 50) ? Description.Substring(0, 50) + "..." : Description;
}