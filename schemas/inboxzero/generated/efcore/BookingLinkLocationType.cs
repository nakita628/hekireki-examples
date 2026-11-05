#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum BookingLinkLocationType
{
    [PgName("IN_PERSON")]
    InPerson,

    [PgName("GOOGLE_MEET")]
    GoogleMeet,

    [PgName("MICROSOFT_TEAMS")]
    MicrosoftTeams,

    [PgName("PHONE")]
    Phone,

    [PgName("CUSTOM")]
    Custom,
}
