using IFOA.Blazor.Common;
using IFOA.DB.Entities;
using IFOA.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IFOA.Blazor.Pages.Candidate;

public partial class Experiences : DbPage
{
    [Parameter] public Guid? Id { get; set; }
    [Inject] NavigationManager Navigation { get; set; }

    public List<CandidateExperienceDto> CandidateExperiences = new();

    private async Task LoadData()
    {
        await using var _context = await DbContextFactory.CreateDbContextAsync();
        var dbData = await _context.Candidates.AsNoTracking().Include(x => x.CandidateExperiences)
            .FirstOrDefaultAsync(a => a.Id == Id);

        if (dbData is not null)
        {
            CandidateExperiences =
                dbData.CandidateExperiences.Select(x => (CandidateExperienceDto)x).ToList();
        }

        EndLoading();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadData();
        }
    }

    private async Task AddExperience()
    {
        CandidateExperiences.Add(new CandidateExperienceDto());
    }

    private async void OnCommittedItemChanges()
    {
        StartLoading();
        await using var _context = await DbContextFactory.CreateDbContextAsync();
        var dbData = await _context.Candidates
            .Include(x => x.CandidateExperiences)
            .FirstOrDefaultAsync(a => a.Id == Id);

        if (dbData is not null)
        {
            dbData.CandidateExperiences.Clear();
            dbData.CandidateExperiences = CandidateExperiences
                .Select(x => (DB.Entities.CandidateExperience)x)
                .ToList();
        }

        await _context.SaveChangesAsync();
        EndLoading();
    }

    private async void OnProfileClicked()
    {
        Navigation.NavigateTo($"{ViewProfile.PageUrl}");
    }
}