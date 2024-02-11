using IFOA.DB.Entities;

namespace IFOA.Shared.Dtos;

public class CandidateCertificationDto : FeEntity
{
    public int Id { get; set; }
    public Guid CandidateId { get; set; }
    public string Description { get; set; }
    public string IssuerName { get; set; }
    public string Graduation { get; set; }
    public DateTime IssuedDate { get; set; } = DateTime.Today;
    public DateTime? ExpirationDate { get; set; }
    public string? DocumentLink { get; set; }


    public static explicit operator CandidateCertificationDto(CandidateCertification x)
    {
        return new CandidateCertificationDto
        {
            Id = x.Id,
            CandidateId = x.CandidateId,
            Description = x.Description,
            IssuerName = x.IssuerName,
            Graduation = x.Graduation,
            IssuedDate = x.IssuedDate,
            ExpirationDate = x.ExpirationDate,
            DocumentLink = x.DocumentLink
        };
    }
    
    public static explicit operator CandidateCertification(CandidateCertificationDto x)
    {
        return new CandidateCertification
        {
            Id = x.Id,
            CandidateId = x.CandidateId,
            Description = x.Description,
            IssuerName = x.IssuerName,
            Graduation = x.Graduation,
            IssuedDate = x.IssuedDate,
            ExpirationDate = x.ExpirationDate,
            DocumentLink = x.DocumentLink
        };
    }
}