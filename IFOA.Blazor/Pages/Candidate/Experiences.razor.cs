using IFOA.Blazor.Common;
using IFOA.DB.Entities;
using IFOA.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace IFOA.Blazor.Pages.Candidate;

public partial class Experiences : DbPage
{
    public List<CandidateExperienceDto> CandidateExperiences = new();
    [Parameter] public Guid? Id { get; set; }
    private DB.Entities.Candidate? Candidate { get; set; }

    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return base.OnAfterRenderAsync(firstRender);
        
        using var context = DbContextFactory.CreateDbContext();
        var candidate = context.Candidates.FirstOrDefault(x => x.Id == Id);
        if (candidate is not null)
        {
            Candidate = candidate;
            CandidateExperiences =
                candidate?.CandidateExperiences.ToList()?.Select(x => (CandidateExperienceDto)x).ToList() ?? new();
        }

        return base.OnAfterRenderAsync(firstRender);
    }

    private async Task AddExperience()
    {
        CandidateExperiences.Add(new CandidateExperienceDto()
        {
        });
    }
}