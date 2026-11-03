#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum OrganisationGroupType
{
    [PgName("INTERNAL_ORGANISATION")]
    InternalOrganisation,

    [PgName("INTERNAL_TEAM")]
    InternalTeam,

    [PgName("CUSTOM")]
    Custom,
}
