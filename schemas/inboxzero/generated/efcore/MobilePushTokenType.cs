#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum MobilePushTokenType
{
    [PgName("expo")]
    Expo,

    [PgName("apns")]
    Apns,

    [PgName("fcm")]
    Fcm,
}
