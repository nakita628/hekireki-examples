use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "UserSecurityAuditLogType"
)]
pub enum UserSecurityAuditLogType {
    #[sea_orm(string_value = "ACCOUNT_PROFILE_UPDATE")]
    AccountProfileUpdate,
    #[sea_orm(string_value = "ACCOUNT_SSO_LINK")]
    AccountSsoLink,
    #[sea_orm(string_value = "ACCOUNT_SSO_UNLINK")]
    AccountSsoUnlink,
    #[sea_orm(string_value = "ORGANISATION_SSO_LINK")]
    OrganisationSsoLink,
    #[sea_orm(string_value = "ORGANISATION_SSO_UNLINK")]
    OrganisationSsoUnlink,
    #[sea_orm(string_value = "AUTH_2FA_DISABLE")]
    Auth2faDisable,
    #[sea_orm(string_value = "AUTH_2FA_ENABLE")]
    Auth2faEnable,
    #[sea_orm(string_value = "PASSKEY_CREATED")]
    PasskeyCreated,
    #[sea_orm(string_value = "PASSKEY_DELETED")]
    PasskeyDeleted,
    #[sea_orm(string_value = "PASSKEY_UPDATED")]
    PasskeyUpdated,
    #[sea_orm(string_value = "PASSWORD_RESET")]
    PasswordReset,
    #[sea_orm(string_value = "PASSWORD_UPDATE")]
    PasswordUpdate,
    #[sea_orm(string_value = "SESSION_REVOKED")]
    SessionRevoked,
    #[sea_orm(string_value = "SIGN_OUT")]
    SignOut,
    #[sea_orm(string_value = "SIGN_IN")]
    SignIn,
    #[sea_orm(string_value = "SIGN_IN_FAIL")]
    SignInFail,
    #[sea_orm(string_value = "SIGN_IN_2FA_FAIL")]
    SignIn2faFail,
    #[sea_orm(string_value = "SIGN_IN_PASSKEY_FAIL")]
    SignInPasskeyFail,
}
