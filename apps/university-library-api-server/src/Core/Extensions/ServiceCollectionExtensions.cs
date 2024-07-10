using UniversityLibraryApi.APIs;

namespace UniversityLibraryApi;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IRoomsService, RoomsService>();
        services.AddScoped<ISchedulesService, SchedulesService>();
        services.AddScoped<IStudentsService, StudentsService>();
        services.AddScoped<ITeachersService, TeachersService>();
    }
}
