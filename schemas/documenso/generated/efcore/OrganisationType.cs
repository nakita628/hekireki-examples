#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum OrganisationType
{
    [PgName("PERSONAL")]
    Personal,

    [PgName("ORGANISATION")]
    Organisation,
}
