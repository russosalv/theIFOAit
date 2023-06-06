using System.Diagnostics;
using IFOA.Blazor.Common;
using IFOA.Shared;
using IFOA.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Netizine.Enums;

namespace IFOA.Blazor.Pages.Candidate;

public partial class CreateProfile : DbPage
{
    [Parameter] public Guid? Id { get; set; }
    CandidateDto CandidateDto = new();
    List<Country?> _countries = new List<Country?>();
    
    [Inject] NavigationManager Navigation { get; set; }
    protected override async Task OnInitializedAsync()
    {
        foreach (var country in EnumHelper.GetValues<Country>().ToList())
        {
            if(country != Country.NotSet)
                _countries.Add(country);
        }
        
        await using var _context = await DbContextFactory.CreateDbContextAsync();
        var dbData = await _context.Candidates.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        if (dbData is not null)
        {
            CandidateDto = (CandidateDto)dbData;
        }
    }

    private async Task SaveData()
    {
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
        Navigation.NavigateTo($"{Profile.PageUrl}/{CandidateDto.Id}");
    }
}