using Microsoft.EntityFrameworkCore;
using PurchaseGallery.ApiService.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
// Add Controllers
builder.Services.AddControllers();



// Connect to Server for DB
builder.Services.AddDbContext<PurchaseGalleryDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();



var app = builder.Build();

//// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PurchaseGallery.ApiService v1"));

    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();




 app.MapDefaultEndpoints();

app.Run();

