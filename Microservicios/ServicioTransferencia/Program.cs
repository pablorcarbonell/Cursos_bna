using Microsoft.EntityFrameworkCore;
using Common.DB;
using Common.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//-----para usar las listas desde la DB-----
//se configura el ConnectionString
builder.Services.AddDbContext<TransferenciaDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TransferenciasConection"));
});
builder.Services.AddDbContext<RespuestaDB>(options =>
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


#region Endpoints

app.MapGet("/transferencia/", async (ClienteDb db) =>
    await db.Clientes.ToListAsync());

app.MapGet("/transferencia/{id}", async (int id, ClienteDb db) =>
    await db.Clientes.FindAsync(id)
        is Cliente cliente
            ? Results.Ok(cliente)
            : Results.NotFound());


app.MapPost("/transferencia/", async (Transferencia Transferencia) =>
{
    if (Transferencia != null)
    db.transferencia.Add(Transferencia);
    await db.SaveChangesAsync();
    return Results.Created($"/Clientes/{cliente.id}", cliente);
});

app.MapPut("/Clientes/{id}", async (int id, Cliente inputCliente, ClienteDb db) =>
{
    var cliente = await db.Clientes.FindAsync(id);

    if (cliente is null) return Results.NotFound();

    cliente.nombre = inputCliente.nombre;
    cliente.apellido = inputCliente.apellido;
    cliente.cuil = inputCliente.cuil;
    cliente.nroDocumento = inputCliente.nroDocumento;
    cliente.tipoDocumento = inputCliente.tipoDocumento;
    cliente.esEmpleadoBNA = inputCliente.esEmpleadoBNA;
    cliente.paisOrigen = inputCliente.paisOrigen;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/clientes/{id}", async (int id, ClienteDb db) =>
{
    if (await db.Clientes.FindAsync(id) is Cliente cliente)
    {
        db.Clientes.Remove(cliente);
        await db.SaveChangesAsync();
        return Results.Ok(cliente);
    }
    return Results.NotFound();
});
#endregion



app.Run();