using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TappitSports.DataContext;
using TappitSports.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ISportDataContext, SportDataContext>(
        o => o.UseNpgsql(builder.Configuration.GetConnectionString("FavouriteSportsDb"))
);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Favourite Sports", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var AllowAllCorsPolicy = "_allowAllCorsPolicy";

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddCors(options =>
    {
        options.AddPolicy(AllowAllCorsPolicy,
            policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Favourite Sports V1"));
app.UseCors(AllowAllCorsPolicy);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
