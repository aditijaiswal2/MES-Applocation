using MES.Server.Contracts;
using MES.Server.Data;
using MES.Server.Data.Repositories;
using MES.Server.Services;
using MES.Shared.DTOs;
using MES.Shared.Entities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddRazorPages();
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<ProjectdbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<IReceivingDataRepository, ReceivingDataRepository>();
builder.Services.AddTransient<ReceivingDataRepository>();
builder.Services.AddTransient<IMESWorkCentersRepository, MESWorkCentersRepository>();
builder.Services.AddTransient<MESWorkCentersRepository>();
builder.Services.AddTransient<IIncomingInspection, IncomingInspectionRepository>();
builder.Services.AddTransient<IncomingInspectionRepository>();
builder.Services.AddTransient<IImageRepository, ImageRepository>();
builder.Services.AddTransient<ImageRepository>();
builder.Services.AddTransient<IFileRepository, FileRepository>();
builder.Services.AddTransient<FileRepository>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddTransient<IFinalInspectionImages, FinalImagesRepository>();
builder.Services.AddTransient<FinalImagesRepository>();
builder.Services.AddTransient<IShipmentImageRepository, ShipmentImageRepository>();
builder.Services.AddTransient<ShipmentImageRepository>();

builder.Services.AddTransient<IRotorsStyleRepository, RotorsStyleRepository>();
builder.Services.AddTransient<RotorsStyleRepository>();

builder.Services.AddTransient<ITypedetailsRepository, TypedetailsRepository>();
builder.Services.AddTransient<TypedetailsRepository>();

builder.Services.AddTransient<IOtherRepository, OtherRepository>();
builder.Services.AddTransient<OtherRepository>();

builder.Services.AddTransient<ISaddlePartNumberRepository, SaddlePartNumberRepository>();
builder.Services.AddTransient<SaddlePartNumberRepository>();

builder.Services.AddTransient<IMaterialRepository, MaterialRepository>();
builder.Services.AddTransient<MaterialRepository>();

// Database context configuration
builder.Services.AddDbContext<ProjectdbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MESDataConnection")));

// Add MudBlazor Snackbar service if needed (likely unnecessary)
builder.Services.AddMudServices();

// Optional: Add CORS for API access
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


builder.Services.AddScoped<ILogindetailrepository, Logindetailrepository>();
builder.Services.AddTransient<Logindetailrepository>();


// Optional: Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseDeveloperExceptionPage();

}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseBlazorFrameworkFiles();
app.UseAuthentication();
app.UseAuthorization();


app.UseRouting();

// Use CORS
app.UseCors("CorsPolicy");

// Map endpoints
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
