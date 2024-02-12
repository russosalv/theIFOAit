using IFOA.DB.Entities;
using Netizine.Enums;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components.Forms;

namespace IFOA.Shared.Dtos;

public class CandidateDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public GenderEnum Gender { get; set; } = GenderEnum.Male;
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime? BirthDate { get; set; }
    public Country? Nationality { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public Country? ResidenceCountry { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    public int Ranking { get; set; } = 0;
    public string? ImageLink { get; set; }
    public string? CoverLetter { get; set; } = string.Empty;
    public string? Biography { get; set; }
    
    public IBrowserFile? ImageFile { get; set; } = null!;
    public bool IsImageFileChanged { get; set; } = false;

    public List<CandidateSpokenLanguageDto>? CandidateSpokenLanguages { get; set; } = new List<CandidateSpokenLanguageDto>();

    public List<CandidatePreferredLocationDto>? CandidatePreferredLocations { get; set; } =
        new List<CandidatePreferredLocationDto>();

    public static explicit operator Candidate(CandidateDto x)
    {
        return new Candidate()
        {
            Id = x.Id,
            Gender = x.Gender,
            Name = x.Name,
            Surname = x.Surname,
            BirthDate = x.BirthDate,
            Nationality = x.Nationality ?? Country.NotSet,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            ResidenceCountry = x.ResidenceCountry ?? Country.NotSet,
            Address = x.Address,
            City = x.City,
            ZipCode = x.ZipCode,
            Ranking = x.Ranking,
            ImageLink = x.ImageLink,
            CoverLetter = x.CoverLetter,
            Biography = x.Biography,
        };
    }


    public static explicit operator CandidateDto(Candidate x)
    {
        return new CandidateDto()
        {
            Id = x.Id,
            Gender = x.Gender,
            Name = x.Name,
            Surname = x.Surname,
            BirthDate = x.BirthDate,
            Nationality = x.Nationality == Country.NotSet ? null : x.Nationality,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            ResidenceCountry = x.Nationality == Country.NotSet ? null : x.Nationality,
            Address = x.Address,
            City = x.City,
            ZipCode = x.ZipCode,
            Ranking = x.Ranking,
            ImageLink = x.ImageLink,
            CoverLetter = x.CoverLetter,
            Biography = x.Biography,
        };
    }
}