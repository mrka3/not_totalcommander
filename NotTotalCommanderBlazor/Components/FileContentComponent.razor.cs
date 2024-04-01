using Microsoft.AspNetCore.Components;
using NotTotalCommanderLib.Infrastructure.FileContent;

namespace NotTotalCommanderBlazor.Components;

public partial class FileContentComponent
{
    [Parameter]
    public FileContentModel? FileContentModel { get; set; }
}