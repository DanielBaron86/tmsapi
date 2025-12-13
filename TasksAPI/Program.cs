using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;
using System.Text;
using TasksAPI.Configuration;
using TasksAPI.DataBaseContext;
using TasksAPI.Interfaces;
using TasksAPI.Services;



//Define Loggins Options
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/tasksapi.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

//Add Mapping Service 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//Add Authentification support
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentification:Issuer"],
        ValidAudience = builder.Configuration["Authentification:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentification:SecretForkey"]))
    };
});


// Restrict accepted heaers types to  application/json and application/xml
builder.Services.AddControllers(controllerOptions =>
{
    controllerOptions.ReturnHttpNotAcceptable = false;
}).AddNewtonsoftJson()
  .AddXmlDataContractSerializerFormatters();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

SwaggerControllerOrder<ControllerBase> swaggerControllerOrder = new SwaggerControllerOrder<ControllerBase>(Assembly.GetEntryAssembly());

builder.Services.AddSwaggerGen(swaggerOptions =>
{
    //Fixes Swagger Examples for PATCH
    swaggerOptions.DocumentFilter<JsonPatchDocumentFilter>();

    var xmlCommentFiles = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFiles);
    swaggerOptions.IncludeXmlComments(xmlFullPath);
    swaggerOptions.OrderActionsBy((apiDesc) => $"{swaggerControllerOrder.SortKey(apiDesc.ActionDescriptor.RouteValues["controller"])}");



    swaggerOptions.AddSecurityDefinition("TSMAPIBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access API"
    }
        );

    swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "TSMAPIBearerAuth" }
            }, new List<string>()
        }
    });

});


//Add Database Connection

var DBServer = builder.Configuration["DataBaseType"] ?? "MySql";

var HOST = builder.Configuration["DBHost"];
var USER = builder.Configuration["DBUser"];
var PASSWORD = builder.Configuration["DBPassword"];
var DATABASE = builder.Configuration["DBName"];
string DATABASEPORT;

if (DBServer == "MySql")
{
    DATABASEPORT = builder.Configuration["DBPort"] ?? "3306";
    var mySqLconnectionString = $"server={HOST},{DATABASEPORT};user={USER};password={PASSWORD};database={DATABASE}";
    builder.Services.AddDbContextPool<DatabaseConnectContext>(options =>
    {

        options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        options.UseMySql(
                  mySqLconnectionString,
                  ServerVersion.AutoDetect(mySqLconnectionString),
                  options => options.EnableRetryOnFailure(
                      maxRetryCount: 5,
                      maxRetryDelay: System.TimeSpan.FromSeconds(30),
                      errorNumbersToAdd: null)
                  );
        
    });
}
else
{
    DATABASEPORT = builder.Configuration["DBPort"] ?? "1433";
    var MicrosoftSQLconnectionString = @$"Server={HOST},{DATABASEPORT};Database={DATABASE};User Id={USER};Password={PASSWORD};Trust Server Certificate=True;Connect Timeout=30;Application Intent=ReadWrite;Multi Subnet Failover=False";
    builder.Services.AddDbContextPool<DatabaseConnectContext>(options =>
    {
        options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        options.UseSqlServer(MicrosoftSQLconnectionString,
                            options => options.EnableRetryOnFailure(
                                      maxRetryCount: 5,
                                      maxRetryDelay: System.TimeSpan.FromSeconds(30),
                                      errorNumbersToAdd: null)
            );
    });
}




//Add API Version Support
builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    setupAction.ReportApiVersions = true;

});

builder.Services.AddVersionedApiExplorer(o =>
{
    o.GroupNameFormat = "'v'VVV";
    o.SubstituteApiVersionInUrl = true;
});

//Add Custom Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILocationService, LocationServices>();
builder.Services.AddScoped<IGoodsServices, GoodsServices>();
builder.Services.AddScoped<ITasksService, TasksServices>();
builder.Services.AddScoped<IStoresOperationsService, StoresOperationsService>();
builder.Services.AddScoped<IReportsServices, ReportsServices>();
builder.Services.AddScoped<IClientServices, ClientService>();


builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("userType", "3");
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Supervisor", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("userType", "4");
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("employee", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("role", "employee");
    });
});

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

//Configure Swagger Versioning support
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger(options =>
    {

        options.PreSerializeFilters.Add((swagger, req) =>
        {
            swagger.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = $"https://{req.Host}" } };
        });
    });
    app.UseSwaggerUI(options =>
    {
        foreach (var desc in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"../swagger/{desc.GroupName}/swagger.json", desc.ApiVersion.ToString());
            options.DefaultModelsExpandDepth(-1);
            options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        }
    });

//}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

