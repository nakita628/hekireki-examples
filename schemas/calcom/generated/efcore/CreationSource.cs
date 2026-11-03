#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum CreationSource
{
    [PgName("api_v1")]
    ApiV1,

    [PgName("api_v2")]
    ApiV2,

    [PgName("webapp")]
    Webapp,
}
