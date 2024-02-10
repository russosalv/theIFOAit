using IFOA.DB.Entities;

namespace IFOA.Shared.Dtos;

public class CandidateExperienceDto : FeEntity
{
    public int Id { get; set; }
    public Guid CandidateId { get; set; }
    public string Description { get; set; }
    public string CompanyName { get; set; }
    public DateTime FromDate { get; set; } = DateTime.Today;
    public string JobTitle { get; set; }
    public DateTime? ToDate { get; set; }


    public string ShortDescription => (Description is not null && Description?.Length > 50)
        ? Description.Substring(0, 50) + "..."
        : Description;

    public static explicit operator  CandidateExperienceDto(CandidateExperience x)
    {
        return new CandidateExperienceDto()
        {
            CandidateId = x.CandidateId,
            Description = x.Description,
            Id = x.Id,
            CompanyName = x.CompanyName,
            FromDate = x.FromDate,
            JobTitle = x.JobTitle,
            ToDate = x.ToDate
        };
    }

    public static explicit operator  CandidateExperience(CandidateExperienceDto x)
    {
        return new CandidateExperience()
        {
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