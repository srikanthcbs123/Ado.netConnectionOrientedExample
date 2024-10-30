using Ado.netConnectionOrientedExample;

var builder = WebApplication.CreateBuilder(args);

//Section1: Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeServices,EmployeeServices>();
builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();

var app = builder.Build();

//Section2: Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
