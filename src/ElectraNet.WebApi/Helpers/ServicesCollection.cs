using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.Helpers;
using ElectraNet.Service.Services.Assets;
using ElectraNet.Service.Services.Cables;
using ElectraNet.Service.Services.Employees;
using ElectraNet.Service.Services.Laboratories;
using ElectraNet.Service.Services.Organizations;
using ElectraNet.Service.Services.Permissions;
using ElectraNet.Service.Services.Positions;
using ElectraNet.Service.Services.ServiceRecords;
using ElectraNet.Service.Services.TransformerPoints;
using ElectraNet.Service.Services.Transformers;
using ElectraNet.Service.Services.UserPermissions;
using ElectraNet.Service.Services.UserRoles;
using ElectraNet.Service.Services.Users;
using ElectraNet.Service.Validators.Cables;
using ElectraNet.Service.Validators.Laboratories;
using ElectraNet.Service.Validators.Permissions;
using ElectraNet.Service.Validators.Positions;
using ElectraNet.WebApi.Middlewares;
using ElectraNet.WebApi.Validator.Employees;
using ElectraNet.WebApi.Validator.Organizations;
using ElectraNet.WebApi.Validator.ServiceRecords;
using ElectraNet.WebApi.Validator.TransformerPoints;
using ElectraNet.WebApi.Validator.Transformers;
using ElectraNet.WebApi.Validator.UserPermissions;
using ElectraNet.WebApi.Validator.UserRoles;
using ElectraNet.WebApi.Validator.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
namespace ElectraNet.WebApi.Helpers;

public static class ServicesCollection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IUserPermissionService, UserPermissionService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<ICableService, CableService>();
        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ITransformerService, TransformerService>();
        services.AddScoped<ITransformerPointService, TransformerPointService>();
        services.AddScoped<ILaboratoryService, LaboratoryService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IServiceRecordService, ServiceRecordService>();
    }

    public static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<AlreadyExistExceptionHandler>();
        services.AddExceptionHandler<ArgumentIsNotValidExceptionHandler>();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddExceptionHandler<InternalServerExceptionHandler>();
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddTransient<UserRoleCreateModelValidator>();
        services.AddTransient<UserRoleUpdateModelValidator>();

        services.AddTransient<UserCreateModelValidator>();
        services.AddTransient<UserUpdateModelValidator>();

        services.AddTransient<PermissionCreateModelValidator>();
        services.AddTransient<PermissionUpdateModelValidator>();

        services.AddTransient<UserPermissionCreateModelValidator>();
        services.AddTransient<UserPermissionUpdateModelValidator>();

        services.AddTransient<CableCreateModelValidator>();
        services.AddTransient<CableUpdateModelValidator>();

        services.AddTransient<OrganizationCreateModelValidator>();
        services.AddTransient<OrganizationUpdateModelValidator>();

        services.AddTransient<EmployeeCreateModelValidator>();
        services.AddTransient<EmployeeUpdateModelValidator>();

        services.AddTransient<TransformerCreateModelValidator>();
        services.AddTransient<TransformerUpdateModelValidator>();

        services.AddTransient<TransformerPointCreateModelValidator>();
        services.AddTransient<TransformerPointUpdateModelValidator>();

        services.AddTransient<LaboratoryCreateModelValidator>();
        services.AddTransient<LaboratoryUpdateModelValidator>();

        services.AddTransient<PositionCreateModelValidator>();
        services.AddTransient<PositionUpdateModelValidator>();

        services.AddTransient<ServiceRecordCreateModelValidator>();
        services.AddTransient<ServiceRecordUpdateModelValidator>();
    }

    public static void InjectEnvironmentItems(this WebApplication app)
    {
        HttpContextHelper.ContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
        EnvironmentHelper.WebRootPath = Path.GetFullPath("wwwroot");
        EnvironmentHelper.JWTKey = app.Configuration.GetSection("JWT:Key").Value;
        EnvironmentHelper.TokenLifeTimeInHours = app.Configuration.GetSection("JWT:LifeTime").Value;
        EnvironmentHelper.EmailAddress = app.Configuration.GetSection("Email:EmailAddress").Value;
        EnvironmentHelper.EmailPassword = app.Configuration.GetSection("Email:Password").Value;
        EnvironmentHelper.SmtpPort = app.Configuration.GetSection("Email:Port").Value;
        EnvironmentHelper.SmtpHost = app.Configuration.GetSection("Email:Host").Value;
    }

    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
        });
    }
}