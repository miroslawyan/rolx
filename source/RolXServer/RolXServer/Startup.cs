// -----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;

using RolXServer.Auth;
using RolXServer.Common.Errors;
using RolXServer.Common.Util;
using RolXServer.Common.WebApi;
using RolXServer.Projects;
using RolXServer.Records;
using RolXServer.Reports;
using RolXServer.Users;

namespace RolXServer;

/// <summary>
/// The application startup specification.
/// </summary>
public class Startup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Configures the services.
    /// </summary>
    /// <param name="services">The services.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers(o => o.Filters.Add(new NotFoundExceptionFilter()))
            .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new TimeSpanJsonSecondsConverter()))
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

        var connectionString = this.Configuration.GetConnectionString("RolXContext");
        services.AddDbContextPool<RolXContext>(options => options.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString),
            options => options.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: Array.Empty<int>())));

        services.AddProjects();
        services.AddAuth(this.Configuration);
        services.AddWorkRecord(this.Configuration);
        services.AddReports();
        services.AddUserManagement();
    }

    /// <summary>
    /// Configures the specified application.
    /// </summary>
    /// <param name="app">The application.</param>
    /// <param name="env">The environment.</param>
    /// <param name="dbContext">The database context.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RolXContext dbContext)
    {
        dbContext.Database.Migrate();

        app.UseStaticFiles();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseExceptionHandlerMiddleware();

        app.UseRouting();

        // Add CORS policy for development
        app.UseCors(builder => builder
            .WithOrigins("http://localhost:4200")
            .WithExposedHeaders("content-disposition")
            .AllowAnyHeader()
            .AllowAnyMethod());

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");
        });
    }
}
