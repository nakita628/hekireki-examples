defmodule Trigger.BatchTaskRunItem do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          status: atom(),
          completed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          batch_task_run: Trigger.BatchTaskRun.t() | nil,
          task_run: Trigger.TaskRun.t() | nil,
          task_run_attempt: Trigger.TaskRunAttempt.t() | nil
        }

  schema "BatchTaskRunItem" do
    field(:id, :string, primary_key: true)
    field(:status, Ecto.Enum, values: [:PENDING, :FAILED, :CANCELED, :COMPLETED], default: :PENDING)
    field(:completed_at, Trigger.PrismaDateTime, source: :completedAt)
    field(:batch_task_run_id, :string, source: :batchTaskRunId)
    field(:task_run_id, :string, source: :taskRunId)
    field(:task_run_attempt_id, :string, source: :taskRunAttemptId)
    belongs_to(:batch_task_run, Trigger.BatchTaskRun, foreign_key: :batch_task_run_id, define_field: false, type: :string)
    belongs_to(:task_run, Trigger.TaskRun, foreign_key: :task_run_id, define_field: false, type: :string)
    belongs_to(:task_run_attempt, Trigger.TaskRunAttempt, foreign_key: :task_run_attempt_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end