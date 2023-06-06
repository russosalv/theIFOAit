using IFOA.DB;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace IFOA.Blazor.Common;

public class DbPage : ComponentBase
{
    [Inject] public IDbContextFactory<IfoaContext> DbContextFactory { get; set; }
}