#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum TemplateType
{
    [PgName("TRANSACTIONAL")]
    Transactional,

    [PgName("MARKETING")]
    Marketing,

    [PgName("HEADLESS")]
    Headless,
}
