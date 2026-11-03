#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum EvalTemplateSourceCodeLanguage
{
    [PgName("PYTHON")]
    Python,

    [PgName("TYPESCRIPT")]
    Typescript,
}
