defmodule Teable.ComputedFieldActivity do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          field_id: String.t(),
          table_id: String.t(),
          base_id: String.t(),
          status: String.t(),
          active_task_count: integer(),
          processing_task_count: integer(),
          generation: integer(),
          estimated_complexity: integer(),
          estimated_dirty_records: integer(),
          has_all_target_records: boolean(),
          queued_at: DateTime.t() | nil,
          started_at: DateTime.t() | nil,
          last_completed_at: DateTime.t() | nil,
          last_duration_ms: integer() | nil,
          last_error: map() | nil,
          extensions: map() | nil,
          updated_at: DateTime.t()
        }

  schema "computed_field_activity" do
    field(:field_id, :string, primary_key: true)
    field(:table_id, :string)
    field(:base_id, :string)
    field(:status, :string)
    field(:active_task_count, :integer, default: 0)
    field(:processing_task_count, :integer, default: 0)
    field(:generation, :integer, default: 0)
    field(:estimated_complexity, :integer, default: 0)
    field(:estimated_dirty_records, :integer, default: 0)
    field(:has_all_target_records, :boolean, default: false)
    field(:queued_at, Teable.PrismaDateTime)
    field(:started_at, Teable.PrismaDateTime)
    field(:last_completed_at, Teable.PrismaDateTime)
    field(:last_duration_ms, :integer)
    field(:last_error, :map)
    field(:extensions, :map)
    field(:updated_at, Teable.PrismaDateTime, autogenerate: true)
  end
end