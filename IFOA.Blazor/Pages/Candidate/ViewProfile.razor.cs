using IFOA.Blazor.Common;
using IFOA.DB;
using IFOA.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace IFOA.Blazor.Pages.Candidate;

public partial class ViewProfile : DbPage
{
    [Parameter] public Guid? Id { get; set; }

    // [Inject] public PictureBlobStorageService PictureBlobStorageService { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }
    [Inject] public PictureBlobStorageService PictureBlobStorageService { get; set; } = null!;
    private bool Busy = true;
    
    public string ImageBase64 { get; set; } = string.Empty;
    
    private IFOA.DB.Entities.Candidate Candidate { get; set; } = new();
    

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender == false) return;

        if (Id is not null && Id != Guid.Empty)
        {
            await using var _context = await DbContextFactory.CreateDbContextAsync();
            var candidateFromDb = await _context.Candidates.FirstOrDefaultAsync(x => x.Id == Id);

            // if (candidateFromDb is null)
            // {
            //     Navigation.NavigateTo(EditProfile.PageUrl);
            // }

            // if (candidateFromDb is not null && string.IsNullOrEmpty(candidateFromDb.ImageLink) == false)
            // {
            //     ImageBase64 = await PictureBlobStorageService.GetBase64ImageAsync(candidateFromDb!.ImageLink);
            // }

            Candidate = candidateFromDb ?? new();
        }
        else
        {
            // Navigation.NavigateTo(EditProfile.PageUrl);
        }

        Busy = false;
        StateHasChanged();
    }
}