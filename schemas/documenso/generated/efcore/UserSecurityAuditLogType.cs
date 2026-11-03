#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum UserSecurityAuditLogType
{
    [PgName("ACCOUNT_PROFILE_UPDATE")]
    AccountProfileUpdate,

    [PgName("ACCOUNT_SSO_LINK")]
    AccountSsoLink,

    [PgName("ACCOUNT_SSO_UNLINK")]
    AccountSsoUnlink,

    [PgName("ORGANISATION_SSO_LINK")]
    OrganisationSsoLink,

    [PgName("ORGANISATION_SSO_UNLINK")]
    OrganisationSsoUnlink,

    [PgName("AUTH_2FA_DISABLE")]
    Auth2faDisable,

    [PgName("AUTH_2FA_ENABLE")]
    Auth2faEnable,

    [PgName("PASSKEY_CREATED")]
    PasskeyCreated,

    [PgName("PASSKEY_DELETED")]
    PasskeyDeleted,

    [PgName("PASSKEY_UPDATED")]
    PasskeyUpdated,

    [PgName("PASSWORD_RESET")]
    PasswordReset,

    [PgName("PASSWORD_UPDATE")]
    PasswordUpdate,

    [PgName("SESSION_REVOKED")]
    SessionRevoked,

    [PgName("SIGN_OUT")]
    SignOut,

    [PgName("SIGN_IN")]
    SignIn,

    [PgName("SIGN_IN_FAIL")]
    SignInFail,

    [PgName("SIGN_IN_2FA_FAIL")]
    SignIn2faFail,

    [PgName("SIGN_IN_PASSKEY_FAIL")]
    SignInPasskeyFail,
}
