defmodule Trigger.BackgroundWorker do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          engine: atom(),
          content_hash: String.t(),
          sdk_version: String.t(),
          cli_version: String.t(),
          version: String.t(),
          metadata: map(),
          runtime: String.t() | nil,
          runtime_version: String.t() | nil,
          supports_lazy_attempts: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Trigger.Project.t() | nil,
          runtime_environment: Trigger.RuntimeEnvironment.t() | nil,
          worker_group: Trigger.WorkerInstanceGroup.t() | nil,
          deployment: Trigger.WorkerDeployment.t() | nil,
          tasks: [Trigger.BackgroundWorkerTask.t()],
          attempts: [Trigger.TaskRunAttempt.t()],
          locked_runs: [Trigger.TaskRun.t()],
          prompt_versions: [Trigger.PromptVersion.t()],
          task_identifiers: [Trigger.TaskIdentifier.t()],
          files: [Trigger.BackgroundWorkerFile.t()],
          queues: [Trigger.TaskQueue.t()]
        }

  schema "BackgroundWorker" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:engine, Ecto.Enum, values: [:V1, :V2], default: :V1)
    field(:content_hash, :string, source: :contentHash)
    field(:sdk_version, :string, default: "unknown", source: :sdkVersion)
    field(:cli_version, :string, default: "unknown", source: :cliVersion)
    field(:version, :string)
    field(:metadata, :map)
    field(:runtime, :string)
    field(:runtime_version, :string, source: :runtimeVersion)
    field(:supports_lazy_attempts, :boolean, default: false, source: :supportsLazyAttempts)
    field(:project_id, :string, source: :projectId)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    field(:worker_group_id, :string, source: :workerGroupId)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:runtime_environment, Trigger.RuntimeEnvironment, foreign_key: :runtime_environment_id, define_field: false, type: :string)
    belongs_to(:worker_group, Trigger.WorkerInstanceGroup, foreign_key: :worker_group_id, define_field: false, type: :string)
    has_one(:deployment, Trigger.WorkerDeployment, foreign_key: :worker_id, references: :id)
    has_many(:tasks, Trigger.BackgroundWorkerTask, foreign_key: :worker_id, references: :id)
    has_many(:attempts, Trigger.TaskRunAttempt, foreign_key: :background_worker_id, references: :id)
    has_many(:locked_runs, Trigger.TaskRun, foreign_key: :locked_to_version_id, references: :id)
    has_many(:prompt_versions, Trigger.PromptVersion, foreign_key: :worker_id, references: :id)
    has_many(:task_identifiers, Trigger.TaskIdentifier, foreign_key: :current_worker_id, references: :id)
    many_to_many(:files, Trigger.BackgroundWorkerFile, join_through: "_BackgroundWorkerToBackgroundWorkerFile", join_keys: [A: :id, B: :id])
    many_to_many(:queues, Trigger.TaskQueue, join_through: "_BackgroundWorkerToTaskQueue", join_keys: [A: :id, B: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end