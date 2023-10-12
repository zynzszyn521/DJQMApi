using DbHelper.DbCon;
using DbHelper.Repository;
using DbHelper.Service;
using zyqmapi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<DapperFactory>();
builder.Services.AddScoped<APPService>();
builder.Services.AddScoped<APPRepository>();
builder.Services.AddScoped<SMSService>();
builder.Services.AddScoped<SMSRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserNoteService>();
builder.Services.AddScoped<UserNoteRepository>();
builder.Services.AddScoped<UserOrderService>();
builder.Services.AddScoped<UserOrderRepository>();
builder.Services.AddScoped<VideoService>();
builder.Services.AddScoped<VideoRepository>();
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<ArticleRepository>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderRepository>();

builder.Services.AddControllers(options =>
{
    //ÔÊÔS…¢”µžé¿Õ
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ILoggerProvider>(new MyLoggerProvider(AppContext.BaseDirectory));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
