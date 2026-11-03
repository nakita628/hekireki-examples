#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum CreditUsageType
{
    [PgName("SMS")]
    Sms,

    [PgName("CAL_AI_PHONE_CALL")]
    CalAiPhoneCall,
}
