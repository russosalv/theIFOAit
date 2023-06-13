using System.Diagnostics;
using IFOA.Blazor.Common;
using IFOA.Blazor.Helpers;
using IFOA.DB.Entities;
using IFOA.Shared;
using IFOA.Shared.Dtos;
using ISOLib.ISO;
using ISOLib.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using Country = Netizine.Enums.Country;

namespace IFOA.Blazor.Pages.Candidate;

public partial class EditProfile : DbPage
{
    [Parameter] public Guid? Id { get; set; }
    CandidateDto CandidateDto = new();
    List<CandidateSpokenLanguageDto> CandidateSpokenLanguages = new List<CandidateSpokenLanguageDto>();
    List<CandidatePreferredLocationDto> CandidatePreferredLocations = new List<CandidatePreferredLocationDto>();

    List<Country?> _countriesNullable = new List<Country?>();
    List<Language> _languages = new List<Language>();
    List<JobFunction> _jobFunctions = new List<JobFunction>();
    List<JobFunctionChip> _jobFunctionChips = new List<JobFunctionChip>();

    IEnumerable<Language> _selectedLanguages = new List<Language>();
    IEnumerable<JobFunction> _selectedJobFunctions = new List<JobFunction>();
    IEnumerable<Country?> _selectedCountry = new List<Country?>();

    [Inject] NavigationManager Navigation { get; set; }

    protected override async Task OnInitializedAsync()
    {
        foreach (var country in EnumHelper.GetValues<Country>().ToList().Where(country => country != Country.NotSet))
        {
            _countriesNullable.Add(country);
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

            CandidatePreferredLocations = dbData.CandidatePreferredLocations
                .Select(x => (CandidatePreferredLocationDto)x).ToList();
        }

        var jobFunctions = await _context.JobFunctions.AsNoTracking().ToListAsync();

        foreach (var jobFunction in jobFunctions)
        {
            var isSelected = dbData?.CandidatePreferredJobFunctions.ToList()
                .FirstOrDefault(x => x.JobFunctionId == jobFunction.Id) is not null;

            _jobFunctionChips.Add(new JobFunctionChip()
            {
                JobFunction = jobFunction,
                IsSelected = isSelected
            });
        }
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

    public class JobFunctionChip
    {
        public JobFunction JobFunction { get; set; }
        public bool IsSelected { get; set; }

        public Color Color => IsSelected ? Color.Primary : Color.Dark;

        public string Icon =>
            IsSelected ? Icons.Material.Filled.Check : GraphicHelpers.GetMudIconByName(
                JobFunction.Icon,
                Icons.Material.Filled.NotInterested,
                includeRounded: true);
    }
}