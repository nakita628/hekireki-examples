#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum AnalyticsIntegrationExportSource
{
    [PgName("TRACES_OBSERVATIONS")]
    TracesObservations,

    [PgName("TRACES_OBSERVATIONS_EVENTS")]
    TracesObservationsEvents,

    [PgName("EVENTS")]
    Events,
}
