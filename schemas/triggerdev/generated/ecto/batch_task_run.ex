defmodule Trigger.BatchTaskRun do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          idempotency_key: String.t() | nil,
          idempotency_key_expires_at: DateTime.t() | nil,
          status: atom(),
          runtime_environment_id: String.t(),
          run_ids: [String.t()],
          run_count: integer(),
          payload: String.t() | nil,
          payload_type: String.t(),
          options: map() | nil,
          batch_version: String.t(),
          sealed: boolean(),
          sealed_at: DateTime.t() | nil,
          expected_count: integer(),
          completed_count: integer(),
          completed_at: DateTime.t() | nil,
          resumed_at: DateTime.t() | nil,
          processing_jobs_count: integer(),
          processing_jobs_expected_count: integer(),
          one_time_use_token: String.t() | nil,
          processing_started_at: DateTime.t() | nil,
          processing_completed_at: DateTime.t() | nil,
          successful_run_count: integer() | nil,
          failed_run_count: integer() | nil,
          task_identifier: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          checkpoint_event: Trigger.CheckpointRestoreEvent.t() | nil,
          dependent_task_attempt: Trigger.TaskRunAttempt.t() | nil,
          runs: [Trigger.TaskRun.t()],
          runs_blocked: [Trigger.TaskRunWaitpoint.t()],
          waitpoints: [Trigger.Waitpoint.t()],
          errors: [Trigger.BatchTaskRunError.t()],
          items: [Trigger.BatchTaskRunItem.t()],
          run_dependencies: [Trigger.TaskRunDependency.t()]
        }

  schema "BatchTaskRun" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:idempotency_key, :string, source: :idempotencyKey)
    field(:idempotency_key_expires_at, Trigger.PrismaDateTime, source: :idempotencyKeyExpiresAt)
    field(:status, Ecto.Enum, values: [:PENDING, :PROCESSING, :COMPLETED, :PARTIAL_FAILED, :ABORTED], default: :PENDING)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    field(:run_ids, {:array, :string}, source: :runIds)
    field(:run_count, :integer, default: 0, source: :runCount)
    field(:payload, :string)
    field(:payload_type, :string, default: "application/json", source: :payloadType)
    field(:options, :map)
    field(:batch_version, :string, default: "v1", source: :batchVersion)
    field(:sealed, :boolean, default: false)
    field(:sealed_at, Trigger.PrismaDateTime, source: :sealedAt)
    field(:expected_count, :integer, default: 0, source: :expectedCount)
    field(:completed_count, :integer, default: 0, source: :completedCount)
    field(:completed_at, Trigger.PrismaDateTime, source: :completedAt)
    field(:resumed_at, Trigger.PrismaDateTime, source: :resumedAt)
    field(:processing_jobs_count, :integer, default: 0, source: :processingJobsCount)
    field(:processing_jobs_expected_count, :integer, default: 0, source: :processingJobsExpectedCount)
    field(:one_time_use_token, :string, source: :oneTimeUseToken)
    field(:processing_started_at, Trigger.PrismaDateTime, source: :processingStartedAt)
    field(:processing_completed_at, Trigger.PrismaDateTime, source: :processingCompletedAt)
    field(:successful_run_count, :integer, source: :successfulRunCount)
    field(:failed_run_count, :integer, source: :failedRunCount)
    field(:task_identifier, :string, source: :taskIdentifier)
    field(:checkpoint_event_id, :string, source: :checkpointEventId)
    field(:dependent_task_attempt_id, :string, source: :dependentTaskAttemptId)
    belongs_to(:checkpoint_event, Trigger.CheckpointRestoreEvent, foreign_key: :checkpoint_event_id, define_field: false, type: :string)
    belongs_to(:dependent_task_attempt, Trigger.TaskRunAttempt, foreign_key: :dependent_task_attempt_id, define_field: false, type: :string)
    has_many(:runs, Trigger.TaskRun, foreign_key: :batch_id)
    has_many(:runs_blocked, Trigger.TaskRunWaitpoint, foreign_key: :batch_id)
    has_many(:waitpoints, Trigger.Waitpoint, foreign_key: :completed_by_batch_id)
    has_many(:errors, Trigger.BatchTaskRunError, foreign_key: :batch_task_run_id)
    has_many(:items, Trigger.BatchTaskRunItem, foreign_key: :batch_task_run_id)
    has_many(:run_dependencies, Trigger.TaskRunDependency, foreign_key: :dependent_batch_run_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end