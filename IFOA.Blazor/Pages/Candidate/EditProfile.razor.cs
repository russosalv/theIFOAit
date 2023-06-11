using System.Diagnostics;
using IFOA.Blazor.Common;
using IFOA.DB.Entities;
using IFOA.Shared;
using IFOA.Shared.Dtos;
using ISOLib.ISO;
using ISOLib.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Country = Netizine.Enums.Country;

namespace IFOA.Blazor.Pages.Candidate;

public partial class EditProfile : DbPage
{
    [Parameter] public Guid? Id { get; set; }
    CandidateDto CandidateDto = new();
    List<CandidateSpokenLanguageDto> CandidateSpokenLanguages = new List<CandidateSpokenLanguageDto>();
    List<CandidatePreferredJobFunction> CandidatePreferredJobFunctions = new List<CandidatePreferredJobFunction>();
    List<CandidatePreferredLocation> CandidatePreferredLocations = new List<CandidatePreferredLocation>();

    List<Country?> _countries = new List<Country?>();
    List<Language> _languages = new List<Language>();
    List<JobFunction> _jobFunctions = new List<JobFunction>();

    IEnumerable<Language> _selectedLanguages = new List<Language>();
    IEnumerable<JobFunction> _selectedJobFunctions = new List<JobFunction>();
    IEnumerable<Country?> _selectedCountry = new List<Country?>();

    [Inject] NavigationManager Navigation { get; set; }

    protected override async Task OnInitializedAsync()
    {
        foreach (var country in EnumHelper.GetValues<Country>().ToList().Where(country => country != Country.NotSet))
        {
            _countries.Add(country);
        }

        ISO<Language> iso639 = new ISO639();
        _languages = iso639.GetArray().ToList();

        await using var _context = await DbContextFactory.CreateDbContextAsync();
        var dbData = await _context.Candidates.AsNoTracking()
            .Include(x => x.CandidateSpokenLanguages)
            .Include(x => x.CandidatePreferredJobFunctions)
            .ThenInclude(x => x.JobFunction)
            .Include(x => x.CandidatePreferredLocations)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == Id);
        if (dbData is not null)
        {
            CandidateDto = (CandidateDto)dbData;
            CandidateSpokenLanguages =
                dbData.CandidateSpokenLanguages
                    .Select(x => (CandidateSpokenLanguageDto)x).ToList();

            CandidatePreferredJobFunctions = dbData.CandidatePreferredJobFunctions.ToList();
            CandidatePreferredLocations = dbData.CandidatePreferredLocations.ToList();
        }

        var jobFunctions = await _context.JobFunctions.AsNoTracking().ToListAsync();
    }

    private async Task SaveData()
    {
        await form.Validate();

        if (form.IsValid is false)
            return;

        await using var _context = await DbContextFactory.CreateDbContextAsync();
        var dbData = await _context?.Candidates.AsNoTracking().FirstOrDefaultAsync(x => x.Id == CandidateDto.Id)!;
        if (dbData is null)
        {
            _context?.Candidates.Add((DB.Entities.Candidate)CandidateDto);
        }
        else
        {
            _context?.Candidates.Update((DB.Entities.Candidate)CandidateDto);
        }

        await _context?.SaveChangesAsync(CancellationToken.None)!;
        Navigation.NavigateTo($"{ViewProfile.PageUrl}/{CandidateDto.Id}");
    }
}