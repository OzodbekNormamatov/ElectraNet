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
using ElectraNet.WebApi.Middlewares;
using ElectraNet.DataAccess.UnitOfWorks;
namespace ElectraNet.WebApi.Helpers;

public static class ServicesCollection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<ICableService, CableService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ILaboratoryService, LaboratoryService>();
        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IServiceRecordService, ServiceRecordService>();
        services.AddScoped<ITransformerPointService, TransformerPointService>();
        services.AddScoped<ITransformerService, TransformerService>();
        services.AddScoped<IUserPermissionService, UserPermissionService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IUserService, UserService>();
    }

    public static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<AlreadyExistExceptionHandler>();
        services.AddExceptionHandler<ArgumentIsNotValidExceptionHandler>();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddExceptionHandler<InternalServerExceptionHandler>();
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
}