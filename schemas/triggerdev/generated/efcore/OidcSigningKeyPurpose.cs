#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum OidcSigningKeyPurpose
{
    [PgName("EXTERNAL_OIDC")]
    ExternalOidc,

    [PgName("INTERNAL_RUN")]
    InternalRun,
}
