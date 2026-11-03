defmodule Teable.ComputedUpdateOutbox do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          base_id: String.t(),
          seed_table_id: String.t(),
          seed_record_ids: map() | nil,
          change_type: String.t(),
          steps: map() | nil,
          edges: map() | nil,
          status: String.t(),
          attempts: integer(),
          max_attempts: integer(),
          next_run_at: DateTime.t(),
          locked_at: DateTime.t() | nil,
          locked_by: String.t() | nil,
          last_error: String.t() | nil,
          estimated_complexity: integer(),
          plan_hash: String.t(),
          dirty_stats: map() | nil,
          run_id: String.t(),
          origin_run_ids: [String.t()],
          run_total_steps: integer(),
          run_completed_steps_before: integer(),
          affected_table_ids: [String.t()],
          affected_field_ids: [String.t()],
          sync_max_level: integer() | nil,
          source_changed_at: DateTime.t() | nil,
          stage_depth: integer(),
          predecessor_task_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          seeds: [Teable.ComputedUpdateOutboxSeed.t()]
        }

  schema "computed_update_outbox" do
    field(:id, :string, primary_key: true)
    field(:base_id, :string)
    field(:seed_table_id, :string)
    field(:seed_record_ids, :map)
    field(:change_type, :string)
    field(:steps, :map)
    field(:edges, :map)
    field(:status, :string)
    field(:attempts, :integer, default: 0)
    field(:max_attempts, :integer, default: 8)
    field(:next_run_at, Teable.PrismaDateTime, autogenerate: true)
    field(:locked_at, Teable.PrismaDateTime)
    field(:locked_by, :string)
    field(:last_error, :string)
    field(:estimated_complexity, :integer, default: 0)
    field(:plan_hash, :string)
    field(:dirty_stats, :map)
    field(:run_id, :string)
    field(:origin_run_ids, {:array, :string})
    field(:run_total_steps, :integer, default: 0)
    field(:run_completed_steps_before, :integer, default: 0)
    field(:affected_table_ids, {:array, :string})
    field(:affected_field_ids, {:array, :string})
    field(:sync_max_level, :integer)
    field(:source_changed_at, Teable.PrismaDateTime)
    field(:stage_depth, :integer, default: 0)
    field(:predecessor_task_id, :string)
    has_many(:seeds, Teable.ComputedUpdateOutboxSeed, foreign_key: :task_id)
    timestamps(inserted_at_source: :created_at)
  end
end