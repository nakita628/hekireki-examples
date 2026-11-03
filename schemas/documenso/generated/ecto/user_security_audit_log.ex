defmodule Documenso.UserSecurityAuditLog do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          type: atom(),
          user_agent: String.t() | nil,
          ip_address: String.t() | nil,
          inserted_at: DateTime.t(),
          user: Documenso.User.t() | nil
        }

  schema "UserSecurityAuditLog" do
    field(:type, Ecto.Enum, values: [:ACCOUNT_PROFILE_UPDATE, :ACCOUNT_SSO_LINK, :ACCOUNT_SSO_UNLINK, :ORGANISATION_SSO_LINK, :ORGANISATION_SSO_UNLINK, :AUTH_2FA_DISABLE, :AUTH_2FA_ENABLE, :PASSKEY_CREATED, :PASSKEY_DELETED, :PASSKEY_UPDATED, :PASSWORD_RESET, :PASSWORD_UPDATE, :SESSION_REVOKED, :SIGN_OUT, :SIGN_IN, :SIGN_IN_FAIL, :SIGN_IN_2FA_FAIL, :SIGN_IN_PASSKEY_FAIL])
    field(:user_agent, :string, source: :userAgent)
    field(:ip_address, :string, source: :ipAddress)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Documenso.User, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end