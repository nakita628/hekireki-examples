#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum AppCategories
{
    [PgName("calendar")]
    Calendar,

    [PgName("messaging")]
    Messaging,

    [PgName("other")]
    Other,

    [PgName("payment")]
    Payment,

    [PgName("video")]
    Video,

    [PgName("web3")]
    Web3,

    [PgName("automation")]
    Automation,

    [PgName("analytics")]
    Analytics,

    [PgName("conferencing")]
    Conferencing,

    [PgName("crm")]
    Crm,
}
