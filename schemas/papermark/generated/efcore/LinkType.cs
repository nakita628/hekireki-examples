#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum LinkType
{
    [PgName("DOCUMENT_LINK")]
    DocumentLink,

    [PgName("DATAROOM_LINK")]
    DataroomLink,

    [PgName("WORKFLOW_LINK")]
    WorkflowLink,
}
