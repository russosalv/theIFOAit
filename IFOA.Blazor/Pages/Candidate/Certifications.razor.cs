using IFOA.Blazor.Common;
using IFOA.DB.Entities;
using IFOA.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IFOA.Blazor.Pages.Candidate;

public partial class Certifications : DbPage
{
    [Parameter] public Guid? Id { get; set; }
    [Inject] NavigationManager Navigation { get; set; }
    
    public List<CandidateCertificationDto> CandidateCertifications= new();

    private async Task LoadData()
    {
        await using var _context = await DbContextFactory.CreateDbContextAsync();
        var dbData = await _context.Candidates.AsNoTracking().Include(x => x.CandidateCertifications)
            .FirstOrDefaultAsync(a => a.Id == Id);

        if (dbData is not null)
        {
            CandidateCertifications =
                dbData.CandidateCertifications.Select(x => (CandidateCertificationDto)x).ToList();
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

    private async Task AddCertification()
    {
        CandidateCertifications.Add(new CandidateCertificationDto());
    }

    private async void OnCommittedItemChanges()
    {
        StartLoading();
        await using var _context = await DbContextFactory.CreateDbContextAsync();
        var dbData = await _context.Candidates
            .Include(x => x.CandidateCertifications)
            .FirstOrDefaultAsync(a => a.Id == Id);

        if (dbData is not null)
        {
            dbData.CandidateCertifications.Clear();
            dbData.CandidateCertifications = CandidateCertifications
                .Select(x => (DB.Entities.CandidateCertification)x)
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