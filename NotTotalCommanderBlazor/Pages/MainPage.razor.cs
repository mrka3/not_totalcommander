using Microsoft.AspNetCore.Components;
using NotTotalCommanderLib.Infrastructure.DirectoryContent;
using NotTotalCommanderLib.Infrastructure.FileContent;
using NotTotalCommanderLib.Infrastructure.Routing;

namespace NotTotalCommanderBlazor.Pages;

public partial class MainPage
{
    [Inject] public RouteService RouteService { get; set; }
    private FileContentModel? FileContent { get; set; }

    void HandleChangeSelect(IList<DirectoryContentModel> selected)
    {
        FileContent = RouteService.GetFileContent(selected.Single().Name, true);
        StateHasChanged();
    }
}