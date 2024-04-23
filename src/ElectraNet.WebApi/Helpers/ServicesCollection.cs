using ElectraNet.Service.Helpers;
using ElectraNet.WebApi.Middlewares;
using ElectraNet.DataAccess.UnitOfWorks;
using ElectraNet.Service.Services.Users;
using ElectraNet.Service.Services.Assets;
using ElectraNet.Service.Services.UserRoles;

namespace ElectraNet.WebApi.Helpers;

public static class ServicesCollection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAssetService, AssetService>();
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