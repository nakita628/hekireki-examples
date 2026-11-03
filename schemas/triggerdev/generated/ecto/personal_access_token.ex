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
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Trigger.User, foreign_key: :user_id, define_field: false, type: :string)
    has_many(:authorization_codes, Trigger.AuthorizationCode, foreign_key: :personal_access_token_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end