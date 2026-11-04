defmodule Trigger.TaskRun do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          number: integer(),
          friendly_id: String.t(),
          engine: atom(),
          status: atom(),
          status_reason: String.t() | nil,
          idempotency_key: String.t() | nil,
          idempotency_key_expires_at: DateTime.t() | nil,
          idempotency_key_options: map() | nil,
          debounce: map() | nil,
          task_identifier: String.t(),
          is_test: boolean(),
          payload: String.t(),
          payload_type: String.t(),
          context: map() | nil,
          trace_context: map() | nil,
          trace_id: String.t(),
          span_id: String.t(),
          environment_type: atom() | nil,
          organization_id: String.t() | nil,
          queue: String.t(),
          locked_queue_id: String.t() | nil,
          worker_queue: String.t(),
          region: String.t() | nil,
          secondary_master_queue: String.t() | nil,
          attempt_number: integer() | nil,
          run_tags: [String.t()],
          task_version: String.t() | nil,
          sdk_version: String.t() | nil,
          cli_version: String.t() | nil,
          started_at: DateTime.t() | nil,
          executed_at: DateTime.t() | nil,
          completed_at: DateTime.t() | nil,
          machine_preset: String.t() | nil,
          usage_duration_ms: integer(),
          cost_in_cents: float(),
          base_cost_in_cents: float(),
          locked_at: DateTime.t() | nil,
          priority_ms: integer(),
          concurrency_key: String.t() | nil,
          gates: map() | nil,
          delay_until: DateTime.t() | nil,
          queued_at: DateTime.t() | nil,
          ttl: String.t() | nil,
          expired_at: DateTime.t() | nil,
          max_attempts: integer() | nil,
          locked_retry_config: map() | nil,
          one_time_use_token: String.t() | nil,
          task_event_store: String.t(),
          queue_timestamp: DateTime.t() | nil,
          schedule_instance_id: String.t() | nil,
          schedule_id: String.t() | nil,
          bulk_action_group_ids: [String.t()],
          logs_deleted_at: DateTime.t() | nil,
          replayed_from_task_run_friendly_id: String.t() | nil,
          resume_parent_on_completion: boolean(),
          depth: integer(),
          parent_span_id: String.t() | nil,
          run_chain_state: map() | nil,
          seed_metadata: String.t() | nil,
          seed_metadata_type: String.t(),
          metadata: String.t() | nil,
          metadata_type: String.t(),
          metadata_version: integer(),
          annotations: map() | nil,
          is_warm_start: boolean() | nil,
          output: String.t() | nil,
          output_type: String.t(),
          error: map() | nil,
          plan_type: String.t() | nil,
          max_duration_in_seconds: integer() | nil,
          realtime_streams_version: String.t(),
          realtime_streams: [String.t()],
          stream_basin_name: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          runtime_environment: Trigger.RuntimeEnvironment.t() | nil,
          project: Trigger.Project.t() | nil,
          locked_by: Trigger.BackgroundWorkerTask.t() | nil,
          locked_to_version: Trigger.BackgroundWorker.t() | nil,
          root_task_run: Trigger.TaskRun.t() | nil,
          parent_task_run: Trigger.TaskRun.t() | nil,
          parent_task_run_attempt: Trigger.TaskRunAttempt.t() | nil,
          batch: Trigger.BatchTaskRun.t() | nil,
          associated_waitpoint: Trigger.Waitpoint.t() | nil,
          dependency: Trigger.TaskRunDependency.t() | nil,
          attempts: [Trigger.TaskRunAttempt.t()],
          checkpoints: [Trigger.Checkpoint.t()],
          blocked_by_waitpoints: [Trigger.TaskRunWaitpoint.t()],
          batch_items: [Trigger.BatchTaskRunItem.t()],
          checkpoint_restore_event: [Trigger.CheckpointRestoreEvent.t()],
          execution_snapshots: [Trigger.TaskRunExecutionSnapshot.t()],
          source_bulk_action_items: [Trigger.BulkActionItem.t()],
          destination_bulk_action_items: [Trigger.BulkActionItem.t()],
          descendant_runs: [Trigger.TaskRun.t()],
          child_runs: [Trigger.TaskRun.t()],
          playground_conversations: [Trigger.PlaygroundConversation.t()],
          connected_waitpoints: [Trigger.Waitpoint.t()]
        }

  schema "TaskRun" do
    field(:id, :string, primary_key: true)
    field(:number, :integer, default: 0)
    field(:friendly_id, :string, source: :friendlyId)
    field(:engine, Ecto.Enum, values: [:V1, :V2], default: :V1)
    field(:status, Ecto.Enum, values: [:DELAYED, :PENDING, :PENDING_VERSION, :WAITING_FOR_DEPLOY, :DEQUEUED, :EXECUTING, :WAITING_TO_RESUME, :RETRYING_AFTER_FAILURE, :PAUSED, :CANCELED, :INTERRUPTED, :COMPLETED_SUCCESSFULLY, :COMPLETED_WITH_ERRORS, :SYSTEM_FAILURE, :CRASHED, :EXPIRED, :TIMED_OUT], default: :PENDING)
    field(:status_reason, :string, source: :statusReason)
    field(:idempotency_key, :string, source: :idempotencyKey)
    field(:idempotency_key_expires_at, Trigger.PrismaDateTime, source: :idempotencyKeyExpiresAt)
    field(:idempotency_key_options, :map, source: :idempotencyKeyOptions)
    field(:debounce, :map)
    field(:task_identifier, :string, source: :taskIdentifier)
    field(:is_test, :boolean, default: false, source: :isTest)
    field(:payload, :string)
    field(:payload_type, :string, default: "application/json", source: :payloadType)
    field(:context, :map)
    field(:trace_context, :map, source: :traceContext)
    field(:trace_id, :string, source: :traceId)
    field(:span_id, :string, source: :spanId)
    field(:environment_type, Ecto.Enum, values: [:PRODUCTION, :STAGING, :DEVELOPMENT, :PREVIEW], source: :environmentType)
    field(:organization_id, :string, source: :organizationId)
    field(:queue, :string)
    field(:locked_queue_id, :string, source: :lockedQueueId)
    field(:worker_queue, :string, default: "main", source: :masterQueue)
    field(:region, :string)
    field(:secondary_master_queue, :string, source: :secondaryMasterQueue)
    field(:attempt_number, :integer, source: :attemptNumber)
    field(:run_tags, {:array, :string}, source: :runTags)
    field(:task_version, :string, source: :taskVersion)
    field(:sdk_version, :string, source: :sdkVersion)
    field(:cli_version, :string, source: :cliVersion)
    field(:started_at, Trigger.PrismaDateTime, source: :startedAt)
    field(:executed_at, Trigger.PrismaDateTime, source: :executedAt)
    field(:completed_at, Trigger.PrismaDateTime, source: :completedAt)
    field(:machine_preset, :string, source: :machinePreset)
    field(:usage_duration_ms, :integer, default: 0, source: :usageDurationMs)
    field(:cost_in_cents, :float, default: 0.0, source: :costInCents)
    field(:base_cost_in_cents, :float, default: 0.0, source: :baseCostInCents)
    field(:locked_at, Trigger.PrismaDateTime, source: :lockedAt)
    field(:priority_ms, :integer, default: 0, source: :priorityMs)
    field(:concurrency_key, :string, source: :concurrencyKey)
    field(:gates, :map)
    field(:delay_until, Trigger.PrismaDateTime, source: :delayUntil)
    field(:queued_at, Trigger.PrismaDateTime, source: :queuedAt)
    field(:ttl, :string)
    field(:expired_at, Trigger.PrismaDateTime, source: :expiredAt)
    field(:max_attempts, :integer, source: :maxAttempts)
    field(:locked_retry_config, :map, source: :lockedRetryConfig)
    field(:one_time_use_token, :string, source: :oneTimeUseToken)
    field(:task_event_store, :string, default: "taskEvent", source: :taskEventStore)
    field(:queue_timestamp, Trigger.PrismaDateTime, source: :queueTimestamp)
    field(:schedule_instance_id, :string, source: :scheduleInstanceId)
    field(:schedule_id, :string, source: :scheduleId)
    field(:bulk_action_group_ids, {:array, :string}, source: :bulkActionGroupIds)
    field(:logs_deleted_at, Trigger.PrismaDateTime, source: :logsDeletedAt)
    field(:replayed_from_task_run_friendly_id, :string, source: :replayedFromTaskRunFriendlyId)
    field(:resume_parent_on_completion, :boolean, default: false, source: :resumeParentOnCompletion)
    field(:depth, :integer, default: 0)
    field(:parent_span_id, :string, source: :parentSpanId)
    field(:run_chain_state, :map, source: :runChainState)
    field(:seed_metadata, :string, source: :seedMetadata)
    field(:seed_metadata_type, :string, default: "application/json", source: :seedMetadataType)
    field(:metadata, :string)
    field(:metadata_type, :string, default: "application/json", source: :metadataType)
    field(:metadata_version, :integer, default: 1, source: :metadataVersion)
    field(:annotations, :map)
    field(:is_warm_start, :boolean, source: :isWarmStart)
    field(:output, :string)
    field(:output_type, :string, default: "application/json", source: :outputType)
    field(:error, :map)
    field(:plan_type, :string, source: :planType)
    field(:max_duration_in_seconds, :integer, source: :maxDurationInSeconds)
    field(:realtime_streams_version, :string, default: "v1", source: :realtimeStreamsVersion)
    field(:realtime_streams, {:array, :string}, source: :realtimeStreams)
    field(:stream_basin_name, :string, source: :streamBasinName)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    field(:project_id, :string, source: :projectId)
    field(:locked_by_id, :string, source: :lockedById)
    field(:locked_to_version_id, :string, source: :lockedToVersionId)
    field(:root_task_run_id, :string, source: :rootTaskRunId)
    field(:parent_task_run_id, :string, source: :parentTaskRunId)
    field(:parent_task_run_attempt_id, :string, source: :parentTaskRunAttemptId)
    field(:batch_id, :string, source: :batchId)
    belongs_to(:runtime_environment, Trigger.RuntimeEnvironment, foreign_key: :runtime_environment_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:locked_by, Trigger.BackgroundWorkerTask, foreign_key: :locked_by_id, define_field: false, type: :string)
    belongs_to(:locked_to_version, Trigger.BackgroundWorker, foreign_key: :locked_to_version_id, define_field: false, type: :string)
    belongs_to(:root_task_run, Trigger.TaskRun, foreign_key: :root_task_run_id, define_field: false, type: :string)
    belongs_to(:parent_task_run, Trigger.TaskRun, foreign_key: :parent_task_run_id, define_field: false, type: :string)
    belongs_to(:parent_task_run_attempt, Trigger.TaskRunAttempt, foreign_key: :parent_task_run_attempt_id, define_field: false, type: :string)
    belongs_to(:batch, Trigger.BatchTaskRun, foreign_key: :batch_id, define_field: false, type: :string)
    has_one(:associated_waitpoint, Trigger.Waitpoint, foreign_key: :completed_by_task_run_id, references: :id)
    has_one(:dependency, Trigger.TaskRunDependency, foreign_key: :task_run_id, references: :id)
    has_many(:attempts, Trigger.TaskRunAttempt, foreign_key: :task_run_id, references: :id)
    has_many(:checkpoints, Trigger.Checkpoint, foreign_key: :run_id, references: :id)
    has_many(:blocked_by_waitpoints, Trigger.TaskRunWaitpoint, foreign_key: :task_run_id, references: :id)
    has_many(:batch_items, Trigger.BatchTaskRunItem, foreign_key: :task_run_id, references: :id)
    has_many(:checkpoint_restore_event, Trigger.CheckpointRestoreEvent, foreign_key: :run_id, references: :id)
    has_many(:execution_snapshots, Trigger.TaskRunExecutionSnapshot, foreign_key: :run_id, references: :id)
    has_many(:source_bulk_action_items, Trigger.BulkActionItem, foreign_key: :source_run_id, references: :id)
    has_many(:destination_bulk_action_items, Trigger.BulkActionItem, foreign_key: :destination_run_id, references: :id)
    has_many(:descendant_runs, Trigger.TaskRun, foreign_key: :root_task_run_id, references: :id)
    has_many(:child_runs, Trigger.TaskRun, foreign_key: :parent_task_run_id, references: :id)
    has_many(:playground_conversations, Trigger.PlaygroundConversation, foreign_key: :run_id, references: :id)
    many_to_many(:connected_waitpoints, Trigger.Waitpoint, join_through: "_WaitpointRunConnections", join_keys: [A: :id, B: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end