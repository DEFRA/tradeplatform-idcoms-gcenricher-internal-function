// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Infrastructure;

[ExcludeFromCodeCoverage]
public static class ConfigureMapperExtensions
{
    public static void ConfigureMapper(this IFunctionsHostBuilder hostBuilder)
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName is { } fullName && fullName.Contains("Defra"))
            .OrderBy(a => a.FullName)
            .ToList();
        hostBuilder.Services.AddAutoMapper(assembly);
    }
}
