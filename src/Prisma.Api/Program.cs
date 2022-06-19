using Hangfire;
using Prisma.Api.Services;
using Prisma.Application.Torrent;
using Prisma.Core;
using Prisma.Providers.Yts;

var builder = WebApplication.CreateBuilder(args);

// Add Hangfire services.
builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer()
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseInMemoryStorage();
});

builder.Services.AddHangfireServer();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Prisma services.
builder.Services.AddScoped<IMediaProvider<Media>, YtsProvider>();
builder.Services.AddScoped<IMediaStorageService, MediaStorageService>();

builder.Services.AddSingleton<TorrentClient>();

builder.Services.AddHttpClient<IMediaStorageService, MediaStorageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
