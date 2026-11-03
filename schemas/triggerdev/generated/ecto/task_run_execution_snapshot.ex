defmodule Trigger.TaskRunExecutionSnapshot do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          engine: atom(),
          execution_status: atom(),
          description: String.t(),
          is_valid: boolean(),
          error: String.t() | nil,
          previous_snapshot_id: String.t() | nil,
          run_status: atom(),
          batch_id: String.t() | nil,
          attempt_number: integer() | nil,
          environment_id: String.t(),
          environment_type: atom(),
          project_id: String.t(),
          organization_id: String.t(),
          completed_waitpoint_order: [String.t()],
          worker_id: String.t() | nil,
          runner_id: String.t() | nil,
          last_heartbeat_at: DateTime.t() | nil,
          metadata: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          run: Trigger.TaskRun.t() | nil,
          checkpoint: Trigger.TaskRunCheckpoint.t() | nil,
          completed_waitpoints: [Trigger.Waitpoint.t()]
        }

  schema "TaskRunExecutionSnapshot" do
    field(:id, :string, primary_key: true)
    field(:engine, Ecto.Enum, values: [:V1, :V2], default: :V2)
    field(:execution_status, Ecto.Enum, values: [:RUN_CREATED, :DELAYED, :QUEUED, :QUEUED_EXECUTING, :PENDING_EXECUTING, :EXECUTING, :EXECUTING_WITH_WAITPOINTS, :SUSPENDED, :PENDING_CANCEL, :FINISHED], source: :executionStatus)
    field(:description, :string)
    field(:is_valid, :boolean, default: true, source: :isValid)
    field(:error, :string)
    field(:previous_snapshot_id, :string, source: :previousSnapshotId)
    field(:run_status, Ecto.Enum, values: [:DELAYED, :PENDING, :PENDING_VERSION, :WAITING_FOR_DEPLOY, :DEQUEUED, :EXECUTING, :WAITING_TO_RESUME, :RETRYING_AFTER_FAILURE, :PAUSED, :CANCELED, :INTERRUPTED, :COMPLETED_SUCCESSFULLY, :COMPLETED_WITH_ERRORS, :SYSTEM_FAILURE, :CRASHED, :EXPIRED, :TIMED_OUT], source: :runStatus)
    field(:batch_id, :string, source: :batchId)
    field(:attempt_number, :integer, source: :attemptNumber)
    field(:environment_id, :string, source: :environmentId)
    field(:environment_type, Ecto.Enum, values: [:PRODUCTION, :STAGING, :DEVELOPMENT, :PREVIEW], source: :environmentType)
    field(:project_id, :string, source: :projectId)
    field(:organization_id, :string, source: :organizationId)
    field(:completed_waitpoint_order, {:array, :string}, source: :completedWaitpointOrder)
    field(:worker_id, :string, source: :workerId)
    field(:runner_id, :string, source: :runnerId)
    field(:last_heartbeat_at, Trigger.PrismaDateTime, source: :lastHeartbeatAt)
    field(:metadata, :map)
    field(:run_id, :string, source: :runId)
    field(:checkpoint_id, :string, source: :checkpointId)
    belongs_to(:run, Trigger.TaskRun, foreign_key: :run_id, define_field: false, type: :string)
    belongs_to(:checkpoint, Trigger.TaskRunCheckpoint, foreign_key: :checkpoint_id, define_field: false, type: :string)
    many_to_many(:completed_waitpoints, Trigger.Waitpoint, join_through: "_completedWaitpoints", join_keys: [A: :id, B: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end