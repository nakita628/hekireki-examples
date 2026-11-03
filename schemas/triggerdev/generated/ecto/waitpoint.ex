defmodule Trigger.Waitpoint do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          type: atom(),
          status: atom(),
          completed_at: DateTime.t() | nil,
          idempotency_key: String.t(),
          user_provided_idempotency_key: boolean(),
          idempotency_key_expires_at: DateTime.t() | nil,
          inactive_idempotency_key: String.t() | nil,
          completed_after: DateTime.t() | nil,
          output: String.t() | nil,
          output_type: String.t(),
          output_is_error: boolean(),
          tags: [String.t()],
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          completed_by_task_run: Trigger.TaskRun.t() | nil,
          completed_by_batch: Trigger.BatchTaskRun.t() | nil,
          project: Trigger.Project.t() | nil,
          environment: Trigger.RuntimeEnvironment.t() | nil,
          blocking_task_runs: [Trigger.TaskRunWaitpoint.t()],
          connected_runs: [Trigger.TaskRun.t()],
          completed_execution_snapshots: [Trigger.TaskRunExecutionSnapshot.t()]
        }

  schema "Waitpoint" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:type, Ecto.Enum, values: [:RUN, :DATETIME, :MANUAL, :BATCH])
    field(:status, Ecto.Enum, values: [:PENDING, :COMPLETED], default: :PENDING)
    field(:completed_at, Trigger.PrismaDateTime, source: :completedAt)
    field(:idempotency_key, :string, source: :idempotencyKey)
    field(:user_provided_idempotency_key, :boolean, source: :userProvidedIdempotencyKey)
    field(:idempotency_key_expires_at, Trigger.PrismaDateTime, source: :idempotencyKeyExpiresAt)
    field(:inactive_idempotency_key, :string, source: :inactiveIdempotencyKey)
    field(:completed_after, Trigger.PrismaDateTime, source: :completedAfter)
    field(:output, :string)
    field(:output_type, :string, default: "application/json", source: :outputType)
    field(:output_is_error, :boolean, default: false, source: :outputIsError)
    field(:tags, {:array, :string})
    field(:completed_by_task_run_id, :string, source: :completedByTaskRunId)
    field(:completed_by_batch_id, :string, source: :completedByBatchId)
    field(:project_id, :string, source: :projectId)
    field(:environment_id, :string, source: :environmentId)
    belongs_to(:completed_by_task_run, Trigger.TaskRun, foreign_key: :completed_by_task_run_id, define_field: false, type: :string)
    belongs_to(:completed_by_batch, Trigger.BatchTaskRun, foreign_key: :completed_by_batch_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:environment, Trigger.RuntimeEnvironment, foreign_key: :environment_id, define_field: false, type: :string)
    has_many(:blocking_task_runs, Trigger.TaskRunWaitpoint, foreign_key: :waitpoint_id)
    many_to_many(:connected_runs, Trigger.TaskRun, join_through: "_WaitpointRunConnections", join_keys: [B: :id, A: :id])
    many_to_many(:completed_execution_snapshots, Trigger.TaskRunExecutionSnapshot, join_through: "_completedWaitpoints", join_keys: [B: :id, A: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end