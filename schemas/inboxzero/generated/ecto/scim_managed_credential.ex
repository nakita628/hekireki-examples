defmodule InboxZero.ScimManagedCredential do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          credential_id: String.t(),
          token_digest: String.t(),
          hash_version: String.t(),
          active_slot_key: String.t(),
          status: String.t(),
          serialized_scopes: String.t(),
          expires_at: DateTime.t(),
          created_at: DateTime.t(),
          created_by: String.t(),
          last_used_at: DateTime.t() | nil,
          revoked_at: DateTime.t() | nil,
          revoked_by: String.t() | nil,
          decommissioned_at: DateTime.t() | nil,
          scimmanagedconnection: InboxZero.ScimManagedConnection.t() | nil
        }

  schema "scimManagedCredential" do
    field(:id, :string, primary_key: true)
    field(:credential_id, :string, source: :credentialId)
    field(:token_digest, :string, source: :tokenDigest)
    field(:hash_version, :string, source: :hashVersion)
    field(:active_slot_key, :string, source: :activeSlotKey)
    field(:status, :string)
    field(:serialized_scopes, :string, source: :serializedScopes)
    field(:expires_at, InboxZero.PrismaDateTime, source: :expiresAt)
    field(:created_at, InboxZero.PrismaDateTime, source: :createdAt)
    field(:created_by, :string, source: :createdBy)
    field(:last_used_at, InboxZero.PrismaDateTime, source: :lastUsedAt)
    field(:revoked_at, InboxZero.PrismaDateTime, source: :revokedAt)
    field(:revoked_by, :string, source: :revokedBy)
    field(:decommissioned_at, InboxZero.PrismaDateTime, source: :decommissionedAt)
    field(:connection_record_id, :string, source: :connectionRecordId)
    belongs_to(:scimmanagedconnection, InboxZero.ScimManagedConnection, foreign_key: :connection_record_id, define_field: false, type: :string)
  end
end