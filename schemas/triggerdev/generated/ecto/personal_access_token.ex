defmodule Trigger.PersonalAccessToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          encrypted_token: map(),
          obfuscated_token: String.t(),
          hashed_token: String.t(),
          revoked_at: DateTime.t() | nil,
          last_accessed_at: DateTime.t() | nil,
          expires_at: DateTime.t() | nil,
          idle_timeout_seconds: integer() | nil,
          expiry_reminder_sent_at: DateTime.t() | nil,
          expired_notice_sent_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Trigger.User.t() | nil,
          authorization_codes: [Trigger.AuthorizationCode.t()]
        }

  schema "PersonalAccessToken" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:encrypted_token, :map, source: :encryptedToken)
    field(:obfuscated_token, :string, source: :obfuscatedToken)
    field(:hashed_token, :string, source: :hashedToken)
    field(:revoked_at, Trigger.PrismaDateTime, source: :revokedAt)
    field(:last_accessed_at, Trigger.PrismaDateTime, source: :lastAccessedAt)
    field(:expires_at, Trigger.PrismaDateTime, source: :expiresAt)
    field(:idle_timeout_seconds, :integer, source: :idleTimeoutSeconds)
    field(:expiry_reminder_sent_at, Trigger.PrismaDateTime, source: :expiryReminderSentAt)
    field(:expired_notice_sent_at, Trigger.PrismaDateTime, source: :expiredNoticeSentAt)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Trigger.User, foreign_key: :user_id, define_field: false, type: :string)
    has_many(:authorization_codes, Trigger.AuthorizationCode, foreign_key: :personal_access_token_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end