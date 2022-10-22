using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using student_integration_system_backend.Data.Import;
using student_integration_system_backend.Entities;
using student_integration_system_backend.Middleware;
using student_integration_system_backend.Models.Request;
using student_integration_system_backend.Services.AccountService;
using student_integration_system_backend.Services.AuthService;
using student_integration_system_backend.Services.ClientService;
using student_integration_system_backend.Services.ModeratorService;
using student_integration_system_backend.Services.PlaceOwnerService;
using student_integration_system_backend.Services.RoleService;
using student_integration_system_backend.Services.UserRoleService;
using student_integration_system_backend.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

//Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "STUDENT INTEGRATION SYSTEM API",
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.TagActionsBy(api =>
    {
        if (api.GroupName != null)
            return new[] {api.GroupName};
        if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            return new[] {controllerActionDescriptor.ControllerName};

        throw new InvalidOperationException("Unable to determine tag for endpoint.");
    });
    options.DocInclusionPredicate((_, _) => true);
});

// Exception handling
builder.Services.AddScoped<ExceptionHandlerMiddleware>();

//services
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<IDataImport, RolesImport>();
builder.Services.AddScoped<IDataImport, UsersImport>();
builder.Services.AddScoped<IDataImport, UserRolesImport>();
builder.Services.AddScoped<IClientService, ClientServiceImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<IUserRoleService, UserRoleServiceImpl>();
builder.Services.AddScoped<IAccountService, AccountServiceImpl>();
builder.Services.AddScoped<IModeratorService, ModeratorServiceImpl>();
builder.Services.AddScoped<IPlaceOwnerService, PlaceOwnerServiceImpl>();
builder.Services.AddScoped<IAuthService, AuthServiceImpl>();
builder.Services.AddScoped<IRoleService, RoleServiceImpl>();

//Fluent validation
builder.Services.AddFluentValidation();
builder.Services.AddFluentValidationRulesToSwagger();
builder.Services.AddScoped<IValidator<ClientSignUpRequest>, ClientSignUpRequestValidator>();
builder.Services.AddScoped<IValidator<ModeratorSignUpRequest>, ModeratorSignUpRequestValidator>();
builder.Services.AddScoped<IValidator<PlaceOwnerSignUpRequest>, PlaceOwnerSignUpRequestValidator>();
builder.Services.AddScoped<IValidator<SignInRequest>, SignInRequestValidator>();


//Database connection
builder.Services.AddDbContext<AppDbContext>(options =>
{
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    options.UseNpgsql(builder.Configuration.GetConnectionString("AppDb") ?? throw new InvalidOperationException());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();