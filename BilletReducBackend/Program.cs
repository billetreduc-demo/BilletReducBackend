using BilletReducBackend.Services.Plays;

DotNetEnv.Env.Load();

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Builder variable used for dependency injection and configuation
var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IPlayService, PlayService>();
    
    var baseClientUrl = Environment.GetEnvironmentVariable("BASE_CLIENT_URL");
    
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: myAllowSpecificOrigins,
            policy =>
            {
                policy.WithOrigins(baseClientUrl ?? "")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
    });
}

// App variable used to manage the request pipeline
var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseCors(myAllowSpecificOrigins);
    app.MapControllers();
    app.Run();
}
