#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum ParticipantRole
{
    [PgName("OWNER")]
    Owner,

    [PgName("PARTICIPANT")]
    Participant,
}
