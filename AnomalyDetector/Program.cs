
using AnomalyDetector.constants;
using AnomalyDetector.Options;
using AnomalyDetector.Services.GitSuspicionDetectors;
using AnomalyDetector.Services.NotiferService;
using AnomalyDetector.Services.ValidationsFactory;
using AnomalyDetector.Services.WebhookHandlerService;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddKeyedSingleton<ISuspicionDetector, GitSuspiciousRepositoryDetector>(KeyedServices.GitRepositoryDetector);
builder.Services.AddKeyedSingleton<ISuspicionDetector, GitSuspiciousTeamsDetector>(KeyedServices.GitTeamsDetector);
builder.Services.AddKeyedSingleton<ISuspicionDetector, GitSuspiciousPushesDetector>(KeyedServices.GitPushesDetector);
builder.Services.AddSingleton<IValidationFactory, ValidationFactory>();
builder.Services.AddSingleton<IWebhookHandler, WebhookHandler>();
builder.Services.AddSingleton<INotifier, Notifier>();


builder.Services.Configure<PushTimeOptions>(Configuration.GetSection("SuspiciousBehaviors:Repository:PushTime"));
builder.Services.Configure<RepositoryOptions>(Configuration.GetSection("SuspiciousBehaviors:Repository"));
builder.Services.Configure<TeamOptions>(Configuration.GetSection("SuspiciousBehaviors:Team"));


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

