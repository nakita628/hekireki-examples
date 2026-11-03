class UserSecurityAuditLog < ApplicationRecord
  self.table_name = "UserSecurityAuditLog"
  self.inheritance_column = nil

  alias_attribute :created_at, :createdAt

  enum :type, { account_profile_update: "ACCOUNT_PROFILE_UPDATE", account_sso_link: "ACCOUNT_SSO_LINK", account_sso_unlink: "ACCOUNT_SSO_UNLINK", organisation_sso_link: "ORGANISATION_SSO_LINK", organisation_sso_unlink: "ORGANISATION_SSO_UNLINK", auth_2_fa_disable: "AUTH_2FA_DISABLE", auth_2_fa_enable: "AUTH_2FA_ENABLE", passkey_created: "PASSKEY_CREATED", passkey_deleted: "PASSKEY_DELETED", passkey_updated: "PASSKEY_UPDATED", password_reset: "PASSWORD_RESET", password_update: "PASSWORD_UPDATE", session_revoked: "SESSION_REVOKED", sign_out: "SIGN_OUT", sign_in: "SIGN_IN", sign_in_fail: "SIGN_IN_FAIL", sign_in_2_fa_fail: "SIGN_IN_2FA_FAIL", sign_in_passkey_fail: "SIGN_IN_PASSKEY_FAIL" },
       validate: true

  belongs_to :user, foreign_key: "userId", inverse_of: :security_audit_logs
end
