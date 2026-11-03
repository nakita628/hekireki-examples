#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum DocumentSource
{
    [PgName("DOCUMENT")]
    Document,

    [PgName("TEMPLATE")]
    Template,

    [PgName("TEMPLATE_DIRECT_LINK")]
    TemplateDirectLink,
}
