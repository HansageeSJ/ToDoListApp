using ToDoListApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{

});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IToDoListService, ToDoListService>();

const string AllowLocalUi = "AllowLocalUi";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(AllowLocalUi, p =>
        p.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(AllowLocalUi);
app.MapControllers();

app.Run();