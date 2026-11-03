#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum ViewType
{
    [PgName("DOCUMENT_VIEW")]
    DocumentView,

    [PgName("DATAROOM_VIEW")]
    DataroomView,
}
