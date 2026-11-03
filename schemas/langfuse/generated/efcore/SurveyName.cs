#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum SurveyName
{
    [PgName("org_onboarding")]
    OrgOnboarding,

    [PgName("user_onboarding")]
    UserOnboarding,
}
