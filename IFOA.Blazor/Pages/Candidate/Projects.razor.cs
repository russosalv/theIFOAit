using IFOA.Blazor.Common;
using IFOA.DB.Entities;
using IFOA.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IFOA.Blazor.Pages.Candidate;

public partial class Projects : DbPage
{
    [Parameter] public Guid? Id { get; set; }
    [Inject] NavigationManager Navigation { get; set; }

    public List<CandidateProjectsDto> CandidateProjects = new();

    private async Task LoadData()
    {
        await using var _context = await DbContextFactory.CreateDbContextAsync();
        var dbData = await _context.Candidates.AsNoTracking().Include(x => x.CandidateProjects)
            .FirstOrDefaultAsync(a => a.Id == Id);

        if (dbData is not null)
        {
            CandidateProjects =
                dbData.CandidateProjects.Select(x => (CandidateProjectsDto)x).ToList();
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

    private async Task AddProject()
    {
        CandidateProjects.Add(new CandidateProjectsDto());
    }

    private async void OnCommittedItemChanges()
    {
        StartLoading();
        await using var _context = await DbContextFactory.CreateDbContextAsync();
        var dbData = await _context.Candidates
            .Include(x => x.CandidateProjects)
            .FirstOrDefaultAsync(a => a.Id == Id);

        if (dbData is not null)
        {
            dbData.CandidateProjects.Clear();
            dbData.CandidateProjects = CandidateProjects
                .Select(x => (DB.Entities.CandidateProject)x)
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