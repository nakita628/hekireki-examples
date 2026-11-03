defmodule Trigger.CheckpointRestoreEvent do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          reason: String.t() | nil,
          metadata: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          checkpoint: Trigger.Checkpoint.t() | nil,
          run: Trigger.TaskRun.t() | nil,
          attempt: Trigger.TaskRunAttempt.t() | nil,
          project: Trigger.Project.t() | nil,
          runtime_environment: Trigger.RuntimeEnvironment.t() | nil,
          task_run_dependency: Trigger.TaskRunDependency.t() | nil,
          batch_task_run_dependency: Trigger.BatchTaskRun.t() | nil
        }

  schema "CheckpointRestoreEvent" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:CHECKPOINT, :RESTORE])
    field(:reason, :string)
    field(:metadata, :string)
    field(:checkpoint_id, :string, source: :checkpointId)
    field(:run_id, :string, source: :runId)
    field(:attempt_id, :string, source: :attemptId)
    field(:project_id, :string, source: :projectId)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    belongs_to(:checkpoint, Trigger.Checkpoint, foreign_key: :checkpoint_id, define_field: false, type: :string)
    belongs_to(:run, Trigger.TaskRun, foreign_key: :run_id, define_field: false, type: :string)
    belongs_to(:attempt, Trigger.TaskRunAttempt, foreign_key: :attempt_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:runtime_environment, Trigger.RuntimeEnvironment, foreign_key: :runtime_environment_id, define_field: false, type: :string)
    has_one(:task_run_dependency, Trigger.TaskRunDependency, foreign_key: :checkpoint_event_id)
    has_one(:batch_task_run_dependency, Trigger.BatchTaskRun, foreign_key: :checkpoint_event_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end