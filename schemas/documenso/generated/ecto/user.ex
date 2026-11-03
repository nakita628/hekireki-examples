defmodule Documenso.User do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t() | nil,
          email: String.t(),
          email_verified: DateTime.t() | nil,
          password: String.t() | nil,
          source: String.t() | nil,
          signature: String.t() | nil,
          last_signed_in: DateTime.t(),
          roles: atom(),
          identity_provider: atom(),
          disabled: boolean(),
          two_factor_secret: String.t() | nil,
          two_factor_enabled: boolean(),
          two_factor_backup_codes: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          avatar_image: Documenso.AvatarImage.t() | nil,
          accounts: [Documenso.Account.t()],
          sessions: [Documenso.Session.t()],
          password_reset_tokens: [Documenso.PasswordResetToken.t()],
          owned_organisations: [Documenso.Organisation.t()],
          organisation_member: [Documenso.OrganisationMember.t()],
          folders: [Documenso.Folder.t()],
          envelopes: [Documenso.Envelope.t()],
          verification_tokens: [Documenso.VerificationToken.t()],
          api_tokens: [Documenso.ApiToken.t()],
          security_audit_logs: [Documenso.UserSecurityAuditLog.t()],
          webhooks: [Documenso.Webhook.t()],
          site_settings: [Documenso.SiteSettings.t()],
          passkeys: [Documenso.Passkey.t()]
        }

  schema "User" do
    field(:name, :string)
    field(:email, :string)
    field(:email_verified, Documenso.PrismaDateTime, source: :emailVerified)
    field(:password, :string)
    field(:source, :string)
    field(:signature, :string)
    field(:last_signed_in, Documenso.PrismaDateTime, autogenerate: true, source: :lastSignedIn)
    field(:roles, {:array, Ecto.Enum}, values: [:ADMIN, :USER])
    field(:identity_provider, Ecto.Enum, values: [:DOCUMENSO, :GOOGLE, :OIDC], default: :DOCUMENSO, source: :identityProvider)
    field(:disabled, :boolean, default: false)
    field(:two_factor_secret, :string, source: :twoFactorSecret)
    field(:two_factor_enabled, :boolean, default: false, source: :twoFactorEnabled)
    field(:two_factor_backup_codes, :string, source: :twoFactorBackupCodes)
    field(:avatar_image_id, :string, source: :avatarImageId)
    belongs_to(:avatar_image, Documenso.AvatarImage, foreign_key: :avatar_image_id, define_field: false, type: :string)
    has_many(:accounts, Documenso.Account, foreign_key: :user_id)
    has_many(:sessions, Documenso.Session, foreign_key: :user_id)
    has_many(:password_reset_tokens, Documenso.PasswordResetToken, foreign_key: :user_id)
    has_many(:owned_organisations, Documenso.Organisation, foreign_key: :owner_user_id)
    has_many(:organisation_member, Documenso.OrganisationMember, foreign_key: :user_id)
    has_many(:folders, Documenso.Folder, foreign_key: :user_id)
    has_many(:envelopes, Documenso.Envelope, foreign_key: :user_id)
    has_many(:verification_tokens, Documenso.VerificationToken, foreign_key: :user_id)
    has_many(:api_tokens, Documenso.ApiToken, foreign_key: :user_id)
    has_many(:security_audit_logs, Documenso.UserSecurityAuditLog, foreign_key: :user_id)
    has_many(:webhooks, Documenso.Webhook, foreign_key: :user_id)
    has_many(:site_settings, Documenso.SiteSettings, foreign_key: :last_modified_by_user_id)
    has_many(:passkeys, Documenso.Passkey, foreign_key: :user_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end