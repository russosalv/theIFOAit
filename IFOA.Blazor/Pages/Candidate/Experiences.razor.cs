using IFOA.Blazor.Common;
using IFOA.DB.Entities;
using IFOA.Shared.Dtos;

namespace IFOA.Blazor.Pages.Candidate;

public partial class Experiences : DbPage
{
    public List<CandidateExperienceDto> CandidateExperiences = new();
    private async Task AddExperience()
    {
        CandidateExperiences.Add(new CandidateExperienceDto()
        {
            
        });
    }
}