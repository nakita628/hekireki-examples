defmodule Formbricks.AuthzedProjectionOutbox do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          target_type: String.t(),
          primary_id: String.t(),
          secondary_id: String.t() | nil,
          is_revocation: boolean(),
          attempts: integer(),
          permanent_failures: integer(),
          available_at: DateTime.t(),
          leased_at: DateTime.t() | nil,
          lease_expires_at: DateTime.t() | nil,
          lease_owner: String.t() | nil,
          processed_at: DateTime.t() | nil,
          dead_lettered_at: DateTime.t() | nil,
          last_attempt_at: DateTime.t() | nil,
          last_error_code: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "AuthzedProjectionOutbox" do
    field(:target_type, :string, source: :targetType)
    field(:primary_id, :string, source: :primaryId)
    field(:secondary_id, :string, source: :secondaryId)
    field(:is_revocation, :boolean, default: false, source: :isRevocation)
    field(:attempts, :integer, default: 0)
    field(:permanent_failures, :integer, default: 0, source: :permanentFailures)
    field(:available_at, Formbricks.PrismaDateTime, autogenerate: true, source: :availableAt)
    field(:leased_at, Formbricks.PrismaDateTime, source: :leasedAt)
    field(:lease_expires_at, Formbricks.PrismaDateTime, source: :leaseExpiresAt)
    field(:lease_owner, :string, source: :leaseOwner)
    field(:processed_at, Formbricks.PrismaDateTime, source: :processedAt)
    field(:dead_lettered_at, Formbricks.PrismaDateTime, source: :deadLetteredAt)
    field(:last_attempt_at, Formbricks.PrismaDateTime, source: :lastAttemptAt)
    field(:last_error_code, :string, source: :lastErrorCode)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end