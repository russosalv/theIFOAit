using IFOA.Blazor.Common;
using IFOA.DB;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace IFOA.Blazor.Pages.Candidate;

public partial class Profile : DbPage
{
    [Parameter] public Guid? Id { get; set; }

    // [Inject] public PictureBlobStorageService PictureBlobStorageService { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }
    private bool Busy = true;
    
    private IFOA.DB.Entities.Candidate Candidate { get; set; } = new();
    

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender == false) return;

        if (Id is not null && Id != Guid.Empty)
        {
            await using var _context = await DbContextFactory.CreateDbContextAsync();
            var candidateFromDb = await _context.Candidates.FirstOrDefaultAsync(x => x.Id == Id);

            if (candidateFromDb is null)
            {
                Navigation.NavigateTo(CreateProfile.PageUrl);
            }

            Candidate = candidateFromDb ?? new();
        }
        else
        {
            Navigation.NavigateTo(CreateProfile.PageUrl);
        }

        Busy = false;
        StateHasChanged();
    }
}