defmodule Trigger.Project do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          slug: String.t(),
          name: String.t(),
          external_ref: String.t(),
          deleted_at: DateTime.t() | nil,
          initialized_at: DateTime.t() | nil,
          default_runtime: String.t() | nil,
          version: atom(),
          engine: atom(),
          builder_project_id: String.t() | nil,
          allowed_worker_queues: [String.t()],
          build_settings: map() | nil,
          onboarding_data: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Trigger.Organization.t() | nil,
          default_worker_group: Trigger.WorkerInstanceGroup.t() | nil,
          connected_github_repository: Trigger.ConnectedGithubRepository.t() | nil,
          trusted_deploy_repository: Trigger.TrustedDeployRepository.t() | nil,
          worker_groups: [Trigger.WorkerInstanceGroup.t()],
          workers: [Trigger.WorkerInstance.t()],
          environments: [Trigger.RuntimeEnvironment.t()],
          background_workers: [Trigger.BackgroundWorker.t()],
          background_worker_tasks: [Trigger.BackgroundWorkerTask.t()],
          task_runs: [Trigger.TaskRun.t()],
          run_tags: [Trigger.TaskRunTag.t()],
          task_queues: [Trigger.TaskQueue.t()],
          environment_variables: [Trigger.EnvironmentVariable.t()],
          checkpoints: [Trigger.Checkpoint.t()],
          worker_deployment: [Trigger.WorkerDeployment.t()],
          checkpoint_restore_event: [Trigger.CheckpointRestoreEvent.t()],
          task_schedules: [Trigger.TaskSchedule.t()],
          alert_channels: [Trigger.ProjectAlertChannel.t()],
          alerts: [Trigger.ProjectAlert.t()],
          alert_storages: [Trigger.ProjectAlertStorage.t()],
          bulk_action_groups: [Trigger.BulkActionGroup.t()],
          background_worker_file: [Trigger.BackgroundWorkerFile.t()],
          waitpoints: [Trigger.Waitpoint.t()],
          task_run_waitpoints: [Trigger.TaskRunWaitpoint.t()],
          task_run_checkpoints: [Trigger.TaskRunCheckpoint.t()],
          waitpoint_tags: [Trigger.WaitpointTag.t()],
          organization_project_integration: [Trigger.OrganizationProjectIntegration.t()],
          customer_queries: [Trigger.CustomerQuery.t()],
          playground_conversations: [Trigger.PlaygroundConversation.t()],
          task_schedule_instances: [Trigger.TaskScheduleInstance.t()],
          metrics_dashboards: [Trigger.MetricsDashboard.t()],
          llm_models: [Trigger.LlmModel.t()],
          prompts: [Trigger.Prompt.t()],
          platform_notifications: [Trigger.PlatformNotification.t()],
          error_group_states: [Trigger.ErrorGroupState.t()],
          task_identifiers: [Trigger.TaskIdentifier.t()]
        }

  schema "Project" do
    field(:id, :string, primary_key: true)
    field(:slug, :string)
    field(:name, :string)
    field(:external_ref, :string, source: :externalRef)
    field(:deleted_at, Trigger.PrismaDateTime, source: :deletedAt)
    field(:initialized_at, Trigger.PrismaDateTime, source: :initializedAt)
    field(:default_runtime, :string, source: :defaultRuntime)
    field(:version, Ecto.Enum, values: [:V2, :V3], default: :V2)
    field(:engine, Ecto.Enum, values: [:V1, :V2], default: :V1)
    field(:builder_project_id, :string, source: :builderProjectId)
    field(:allowed_worker_queues, {:array, :string}, source: :allowedMasterQueues)
    field(:build_settings, :map, source: :buildSettings)
    field(:onboarding_data, :map, source: :onboardingData)
    field(:organization_id, :string, source: :organizationId)
    field(:default_worker_group_id, :string, source: :defaultWorkerGroupId)
    belongs_to(:organization, Trigger.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:default_worker_group, Trigger.WorkerInstanceGroup, foreign_key: :default_worker_group_id, define_field: false, type: :string)
    has_one(:connected_github_repository, Trigger.ConnectedGithubRepository, foreign_key: :project_id, references: :id)
    has_one(:trusted_deploy_repository, Trigger.TrustedDeployRepository, foreign_key: :project_id, references: :id)
    has_many(:worker_groups, Trigger.WorkerInstanceGroup, foreign_key: :project_id, references: :id)
    has_many(:workers, Trigger.WorkerInstance, foreign_key: :project_id, references: :id)
    has_many(:environments, Trigger.RuntimeEnvironment, foreign_key: :project_id, references: :id)
    has_many(:background_workers, Trigger.BackgroundWorker, foreign_key: :project_id, references: :id)
    has_many(:background_worker_tasks, Trigger.BackgroundWorkerTask, foreign_key: :project_id, references: :id)
    has_many(:task_runs, Trigger.TaskRun, foreign_key: :project_id, references: :id)
    has_many(:run_tags, Trigger.TaskRunTag, foreign_key: :project_id, references: :id)
    has_many(:task_queues, Trigger.TaskQueue, foreign_key: :project_id, references: :id)
    has_many(:environment_variables, Trigger.EnvironmentVariable, foreign_key: :project_id, references: :id)
    has_many(:checkpoints, Trigger.Checkpoint, foreign_key: :project_id, references: :id)
    has_many(:worker_deployment, Trigger.WorkerDeployment, foreign_key: :project_id, references: :id)
    has_many(:checkpoint_restore_event, Trigger.CheckpointRestoreEvent, foreign_key: :project_id, references: :id)
    has_many(:task_schedules, Trigger.TaskSchedule, foreign_key: :project_id, references: :id)
    has_many(:alert_channels, Trigger.ProjectAlertChannel, foreign_key: :project_id, references: :id)
    has_many(:alerts, Trigger.ProjectAlert, foreign_key: :project_id, references: :id)
    has_many(:alert_storages, Trigger.ProjectAlertStorage, foreign_key: :project_id, references: :id)
    has_many(:bulk_action_groups, Trigger.BulkActionGroup, foreign_key: :project_id, references: :id)
    has_many(:background_worker_file, Trigger.BackgroundWorkerFile, foreign_key: :project_id, references: :id)
    has_many(:waitpoints, Trigger.Waitpoint, foreign_key: :project_id, references: :id)
    has_many(:task_run_waitpoints, Trigger.TaskRunWaitpoint, foreign_key: :project_id, references: :id)
    has_many(:task_run_checkpoints, Trigger.TaskRunCheckpoint, foreign_key: :project_id, references: :id)
    has_many(:waitpoint_tags, Trigger.WaitpointTag, foreign_key: :project_id, references: :id)
    has_many(:organization_project_integration, Trigger.OrganizationProjectIntegration, foreign_key: :project_id, references: :id)
    has_many(:customer_queries, Trigger.CustomerQuery, foreign_key: :project_id, references: :id)
    has_many(:playground_conversations, Trigger.PlaygroundConversation, foreign_key: :project_id, references: :id)
    has_many(:task_schedule_instances, Trigger.TaskScheduleInstance, foreign_key: :project_id, references: :id)
    has_many(:metrics_dashboards, Trigger.MetricsDashboard, foreign_key: :project_id, references: :id)
    has_many(:llm_models, Trigger.LlmModel, foreign_key: :project_id, references: :id)
    has_many(:prompts, Trigger.Prompt, foreign_key: :project_id, references: :id)
    has_many(:platform_notifications, Trigger.PlatformNotification, foreign_key: :project_id, references: :id)
    has_many(:error_group_states, Trigger.ErrorGroupState, foreign_key: :project_id, references: :id)
    has_many(:task_identifiers, Trigger.TaskIdentifier, foreign_key: :project_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end