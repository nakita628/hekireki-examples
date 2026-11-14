defmodule LangWatch.MfaEnrollment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: String.t(),
          enrollment_id: String.t() | nil,
          method: String.t() | nil,
          state: String.t(),
          enrolled_at: DateTime.t() | nil,
          confirmed_at: DateTime.t() | nil,
          expired_at: DateTime.t() | nil,
          disabled_at: DateTime.t() | nil,
          disabled_via: String.t() | nil,
          backup_code_count: integer(),
          consumed_backup_code_indexes: [integer()],
          failed_count: integer(),
          occurred_at: DateTime.t(),
          last_event_id: String.t(),
          accepted_at: DateTime.t(),
          projection_version: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "MfaEnrollment" do
    field(:user_id, :string, primary_key: true, source: :userId)
    field(:enrollment_id, :string, source: :enrollmentId)
    field(:method, :string)
    field(:state, :string)
    field(:enrolled_at, LangWatch.PrismaDateTime, source: :enrolledAt)
    field(:confirmed_at, LangWatch.PrismaDateTime, source: :confirmedAt)
    field(:expired_at, LangWatch.PrismaDateTime, source: :expiredAt)
    field(:disabled_at, LangWatch.PrismaDateTime, source: :disabledAt)
    field(:disabled_via, :string, source: :disabledVia)
    field(:backup_code_count, :integer, default: 0, source: :backupCodeCount)
    field(:consumed_backup_code_indexes, {:array, :integer}, source: :consumedBackupCodeIndexes)
    field(:failed_count, :integer, default: 0, source: :failedCount)
    field(:occurred_at, LangWatch.PrismaDateTime, source: :occurredAt)
    field(:last_event_id, :string, source: :lastEventId)
    field(:accepted_at, LangWatch.PrismaDateTime, source: :acceptedAt)
    field(:projection_version, :string, source: :projectionVersion)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end