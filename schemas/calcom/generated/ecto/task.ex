defmodule Calcom.Task do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          scheduled_at: DateTime.t(),
          succeeded_at: DateTime.t() | nil,
          type: String.t(),
          payload: String.t(),
          attempts: integer(),
          max_attempts: integer(),
          last_error: String.t() | nil,
          last_failed_attempt_at: DateTime.t() | nil,
          reference_uid: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "Task" do
    field(:scheduled_at, Calcom.PrismaDateTime, autogenerate: true, source: :scheduledAt)
    field(:succeeded_at, Calcom.PrismaDateTime, source: :succeededAt)
    field(:type, :string)
    field(:payload, :string)
    field(:attempts, :integer, default: 0)
    field(:max_attempts, :integer, default: 3, source: :maxAttempts)
    field(:last_error, :string, source: :lastError)
    field(:last_failed_attempt_at, Calcom.PrismaDateTime, source: :lastFailedAttemptAt)
    field(:reference_uid, :string, source: :referenceUid)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end