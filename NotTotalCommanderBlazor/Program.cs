using NotTotalCommanderLib.Infrastructure.Directories;
using NotTotalCommanderLib.Infrastructure.DirectoryContent;
using NotTotalCommanderLib.Infrastructure.Drives;
using NotTotalCommanderLib.Infrastructure.FileContent;
using NotTotalCommanderLib.Infrastructure.Files;
using NotTotalCommanderLib.Infrastructure.Routing;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<DirectoryContentService>();
builder.Services.AddScoped<DirectoryService>();
builder.Services.AddScoped<DriveService>();
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<PathService>();
builder.Services.AddScoped<RouteService>();
builder.Services.AddScoped<FileContentService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<DirectoryValidator>();
builder.Services.AddRadzenComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();