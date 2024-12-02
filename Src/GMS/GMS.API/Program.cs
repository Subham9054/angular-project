using GMS.Repository.Container;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add custom container configuration
builder.Services.AddCustomContainer(builder.Configuration);

// Add CORS policy to allow specific origin (e.g., Angular app on localhost:4200)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Apply the specific CORS policy
app.UseCors("AllowAllOrigins");

// Ensure HTTPS redirection is in place
app.UseHttpsRedirection();

// Use authorization middleware if needed
app.UseAuthorization();

// Map controllers to handle requests
app.MapControllers();

// Run the application
app.Run();
