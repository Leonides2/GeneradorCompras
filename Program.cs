using GeneradorCompras.Jobs;
using GeneradorCompras.Models;
using GeneradorCompras.Models.Interface;
using GeneradorCompras.Models.Service;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Proyecto.db"));

//builder.Services.AddEventStoreClient(new Uri(builder.Configuration.GetValue<string>("EventStore")));

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICompraGenerator, CompraGenerator>();

builder.Services.AddQuartz(q =>
{
    // Just use the name of your job that you created in the Jobs folder.
    var jobKey = new JobKey("SendEmailJob");
    q.AddJob<PurchaseGenerator>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("SendEmailJob-trigger")
        //This Cron interval can be described as "run every minute" (when second is zero)
        .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever()).WithIdentity("a")

    ); ;
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


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
