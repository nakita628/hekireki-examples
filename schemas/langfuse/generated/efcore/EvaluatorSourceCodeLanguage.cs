#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum EvaluatorSourceCodeLanguage
{
    [PgName("PYTHON")]
    Python,

    [PgName("TYPESCRIPT")]
    Typescript,
}
