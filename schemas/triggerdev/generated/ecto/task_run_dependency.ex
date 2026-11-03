defmodule Trigger.TaskRunDependency do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          resumed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          task_run: Trigger.TaskRun.t() | nil,
          checkpoint_event: Trigger.CheckpointRestoreEvent.t() | nil,
          dependent_attempt: Trigger.TaskRunAttempt.t() | nil,
          dependent_batch_run: Trigger.BatchTaskRun.t() | nil
        }

  schema "TaskRunDependency" do
    field(:id, :string, primary_key: true)
    field(:resumed_at, Trigger.PrismaDateTime, source: :resumedAt)
    field(:task_run_id, :string, source: :taskRunId)
    field(:checkpoint_event_id, :string, source: :checkpointEventId)
    field(:dependent_attempt_id, :string, source: :dependentAttemptId)
    field(:dependent_batch_run_id, :string, source: :dependentBatchRunId)
    belongs_to(:task_run, Trigger.TaskRun, foreign_key: :task_run_id, define_field: false, type: :string)
    belongs_to(:checkpoint_event, Trigger.CheckpointRestoreEvent, foreign_key: :checkpoint_event_id, define_field: false, type: :string)
    belongs_to(:dependent_attempt, Trigger.TaskRunAttempt, foreign_key: :dependent_attempt_id, define_field: false, type: :string)
    belongs_to(:dependent_batch_run, Trigger.BatchTaskRun, foreign_key: :dependent_batch_run_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end