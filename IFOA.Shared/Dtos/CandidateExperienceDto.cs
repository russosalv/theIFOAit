using IFOA.DB.Entities;

namespace IFOA.Shared.Dtos;

public class CandidateExperienceDto : CandidateExperience
{
    public string ShortDescription => (Description is not null && Description?.Length > 50) ? Description.Substring(0, 50) + "..." : Description;

    public static CandidateExperienceDto FromCandidateExperience(CandidateExperience x)
    {
        return new CandidateExperienceDto()
        {
            Candidate = x.Candidate,
            CandidateId = x.CandidateId,
            Description = x.Description,
            Id = x.Id,
            CompanyName = x.CompanyName,
            FromDate = x.FromDate,
            JobTitle = x.JobTitle,
            ToDate = x.ToDate
        };
    }
    
    public static CandidateExperience ToCandidateExperience(CandidateExperienceDto x)
    {
        return new CandidateExperienceDto()
        {
            Candidate = x.Candidate,
            CandidateId = x.CandidateId,
            Description = x.Description,
            Id = x.Id,
            CompanyName = x.CompanyName,
            FromDate = x.FromDate,
            JobTitle = x.JobTitle,
            ToDate = x.ToDate
        };
    }
}