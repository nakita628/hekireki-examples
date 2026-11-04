defmodule Trigger.TaskRunAttempt do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          number: integer(),
          friendly_id: String.t(),
          status: atom(),
          started_at: DateTime.t() | nil,
          completed_at: DateTime.t() | nil,
          usage_duration_ms: integer(),
          error: map() | nil,
          output: String.t() | nil,
          output_type: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          task_run: Trigger.TaskRun.t() | nil,
          background_worker: Trigger.BackgroundWorker.t() | nil,
          background_worker_task: Trigger.BackgroundWorkerTask.t() | nil,
          runtime_environment: Trigger.RuntimeEnvironment.t() | nil,
          queue: Trigger.TaskQueue.t() | nil,
          dependencies: [Trigger.TaskRunDependency.t()],
          batch_dependencies: [Trigger.BatchTaskRun.t()],
          checkpoints: [Trigger.Checkpoint.t()],
          batch_task_run_items: [Trigger.BatchTaskRunItem.t()],
          checkpoint_restore_event: [Trigger.CheckpointRestoreEvent.t()],
          child_runs: [Trigger.TaskRun.t()]
        }

  schema "TaskRunAttempt" do
    field(:id, :string, primary_key: true)
    field(:number, :integer, default: 0)
    field(:friendly_id, :string, source: :friendlyId)
    field(:status, Ecto.Enum, values: [:PENDING, :EXECUTING, :PAUSED, :FAILED, :CANCELED, :COMPLETED], default: :PENDING)
    field(:started_at, Trigger.PrismaDateTime, source: :startedAt)
    field(:completed_at, Trigger.PrismaDateTime, source: :completedAt)
    field(:usage_duration_ms, :integer, default: 0, source: :usageDurationMs)
    field(:error, :map)
    field(:output, :string)
    field(:output_type, :string, default: "application/json", source: :outputType)
    field(:task_run_id, :string, source: :taskRunId)
    field(:background_worker_id, :string, source: :backgroundWorkerId)
    field(:background_worker_task_id, :string, source: :backgroundWorkerTaskId)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    field(:queue_id, :string, source: :queueId)
    belongs_to(:task_run, Trigger.TaskRun, foreign_key: :task_run_id, define_field: false, type: :string)
    belongs_to(:background_worker, Trigger.BackgroundWorker, foreign_key: :background_worker_id, define_field: false, type: :string)
    belongs_to(:background_worker_task, Trigger.BackgroundWorkerTask, foreign_key: :background_worker_task_id, define_field: false, type: :string)
    belongs_to(:runtime_environment, Trigger.RuntimeEnvironment, foreign_key: :runtime_environment_id, define_field: false, type: :string)
    belongs_to(:queue, Trigger.TaskQueue, foreign_key: :queue_id, define_field: false, type: :string)
    has_many(:dependencies, Trigger.TaskRunDependency, foreign_key: :dependent_attempt_id, references: :id)
    has_many(:batch_dependencies, Trigger.BatchTaskRun, foreign_key: :dependent_task_attempt_id, references: :id)
    has_many(:checkpoints, Trigger.Checkpoint, foreign_key: :attempt_id, references: :id)
    has_many(:batch_task_run_items, Trigger.BatchTaskRunItem, foreign_key: :task_run_attempt_id, references: :id)
    has_many(:checkpoint_restore_event, Trigger.CheckpointRestoreEvent, foreign_key: :attempt_id, references: :id)
    has_many(:child_runs, Trigger.TaskRun, foreign_key: :parent_task_run_attempt_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end