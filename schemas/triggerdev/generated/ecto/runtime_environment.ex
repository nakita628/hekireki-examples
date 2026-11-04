defmodule Trigger.RuntimeEnvironment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          slug: String.t(),
          api_key: String.t(),
          root_api_key_hidden_at: DateTime.t() | nil,
          pk_api_key: String.t(),
          type: atom(),
          is_branchable_environment: boolean(),
          branch_name: String.t() | nil,
          preview_auto_archive_after_days: integer() | nil,
          preview_auto_archive_excluded_branches: [String.t()],
          preview_auto_archive_next_check_at: DateTime.t() | nil,
          preview_auto_archive_cursor_created_at: DateTime.t() | nil,
          preview_auto_archive_cursor_id: String.t() | nil,
          git: map() | nil,
          archived_at: DateTime.t() | nil,
          shortcode: String.t(),
          maximum_concurrency_limit: integer(),
          concurrency_limit_burst_factor: Decimal.t(),
          paused: boolean(),
          pause_source: atom() | nil,
          auto_enable_internal_sources: boolean(),
          built_in_environment_variable_overrides: map() | nil,
          tunnel_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          parent_environment: Trigger.RuntimeEnvironment.t() | nil,
          organization: Trigger.Organization.t() | nil,
          project: Trigger.Project.t() | nil,
          org_member: Trigger.OrgMember.t() | nil,
          current_session: Trigger.RuntimeEnvironmentSession.t() | nil,
          child_environments: [Trigger.RuntimeEnvironment.t()],
          background_workers: [Trigger.BackgroundWorker.t()],
          background_worker_tasks: [Trigger.BackgroundWorkerTask.t()],
          task_runs: [Trigger.TaskRun.t()],
          task_queues: [Trigger.TaskQueue.t()],
          environment_variable_values: [Trigger.EnvironmentVariableValue.t()],
          checkpoints: [Trigger.Checkpoint.t()],
          worker_deployments: [Trigger.WorkerDeployment.t()],
          worker_deployment_promotions: [Trigger.WorkerDeploymentPromotion.t()],
          task_run_attempts: [Trigger.TaskRunAttempt.t()],
          checkpoint_restore_event: [Trigger.CheckpointRestoreEvent.t()],
          task_schedule_instances: [Trigger.TaskScheduleInstance.t()],
          alerts: [Trigger.ProjectAlert.t()],
          sessions: [Trigger.RuntimeEnvironmentSession.t()],
          task_run_number_counter: [Trigger.TaskRunNumberCounter.t()],
          task_run_checkpoints: [Trigger.TaskRunCheckpoint.t()],
          waitpoints: [Trigger.Waitpoint.t()],
          worker_instances: [Trigger.WorkerInstance.t()],
          waitpoint_tags: [Trigger.WaitpointTag.t()],
          bulk_action_group: [Trigger.BulkActionGroup.t()],
          customer_queries: [Trigger.CustomerQuery.t()],
          prompts: [Trigger.Prompt.t()],
          playground_conversations: [Trigger.PlaygroundConversation.t()],
          error_group_states: [Trigger.ErrorGroupState.t()],
          task_identifiers: [Trigger.TaskIdentifier.t()],
          api_keys: [Trigger.ApiKey.t()],
          revoked_api_keys: [Trigger.RevokedApiKey.t()]
        }

  schema "RuntimeEnvironment" do
    field(:id, :string, primary_key: true)
    field(:slug, :string)
    field(:api_key, :string, source: :apiKey)
    field(:root_api_key_hidden_at, Trigger.PrismaDateTime, source: :rootApiKeyHiddenAt)
    field(:pk_api_key, :string, source: :pkApiKey)
    field(:type, Ecto.Enum, values: [:PRODUCTION, :STAGING, :DEVELOPMENT, :PREVIEW], default: :DEVELOPMENT)
    field(:is_branchable_environment, :boolean, default: false, source: :isBranchableEnvironment)
    field(:branch_name, :string, source: :branchName)
    field(:preview_auto_archive_after_days, :integer, source: :previewAutoArchiveAfterDays)
    field(:preview_auto_archive_excluded_branches, {:array, :string}, source: :previewAutoArchiveExcludedBranches)
    field(:preview_auto_archive_next_check_at, Trigger.PrismaDateTime, source: :previewAutoArchiveNextCheckAt)
    field(:preview_auto_archive_cursor_created_at, Trigger.PrismaDateTime, source: :previewAutoArchiveCursorCreatedAt)
    field(:preview_auto_archive_cursor_id, :string, source: :previewAutoArchiveCursorId)
    field(:git, :map)
    field(:archived_at, Trigger.PrismaDateTime, source: :archivedAt)
    field(:shortcode, :string)
    field(:maximum_concurrency_limit, :integer, default: 5, source: :maximumConcurrencyLimit)
    field(:concurrency_limit_burst_factor, :decimal, default: Decimal.new("2"), source: :concurrencyLimitBurstFactor)
    field(:paused, :boolean, default: false)
    field(:pause_source, Ecto.Enum, values: [:BILLING_LIMIT], source: :pauseSource)
    field(:auto_enable_internal_sources, :boolean, default: true, source: :autoEnableInternalSources)
    field(:built_in_environment_variable_overrides, :map, source: :builtInEnvironmentVariableOverrides)
    field(:tunnel_id, :string, source: :tunnelId)
    field(:parent_environment_id, :string, source: :parentEnvironmentId)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    field(:org_member_id, :string, source: :orgMemberId)
    field(:current_session_id, :string, source: :currentSessionId)
    belongs_to(:parent_environment, Trigger.RuntimeEnvironment, foreign_key: :parent_environment_id, define_field: false, type: :string)
    belongs_to(:organization, Trigger.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:org_member, Trigger.OrgMember, foreign_key: :org_member_id, define_field: false, type: :string)
    belongs_to(:current_session, Trigger.RuntimeEnvironmentSession, foreign_key: :current_session_id, define_field: false, type: :string)
    has_many(:child_environments, Trigger.RuntimeEnvironment, foreign_key: :parent_environment_id, references: :id)
    has_many(:background_workers, Trigger.BackgroundWorker, foreign_key: :runtime_environment_id, references: :id)
    has_many(:background_worker_tasks, Trigger.BackgroundWorkerTask, foreign_key: :runtime_environment_id, references: :id)
    has_many(:task_runs, Trigger.TaskRun, foreign_key: :runtime_environment_id, references: :id)
    has_many(:task_queues, Trigger.TaskQueue, foreign_key: :runtime_environment_id, references: :id)
    has_many(:environment_variable_values, Trigger.EnvironmentVariableValue, foreign_key: :environment_id, references: :id)
    has_many(:checkpoints, Trigger.Checkpoint, foreign_key: :runtime_environment_id, references: :id)
    has_many(:worker_deployments, Trigger.WorkerDeployment, foreign_key: :environment_id, references: :id)
    has_many(:worker_deployment_promotions, Trigger.WorkerDeploymentPromotion, foreign_key: :environment_id, references: :id)
    has_many(:task_run_attempts, Trigger.TaskRunAttempt, foreign_key: :runtime_environment_id, references: :id)
    has_many(:checkpoint_restore_event, Trigger.CheckpointRestoreEvent, foreign_key: :runtime_environment_id, references: :id)
    has_many(:task_schedule_instances, Trigger.TaskScheduleInstance, foreign_key: :environment_id, references: :id)
    has_many(:alerts, Trigger.ProjectAlert, foreign_key: :environment_id, references: :id)
    has_many(:sessions, Trigger.RuntimeEnvironmentSession, foreign_key: :environment_id, references: :id)
    has_many(:task_run_number_counter, Trigger.TaskRunNumberCounter, foreign_key: :environment_id, references: :id)
    has_many(:task_run_checkpoints, Trigger.TaskRunCheckpoint, foreign_key: :runtime_environment_id, references: :id)
    has_many(:waitpoints, Trigger.Waitpoint, foreign_key: :environment_id, references: :id)
    has_many(:worker_instances, Trigger.WorkerInstance, foreign_key: :environment_id, references: :id)
    has_many(:waitpoint_tags, Trigger.WaitpointTag, foreign_key: :environment_id, references: :id)
    has_many(:bulk_action_group, Trigger.BulkActionGroup, foreign_key: :environment_id, references: :id)
    has_many(:customer_queries, Trigger.CustomerQuery, foreign_key: :environment_id, references: :id)
    has_many(:prompts, Trigger.Prompt, foreign_key: :runtime_environment_id, references: :id)
    has_many(:playground_conversations, Trigger.PlaygroundConversation, foreign_key: :runtime_environment_id, references: :id)
    has_many(:error_group_states, Trigger.ErrorGroupState, foreign_key: :environment_id, references: :id)
    has_many(:task_identifiers, Trigger.TaskIdentifier, foreign_key: :runtime_environment_id, references: :id)
    has_many(:api_keys, Trigger.ApiKey, foreign_key: :runtime_environment_id, references: :id)
    has_many(:revoked_api_keys, Trigger.RevokedApiKey, foreign_key: :runtime_environment_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end