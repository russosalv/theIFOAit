using IFOA.Blazor.Common;
using IFOA.DB.Entities;
using IFOA.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace IFOA.Blazor.Pages.Candidate;

public partial class Experiences : DbPage
{
    [Parameter] public Guid? Id { get; set; }
    
    public List<CandidateExperienceDto> CandidateExperiences = new();

    private List<DB.Entities.CandidateExperience>? CandidateExperience { get; set; }

    private async Task LoadData()
    {
        
        await using var _context =await DbContextFactory.CreateDbContextAsync();
        var dbData = await _context.CandidateExperience.AsNoTracking()
            .Where(a =>  a.CandidateId == Id)
            .ToListAsync();
        
        if (dbData is not null)
        {
            CandidateExperiences =
                dbData.Select(x => (CandidateExperienceDto)x).ToList();
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
        CandidateExperiences.Add(new CandidateExperienceDto()
        {
            IsNewExperience = true
        });
    }

    private async void OnCommittedItemChanges()
    {
        StartLoading();
        await using var _context =await DbContextFactory.CreateDbContextAsync();
        var dbData = await _context.CandidateExperience.AsNoTracking()
            .Where(a =>  a.CandidateId == Id)
            .ToListAsync();
        
        var toBeUpdated = CandidateExperiences.Where(a => !a.IsNewExperience).ToList();
        foreach (var experience in toBeUpdated)
        {
            var existingExperience = dbData.FirstOrDefault(x => x.Id == experience.Id);
            if (existingExperience != null)
            {
                existingExperience.JobTitle = experience.JobTitle;
                existingExperience.Description = experience.Description;
                existingExperience.CompanyName = experience.CompanyName;
                existingExperience.FromDate = experience.FromDate;
                existingExperience.ToDate = experience.ToDate;
                _context.Entry(existingExperience).State = EntityState.Modified;
            }
        }
        var toBeAdded = CandidateExperiences.Where(a => a.IsNewExperience)
            .Select(CandidateExperienceDto.ToCandidateExperience)
            .ToList();
        _context.CandidateExperience.AddRange(toBeAdded);

        await _context.SaveChangesAsync();
        EndLoading();
    }
    
    
    
            
}