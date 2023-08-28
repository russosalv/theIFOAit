using IFOA.DB;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace IFOA.Blazor.Common;

public class DbPage : ComponentBase
{
    [Inject] public IDbContextFactory<IfoaContext> DbContextFactory { get; set; }
    public bool Loading { get; set; } = true;

    private protected void StartLoading()
    {
        Loading = true;
        StateHasChanged();
    }

    private protected void EndLoading()
    {
        Loading = false;
        StateHasChanged();
    }
}