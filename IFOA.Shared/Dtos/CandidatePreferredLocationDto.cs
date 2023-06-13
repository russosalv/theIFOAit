using IFOA.DB.Entities;
using Netizine.Enums;

namespace IFOA.Shared.Dtos;

public class CandidatePreferredLocationDto : FeEntity
{
    public int Id { get; set; }
    public Guid CandidateId { get; set; }
    public Country? Country { get; set; } 
    public string? City { get; set; }
    
    public static explicit operator CandidatePreferredLocationDto(CandidatePreferredLocation x)
    {
        //var countries = EnumHelper.GetValues<Country>().ToList();

        return new CandidatePreferredLocationDto()
        {
            FeId = x.Id.ToString(),
            Id = x.Id,
            CandidateId = x.CandidateId,
            City = x.City,
            Country = x.Country
        };
    }


    public static explicit operator CandidatePreferredLocation(CandidatePreferredLocationDto x)
    {
        return new CandidatePreferredLocation()
        {
            Id = x.Id,
            CandidateId = x.CandidateId,
            City = x.City,
            Country = (Country)x.Country!
        };
    }
}