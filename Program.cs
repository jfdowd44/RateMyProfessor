using RateMyProfessor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();          
builder.Services.AddServerSideBlazor();    
builder.Services.AddSingleton<JsonFileProfessorService>(); 

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapRazorPages();

app.Run();
