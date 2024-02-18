using IFOA.DB.Entities;

namespace IFOA.Shared.Dtos;

public class CandidateProjectsDto : FeEntity
{
    public int Id { get; set; }
    public Guid CandidateId { get; set; }
    public string Description { get; set; }
    public string CompanyName { get; set; }
    
    public DateTime FromDate { get; set; } = DateTime.Now;
    public DateTime? ToDate { get; set; }

    public static explicit operator CandidateProjectsDto(CandidateProject x)
    {
        return new CandidateProjectsDto
        {
            Id = x.Id,
            CandidateId = x.CandidateId,
            Description = x.Description,
            CompanyName = x.CompanyName,
            FromDate = x.FromDate,
            ToDate = x.ToDate
        };
    }

    public static explicit operator CandidateProject(CandidateProjectsDto x)
    {
        return new CandidateProject
        {
            Id = x.Id,
            CandidateId = x.CandidateId,
            Description = x.Description,
            CompanyName = x.CompanyName,
            FromDate = x.FromDate,
            ToDate = x.ToDate
        };
    }
}