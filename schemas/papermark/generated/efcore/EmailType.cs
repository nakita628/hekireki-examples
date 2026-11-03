#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum EmailType
{
    [PgName("FIRST_DAY_DOMAIN_REMINDER_EMAIL")]
    FirstDayDomainReminderEmail,

    [PgName("FIRST_DOMAIN_INVALID_EMAIL")]
    FirstDomainInvalidEmail,

    [PgName("SECOND_DOMAIN_INVALID_EMAIL")]
    SecondDomainInvalidEmail,

    [PgName("FIRST_TRIAL_END_REMINDER_EMAIL")]
    FirstTrialEndReminderEmail,

    [PgName("FINAL_TRIAL_END_REMINDER_EMAIL")]
    FinalTrialEndReminderEmail,
}
