using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;
using NotTotalCommanderLib.Infrastructure.DirectoryContent;
using NotTotalCommanderLib.Infrastructure.Exceptions;
using NotTotalCommanderLib.Infrastructure.Routing;
using Radzen;
using Radzen.Blazor;

namespace NotTotalCommanderBlazor.Components;

public partial class DirectoryTable
{
    [Inject] public RouteService RouteService { get; set; }
    [Inject] public ContextMenuService ContextMenuService { get; set; }
    [Inject] public DialogService DialogService { get; set; }

    [Parameter] public Action<IList<DirectoryContentModel>> OnSelectChange { get; set; }

    private DirectoryContentModel[] DirectoryContent { get; set; }
    private bool IsRoot { get; set; }
    private bool IsCreatingNewDirectory { get; set; }
    private string ErrorText { get; set; }

    private string newDirectoryName;

    protected override void OnInitialized()
    {
        RefreshDirectoryContent();
        base.OnInitialized();
    }

    private void RefreshDirectoryContent()
    {
        var model = RouteService.GetCurrentDirectoryContent();
        DirectoryContent = model.DirectoryContent;
        IsRoot = model.IsRoot;
        StateHasChanged();
    }

    private void CreateDirectory()
    {
        try
        {
            RouteService.CreateDirectory(newDirectoryName);
            newDirectoryName = string.Empty;
            RefreshDirectoryContent();
            ToggleCreatingNewDirectory();
        }
        catch (CreateDirectoryValidationFailedException e)
        {
            ErrorText = e.Message;
        }
    }

    private void ChangeDirectoryDown(string name)
    {
        RouteService.DirectoryDown(name);
        RefreshDirectoryContent();
    }

    private void ChangeDirectoryUp()
    {
        RouteService.DirectoryUp();
        RefreshDirectoryContent();
    }

    private void DeleteDirectory(string name)
    {
        RouteService.DeleteDirectory(name);
        RefreshDirectoryContent();
    }

    private string GetFileIcon(DirectoryContentType directoryContentType)
    {
        return directoryContentType switch
        {
            DirectoryContentType.Drive => "album",
            DirectoryContentType.Directory => "folder",
            DirectoryContentType.File => "description",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void ToggleCreatingNewDirectory()
    {
        newDirectoryName = string.Empty;
        ErrorText = string.Empty;
        IsCreatingNewDirectory = !IsCreatingNewDirectory;
        StateHasChanged();
    }

    private void OnCellContextMenu(DataGridCellMouseEventArgs<DirectoryContentModel> args)
    {
        if (args.Data.DirectoryContentType == DirectoryContentType.Directory)
        {
            ContextMenuService.Open(args, new List<ContextMenuItem>
                {
                    new() { Text = "Удалить", Value = args.Data.Name, Icon = "home" },
                },
                (e) =>
                {
                    InvokeAsync(async () =>
                    {
                        var confirm = await DialogService.Confirm("Вы действительно хотите удалить папку?",
                            string.Empty,
                            new ConfirmOptions
                            {
                                OkButtonText = "Да",
                                CancelButtonText = "Отмена",
                                ShowTitle = false
                            });

                        if (confirm == true)
                            DeleteDirectory(e.Value.ToString() ?? string.Empty);
                    });

                    ContextMenuService.Close();
                });
        }
    }
}