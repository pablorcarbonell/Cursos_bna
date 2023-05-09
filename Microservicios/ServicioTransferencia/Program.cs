using Common.DB;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ServicioTransferencia;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<TransferenciasDb>(opt => opt.UseInMemoryDatabase("Transferencias"));
//-----para usar las listas desde la DB-----
//se configura el ConnectionString
builder.Services.AddDbContext<TransferenciasDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TransferenciasConection"));
});

//-----para usar las listas en memoria-----
//builder.Services.AddDbContext<ClienteDb>(opt => opt.UseInMemoryDatabase("ClienteList"));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/transferencias/", async (TransferenciasDb db) =>
{
    TranferenciasManager.Validar();
    Log.Debug("Listando Transferencias");
    await db.Transferencias.ToListAsync();
    });

app.MapGet("/transferencias/{id}", async (int id, TransferenciasDb db) =>
    await db.Transferencias.FindAsync(id)
        is Transferencia transf
            ? Results.Ok(transf)
            : Results.NotFound());

app.MapPost("/transferencia/", async (Transferencia Transferencia, TransferenciasDb db) =>
{
    Log.Debug("Listando Transferencias");
    if (Transferencia != null)
        db.Transferencias.Add(Transferencia);
    await db.SaveChangesAsync();
    return Results.Created($"/Clientes/{Transferencia.id}", Transferencia.Respuesta);
});


app.Run();