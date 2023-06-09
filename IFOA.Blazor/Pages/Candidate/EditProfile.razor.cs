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
    List<CandidateSpokenLanguage> CandidateSpokenLanguages = new List<CandidateSpokenLanguage>();
    
    List<Country?> _countries = new List<Country?>();
    List<Language> _languages = new List<Language>();
    IEnumerable<Language> _selectedLanguages = new List<Language>();

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
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == Id);
        if (dbData is not null)
        {
            CandidateDto = (CandidateDto)dbData;
            CandidateSpokenLanguages = dbData.CandidateSpokenLanguages.ToList();
            // _selectedLanguages = _languages
            //     .Where(x => dbData.CandidateSpokenLanguages
            //         .Select(spokenLanguage => spokenLanguage.LanguageCode)
            //         .Contains(x.Alpha2)).ToList();
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

    private Task AddToSpokenLanguage()
    {
        foreach (var selectedLanguage in _selectedLanguages)
        {
            if(CandidateSpokenLanguages.Any(x => x.LanguageCode == selectedLanguage.Alpha2))
                continue;

            CandidateSpokenLanguages.Add(new CandidateSpokenLanguage()
            {
                LanguageName = selectedLanguage.Name,
                LanguageCode = selectedLanguage.Alpha2
            });
        }

        return Task.CompletedTask;
    }
}