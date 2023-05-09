using Common;
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServicioTransferenciasConection"));
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
    Log.Debug("Listando Transferencias");
    await db.Transferencias.ToListAsync();
    });


app.MapGet("/respuestas/", async (TransferenciasDb db) =>
{
    Log.Debug("Listando Respuestas");
    await db.Respuestas.ToListAsync();
});

app.MapGet("/transferencias/{id}", async (int id, TransferenciasDb db) =>
    await db.Transferencias.FindAsync(id)
        is Transferencia transf
            ? Results.Ok(transf)
            : Results.NotFound());


app.MapGet("/transferenciasExitosas/", async (TransferenciasDb db) =>
    await db.Transferencias.Where(t => t.respuesta.resultado == "FINALIZADA").ToListAsync()
        is List<Transferencia> lista
            ? Results.Ok(lista)
            : Results.NotFound() 
);


app.MapPost("/transferencias/", async (Transferencia transferencia, TransferenciasDb db) =>
{
    Log.Information("Procesando Transferencia");

    bool validado = await Manager.validarCuitBna(transferencia.cuilOriginante, transferencia.cuilDestinatario);
    if (transferencia != null) // && validado)
    { db.Transferencias.Add(transferencia);
        Respuesta respuesta = new Respuesta()
        {
            idTransferencia = transferencia.id,
            resultado = "OPERACION EXITOSA",
            importe = transferencia.importe,
            cuentaOrigen = transferencia.cbuOrigen,
            cuentaDestino = transferencia.cbuDestino,
        };        
    }
    await db.SaveChangesAsync();
   
    return Results.Created($"/transferencias/{transferencia.id}", transferencia.respuesta);
});


app.Run();