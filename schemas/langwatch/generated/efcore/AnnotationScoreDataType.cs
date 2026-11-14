#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum AnnotationScoreDataType
{
    [PgName("CATEGORICAL")]
    Categorical,

    [PgName("BOOLEAN")]
    Boolean,

    [PgName("LIKERT")]
    Likert,

    [PgName("OPTION")]
    Option,

    [PgName("CHECKBOX")]
    Checkbox,
}
